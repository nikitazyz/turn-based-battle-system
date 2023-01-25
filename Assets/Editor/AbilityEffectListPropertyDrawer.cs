using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dices;
using Dices.AbilityEffects;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

[CustomEditor(typeof(DiceAbility))]
public class AbilityEffectListPropertyDrawer : Editor
{

    private string assetPath;
    private SerializedProperty effectsProperty;
    private SerializedProperty image;
    private List<Object> subObjects;

    private Type[] types;
    private SerializedProperty[] listEffects;

    private void OnEnable()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        assetPath = AssetDatabase.GetAssetPath(target);
        subObjects = AssetDatabase.LoadAllAssetRepresentationsAtPath(assetPath).ToList();
        effectsProperty = serializedObject.FindProperty("_effects");
        image = serializedObject.FindProperty("_image");
        SubObjectsNullCheck(subObjects);
        ListNullCheck(effectsProperty);
        listEffects = new SerializedProperty[effectsProperty.arraySize];
        for (int i = 0; i < effectsProperty.arraySize; i++)
        {
            listEffects[i] = effectsProperty.GetArrayElementAtIndex(i);
        }

        NoRefCheck(subObjects, listEffects);
        types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsSubclassOf(typeof(AbilityEffect))).ToArray();
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(image);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Effects", EditorStyles.largeLabel);
        DisplayList(listEffects);
        EditorGUILayout.Space();
        AddEffect(listEffects, assetPath);

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

    }

    private void DisplayList(SerializedProperty[] listEffects)
    {
        for (var i = 0; i < listEffects.Length; i++)
        {
            var effect = listEffects[i];
            var content = new GUIContent(LabelFormat(effect.objectReferenceValue.name));
            var i1 = i;
            SerializedObject serializedEffect = new SerializedObject(effect.objectReferenceValue);
            SerializedProperty isOpen = serializedEffect.FindProperty("_isOpen");
            isOpen.boolValue = EditorGUILayout.BeginFoldoutHeaderGroup(isOpen.boolValue, content, null,
                rect => MenuAction(serializedObject, i1));
            if (isOpen.boolValue)
            {
                var iterator = serializedEffect.GetIterator();
                iterator.Next(true);
                iterator.NextVisible(true);
                while (iterator.NextVisible(false))
                {
                    EditorGUILayout.PropertyField(iterator);
                }
            }

            EditorGUI.EndFoldoutHeaderGroup();
            serializedEffect.ApplyModifiedProperties();
            serializedEffect.Dispose();
        }
    }

    private static void NoRefCheck(IList<Object> subObjects, SerializedProperty[] listEffects)
    {
        bool hasChanges = false;
        for (int i = 0; i < subObjects.Count; i++)
        {
            if (listEffects.Any(eff => subObjects[i] == eff.objectReferenceValue)) continue;

            DestroyImmediate(subObjects[i], true);
            subObjects.Remove(subObjects[i]);
            hasChanges = true;
        }

        if (hasChanges)
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    private static void ListNullCheck(SerializedProperty effectsProperty)
    {
        for (int i = 0; i < effectsProperty.arraySize; i++)
        {
            SerializedProperty eff = effectsProperty.GetArrayElementAtIndex(i);
            if (eff.objectReferenceValue == null)
            {
                effectsProperty.DeleteArrayElementAtIndex(i);
            }
        }
    }

    private static void SubObjectsNullCheck(IList<Object> subObjects)
    {
        bool hasChanges = false;
        for (var i = 0; i < subObjects.Count; i++)
        {
            var subObject = subObjects[i];
            if (subObject != null) continue;
            Object.DestroyImmediate(subObject);
            subObjects.Remove(subObject);
            hasChanges = true;
        }

        if (hasChanges)
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    private void AddEffect(SerializedProperty[] listEffects, string assetPath)
    {
        if (types.All(t => listEffects.Any(le => t.IsInstanceOfType(le.objectReferenceValue))))
        {
            return;
        }

        if (!GUILayout.Button("Add effect")) return;

        GenericMenu genericMenu = new GenericMenu();

        foreach (var type in types)
        {
            if (Attribute.IsDefined(type, typeof(AbilityEffectAttribute)))
            {
                continue;
            }

            bool exists = listEffects.Any(serializedProperty =>
                type.IsInstanceOfType(serializedProperty.objectReferenceValue));

            if (exists)
            {
                continue;
            }

            SerializedObject serializedAbility = serializedObject;
            genericMenu.AddItem(new GUIContent(type.Name), false,
                () => AddAbilityEffect(type, assetPath, serializedAbility));
        }

        foreach (var type in types)
        {
            if (!Attribute.IsDefined(type, typeof(AbilityEffectAttribute)))
            {
                continue;
            }

            bool exists = listEffects.Any(serializedProperty =>
                type.IsInstanceOfType(serializedProperty.objectReferenceValue));

            if (exists)
            {
                continue;
            }

            AbilityEffectAttribute attribute =
                (AbilityEffectAttribute)type.GetCustomAttribute(typeof(AbilityEffectAttribute));
            SerializedObject serializedAbility = serializedObject;
            genericMenu.AddItem(new GUIContent(attribute.MenuName), false,
                () => AddAbilityEffect(type, assetPath, serializedAbility));
        }

        genericMenu.ShowAsContext();

    }

    private void MenuAction(SerializedObject serializedObject, int index)
    {
        var effectProperty = serializedObject.FindProperty("_effects");
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove"), false, () =>
        {
            var element = effectProperty.GetArrayElementAtIndex(index);
            AssetDatabase.RemoveObjectFromAsset(element.objectReferenceValue);
            DestroyImmediate(element.objectReferenceValue, true);
            effectProperty.DeleteArrayElementAtIndex(index);
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            OnValidate();
        });
        genericMenu.ShowAsContext();
    }

    public string LabelFormat(string text)
    {
        text = text.Trim(' ', '_');
        string result = "";
        bool isFirst = true;
        foreach (var c in text)
        {
            if (isFirst)
            {
                result += char.ToUpper(c);
                isFirst = false;
                continue;
            }

            if (char.IsUpper(c))
            {
                result += $" {c}";
                continue;
            }

            result += c;
        }

        return result;
    }

    private void AddAbilityEffect(Type type, string path, SerializedObject target)
    {
        ScriptableObject scriptableObject = CreateInstance(type);
        var effectProperty = target.FindProperty("_effects");
        scriptableObject.name = type.Name;

        AssetDatabase.AddObjectToAsset(scriptableObject, path);
        effectProperty.InsertArrayElementAtIndex(effectProperty.arraySize);
        effectProperty.GetArrayElementAtIndex(effectProperty.arraySize - 1).objectReferenceValue = scriptableObject;
        target.ApplyModifiedPropertiesWithoutUndo();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        OnValidate();
    }
}