using System.Collections.Generic;
using System.Linq;
using Game.Scripts;
using UnityEditor;
using UnityEngine;


namespace Editor
{
    internal static class ConnectorToolsEditor
    {
        [MenuItem("Tools/Connector/AutoConnect selection")]
        public static void MakeAutoConnect()
        {
            if (Selection.count == 0)
                return;

            var connectors = GetConnectors();
            foreach (var src in connectors)
            {
                var nearest = TryConnect(src, connectors, -0.3f);

                if (!ReferenceEquals(nearest, null))
                {
                    Undo.RecordObject(src, "auto connection");
                    src.Next = nearest;
                }
            }
        }
        
        private static Portal TryConnect(Portal src, IEnumerable<Portal> others, float lessThenDot)
        {
            var srcPos = src.transform.position;
            var result = others
                .Where(x => !ReferenceEquals(src, x))
                .Where(x => CheckPossibleConnection(src, x, lessThenDot))
                .OrderBy(x => Vector3.Distance(srcPos, x.transform.position))
                .ThenBy(x => Vector3.Dot(srcPos, x.transform.forward))
                .FirstOrDefault();
            return result;
        }


        private static bool CheckPossibleConnection(Portal src, Portal other, float lessThen)
        {
            var srcPos = src.transform.AsPose();
            var otherPos = other.transform.AsPose();
            return srcPos.IsFrontOf(otherPos); //&& Vector3.Dot(srcPos.Forward, otherPos.Position) < lessThen;
        }

        [MenuItem("Tools/Connector/Clear selection")]
        public static void ClearConnections()
        {
            foreach (var connector in GetConnectors())
            {
                var serialized = new SerializedObject(connector);
                serialized.FindProperty("_next").objectReferenceValue = null;
                serialized.ApplyModifiedProperties();
            }
        }

        private static IEnumerable<Portal> GetConnectors()
        {
            return Selection.gameObjects
                .SelectMany(x => x.GetComponentsInChildren<Portal>())
                .GroupBy(x => x.gameObject.GetInstanceID())
                .Select(x => x.First());
        }

    }
}