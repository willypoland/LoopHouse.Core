using System;
using System.Collections.Generic;
using System.Diagnostics;
using EasyCharacterMovement;
using UnityEngine;
using Debug = UnityEngine.Debug;


namespace Game.Scripts
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private Transform _viewRoot;
        [SerializeField] private Chunk[] _chunks;
        [SerializeField] private FirstPersonCharacter _character;
        [SerializeField] private Chunk _active;

        private ChunkViewPool _viewPool;

        private void Reset()
        {
            _chunks = FindObjectsOfType<Chunk>();
        }

        private void Start()
        {
            _viewPool = new ChunkViewPool(_viewRoot);
            

            foreach (var chunk in _chunks)
                chunk.gameObject.SetActive(false);
            
            _active.gameObject.SetActive(true);
            _active.Enter();
            
            SpawnNeighbourView(_active);
        }

        private void Update()
        {
            var fromPortal = DetectPortalIntersection(GetCenterCharcaterBody());

            if (fromPortal != null)
            {
                PreserveConnection(fromPortal);
                RebuildScene(fromPortal);
                SpawnNeighbourView(_active);
            }
        }

        private static void PreserveConnection(Portal fromPortal)
        {
            var nextPortal = fromPortal.Next;
            nextPortal.Next = fromPortal;
        }

        private void RebuildScene(Portal fromPortal)
        {
            Chunk fromChunk = _active;
            Portal toPortal = fromPortal.Next;
            Chunk toChunk = toPortal.Chunk;

            Pose fromPortalPose = fromPortal.AsPose();
            Pose toPortalPose = toPortal.AsPose();
            Pose toChunkPose = toChunk.AsPose();
            Pose charPose = new Pose(_character.GetPosition(), _character.GetRotation());

            Pose newToChunkPose = Util.Snap(toChunkPose, toPortalPose, fromPortalPose);
            Pose localCharPose = newToChunkPose.ToLocal(charPose);

            Pose newRoot = new Pose(Vector3.zero, newToChunkPose.Rotation);
            Pose newCharPose = newRoot.ToWorld(localCharPose);

            toChunk.SetPose(newRoot);
            _character.TeleportPosition(newCharPose.Position);
            // _character.TeleportRotation(newCharPose.Rotation);
            
            fromChunk.Exit();
            fromChunk.gameObject.SetActive(false);
            toChunk.gameObject.SetActive(true);
            toChunk.Enter();
            
            _active = toChunk;
        }

        private void SpawnNeighbourView(Chunk root)
        {
            _viewPool.ReleaseAll();
            for (int i = 0; i < root.ConnectionCount; i++)
            {
                if (!root.PortalExists(i))
                    continue;
                Debug.Log("view spawn");
                Pose outPortal = root.OutPortal(i).AsPose();
                Pose inPortal = root.InPortal(i).AsPose();
                Pose inChunk = root.InChunk(i).AsPose();
                Pose newInChunk = Util.Snap(inChunk, inPortal, outPortal);
                Chunk view = _viewPool.Get(root.InChunk(i));
                view.SetPose(newInChunk);
            }
        }

        private Portal DetectPortalIntersection(Vector3 point)
        {
            for (int i = 0; i < _active.ConnectionCount; i++)
            {
                var portal = _active.OutPortal(i);
                var portalPose = portal.transform.AsPose();
                var res = Util.IsFrontOfSquare(point, portalPose, portal.Width, portal.Height, portal.Depth);

                if (res)
                    return portal;
            }
            return null;
        }

        private Vector3 GetCenterCharcaterBody()
        {
            return _character.GetPosition() + Vector3.up * (_character.GetHeight() / 2f);
        }
    }


}