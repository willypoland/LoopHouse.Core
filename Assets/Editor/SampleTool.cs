using System;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;


namespace Editor
{
    public sealed class GenericMenuExample : EditorWindow
    {
        [MenuItem("Example/GUI Color")]
        public static void Init()
        {
            EditorWindow window = GetWindow<GenericMenuExample>();
            window.position = new Rect(50f, 50f, 200f, 24f);
            window.Show();
        }

        [SerializeField] private Color _color = Color.white;

        private void OnEnable()
        {
            titleContent = new GUIContent("GUI Color");
        }

        void AddMenuItemForColor(GenericMenu menu, string menuPath, Color color)
        {
            menu.AddItem(new GUIContent(menuPath), _color.Equals(color), x => _color = (Color)x, color);
        }

        private void OnGUI()
        {
            GUI.color = _color;

            if (GUILayout.Button("Select GUI Color"))
            {
                var menu = new GenericMenu();
                
                AddMenuItemForColor(menu, "RGB/Red", Color.red);
                AddMenuItemForColor(menu, "RGB/Green", Color.green);
                AddMenuItemForColor(menu, "RGB/Blue", Color.blue);
                
                menu.AddSeparator("");
                
                AddMenuItemForColor(menu, "CMYK/Cyan", Color.cyan);
                AddMenuItemForColor(menu, "CMYK/Yellow", Color.yellow);
                AddMenuItemForColor(menu, "CMYK/Magenta", Color.magenta);
                
                menu.AddSeparator("CMYK/");
                
                AddMenuItemForColor(menu, "CMYK/Black", Color.black);
                
                menu.ShowAsContext();
            }
            
            
        }
    }
}