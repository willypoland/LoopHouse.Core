using System.Linq;
using System.Net;
using Game.Scripts;
using UnityEditor;
using UnityEngine;


namespace Editor
{
    internal static class ChunkToolsEditor
    {
        [MenuItem("Tools/Chunk/Select all")]
        public static void SelectAllChunks()
        {
            var chunks = Object.FindObjectsOfType<Chunk>().Select(x => x.gameObject).ToArray();
            Debug.Log($"Select {chunks.Length} chunks");
            Selection.objects = chunks;
        }
        
        [MenuItem("Tools/Chunk/Reset link")]
        public static void ResetSelectedChunkLinks()
        {
            var chunks = Selection.gameObjects
                .Select(x => x.GetComponent<Chunk>())
                .Where(x => x != null);

            foreach (var chunk in chunks)
            {
                var serChunk = new SerializedObject(chunk);
                var propConnectors = serChunk.FindProperty("_portals");
                propConnectors.ClearArray();

                var portals = chunk.gameObject.GetComponentsInChildren<Portal>();
                for (int i = 0; i < portals.Length; i++) 
                {
                    propConnectors.InsertArrayElementAtIndex(i);
                    var item = propConnectors.GetArrayElementAtIndex(i);
                    item.objectReferenceValue = portals[i];
                    var serConnector = new SerializedObject(portals[i]);
                    serConnector.FindProperty("_chunk").objectReferenceValue = chunk;
                    serConnector.ApplyModifiedProperties();
                }

                serChunk.ApplyModifiedProperties();
            }
        }

    }
}