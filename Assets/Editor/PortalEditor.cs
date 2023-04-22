using Game.Scripts;
using CsUtility.Extensions;
using UnityEditor;
using UnityEngine;


namespace Editor
{

    [CustomEditor(typeof(Portal))]
    [CanEditMultipleObjects]
    internal sealed class PortalEditor : UnityEditor.Editor
    {
        private Portal[] _connectors;
        private GUIContent _selectContent;
        private GUIContent _nextContent;
        private Vector2 _buttonSize;

        private void OnEnable()
        {
            _connectors = FindObjectsOfType<Portal>();
        }

        private void CalculateStaticData()
        {
            _selectContent = new GUIContent("select");
            _nextContent = new GUIContent("next");
            var selectBtnSize = GUI.skin.button.CalcSize(_selectContent);
            var nextBtnSize = GUI.skin.button.CalcSize(_nextContent);
            var width = Mathf.Max(selectBtnSize.x, nextBtnSize.x) * 2f;
            var height = selectBtnSize.y;
            _buttonSize = new Vector2(width, height);
        }

        private void OnSceneGUI()
        {
            CalculateStaticData();
            DrawOtherConnectorHandles();
        }

        private void DrawOtherConnectorHandles()
        {
            Handles.BeginGUI();

            //DrawSelectionButton(target as Connector);
            foreach (var connector in _connectors)
            {
                if (ReferenceEquals(connector, target))
                    continue;
            
                DrawSelectionButton(connector);
            }

            Handles.EndGUI();
        }

        private void DrawSelectionButton(Portal portal)
        {
            var nameContent = new GUIContent(portal.name);
            var headerSize = GUI.skin.label.CalcSize(nameContent);

            var rectWidth = Mathf.Max(headerSize.x, _buttonSize.x) + 12;
            var rectHeight = headerSize.y + _buttonSize.y + 4;
            var rectSize = new Vector2(rectWidth, rectHeight);
            
            var guiPoint = HandleUtility.WorldToGUIPoint(portal.transform.position);
            var startPoint = guiPoint - rectSize.Scaled(0.5f, 0) - Vector2.down * 20f;
            var rect = new Rect(startPoint, rectSize);
            GUI.color = new Color(0.8f, 0.8f, 0.8f, 1.0f);
            GUI.DrawTexture(rect, Texture2D.whiteTexture);

            var pos = rect.position;
            pos.x += 4;

            var labelRect = new Rect(pos, headerSize);
            GUI.Label(labelRect, nameContent);
            
            pos.y += labelRect.height + 2;

            var btnRect1 = new Rect(pos, _buttonSize.Scaled(0.5f, 1f));
            if (GUI.Button(btnRect1, _selectContent))
            {
                Selection.activeObject = portal;
            }

            pos.x += btnRect1.width + 4;
            var btnRect2 = new Rect(pos, btnRect1.size);
            if (GUI.Button(btnRect2, _nextContent))
            {
                Undo.RecordObject(target, "change next");
                (target as Portal).Next = portal;
                EditorUtility.SetDirty(target);
                EditorUtility.SetDirty((target as Portal).Next);
            }
                        
        }
    }
}