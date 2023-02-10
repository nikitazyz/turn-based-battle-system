using Animation;
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(CharacterAnimatorParameter))]
public class CharacterAnimatorParameterPropertyDrawer : PropertyDrawer
{
    private int _iterations;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position.height = EditorGUIUtility.singleLineHeight;
        SerializedProperty useManual = property.FindPropertyRelative("_useManualAnimatorController");

        EditorGUI.LabelField(position, label);
        position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.indentLevel++;
        EditorGUI.PropertyField(position, useManual);
        
        SerializedProperty manual = property.FindPropertyRelative("_manualAnimatorController");
        _iterations = 0;
        if (useManual.boolValue)
        {
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            _iterations = 1;
            EditorGUI.PropertyField(position, manual);
        }
        else
        {
            foreach (var sub in property)
            {
                SerializedProperty subProperty = (SerializedProperty)sub;
                if (subProperty.name == manual.name || subProperty.name == useManual.name)
                {
                    continue;
                }

                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                _iterations++;
                EditorGUI.PropertyField(position, subProperty);
            }
        }
        EditorGUI.indentLevel--;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * (_iterations + 2);
    }
}