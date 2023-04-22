using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Game.Scripts
{
    [ExecuteAlways]
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private Portal[] _portals;
        [SerializeField] private UnityEvent _whenEntering;
        [SerializeField] private UnityEvent _whenExiting;

        private void Reset()
        {
            _portals = GetComponentsInChildren<Portal>();
        }
        
        public int ConnectionCount => _portals.Length;

        public Portal OutPortal(int index) => _portals[index];

        public Portal InPortal(int index) => _portals[index].Next;

        public Chunk InChunk(int index) => _portals[index].Next.Chunk;

        public bool PortalExists(int index) =>
            !(_portals[index] == null ||
              _portals[index].Next == null ||
              _portals[index].Next.Chunk == null);

        public void Enter()
        {
            _whenEntering?.Invoke();
        }

        public void Exit()
        {
            _whenExiting?.Invoke();
        }
    }


}