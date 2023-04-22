using System.Collections.Generic;
using CsUtility.Extensions;
using CsUtility.Pool;
using UnityEngine;

namespace Game.Scripts
{
    public class ChunkViewPool
    {
        private readonly Transform _root;
        private readonly Dictionary<int, ObjectPool<Chunk>> _pools = new();

        public ChunkViewPool(Transform root)
        {
            _root = root;
        }

        public Chunk Get(Chunk srcChunk)
        {
            int id = srcChunk.GetInstanceID();
            if (!_pools.TryGetValue(id, out var pool))
            {
                var factory = new ChunkObjectPoolFactory(srcChunk, _root);
                pool = new ObjectPool<Chunk>(factory, 1);
                _pools.Add(id, pool);
            }

            return pool.Get().Item;
        }

        public void ReleaseAll()
        {
            foreach (var pool in _pools)
            {
                pool.Value.ReleaseAll();
            }
        }
    }

}