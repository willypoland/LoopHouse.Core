using Game.Scripts;
using UnityEditor;
using UnityEngine;


namespace Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyFieldAttribute))]
    public class ReadOnlyFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var content = new GUIContent(property.name, "Read only");
            
            switch (property.propertyType)
            {
                case SerializedPropertyType.Boolean:
                    EditorGUI.Toggle(position, content, property.boolValue);
                    break;
                case SerializedPropertyType.Integer:
                    EditorGUI.IntField(position, content, property.intValue);
                    break;
                case SerializedPropertyType.Float:
                    EditorGUI.FloatField(position, content, property.floatValue);
                    break;
                case SerializedPropertyType.String:
                    EditorGUI.TextField(position, content, property.stringValue);
                    break;
                default:
                    EditorGUI.PropertyField(position, property, new GUIContent(property.name, "Not read only yet"), true);
                    break;
            }
        }
    }
}