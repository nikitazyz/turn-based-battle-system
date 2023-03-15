using SceneLoadSystem;
using UnityEngine;
using UnityEditor;
using UnityEditor.SearchService;


[CustomPropertyDrawer(typeof(ScenePathAttribute))]
public class ScenePathAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.type != "string")
        {
            return;
        }

        var style = new GUIStyle(EditorStyles.textField);

        if (!property.stringValue.StartsWith("Assets") ||
            AssetDatabase.LoadAssetAtPath<SceneAsset>(property.stringValue) == null)
        {
            style.normal.textColor = Color.red;
            style.hover.textColor = Color.red;
        }

        position.width -= 30;
        property.stringValue = EditorGUI.TextField(position, label, property.stringValue, style);

        position.x += position.width + 2;
        position.width = 28;

        int id = EditorGUIUtility.GetControlID(FocusType.Passive);
        
        if (GUI.Button(position, ""))
        {
            EditorGUIUtility.ShowObjectPicker<SceneAsset>(null, false, "t:Scene", id);
        }

        string eventState = Event.current.commandName;
        if (eventState == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID() == id)
        {
            property.stringValue = AssetDatabase.GetAssetPath(EditorGUIUtility.GetObjectPickerObject());
        }
    }
}
