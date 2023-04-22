using CsUtility.Pool;
using UnityEngine;


namespace Game.Scripts
{
    public class ChunkObjectPoolFactory : IObjectPoolFactory<Chunk>
    {
        private readonly Chunk _source;
        private readonly Transform _root;

        public ChunkObjectPoolFactory(Chunk source, Transform root)
        {
            _source = source;
            _root = root;
        }


        public Chunk Create()
        {
            var chunk = Object.Instantiate(_source, _root);
            chunk.gameObject.SetActive(false);
            return chunk;
        }

        public void ActionOnGet(Chunk obj) => obj.gameObject.SetActive(true);

        public void ActionOnRelease(Chunk obj) => obj.gameObject.SetActive(false);

        public void ActionOnDispose(Chunk obj) => Object.Destroy(obj);
    }
}