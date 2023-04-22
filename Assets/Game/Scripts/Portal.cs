using CsUtility.Helpers;
using CsUtility.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;


namespace Game.Scripts
{

    public class Portal : MonoBehaviour
    {
        public static readonly Color LineColor = new Color(1f, 0.9215686f, 0.01568628f, 0.5f);
        public const float LineThickness = 2f;

        [SerializeField] private Chunk _chunk;
        [SerializeField] private float _width;
        [SerializeField] private float _height;
        [SerializeField] private float _depth;
        [SerializeField] private Portal _next;

        public float Width => _width;
        public float Height => _height;
        public float Depth => _depth;
        public Chunk Chunk => _chunk;
        
        public Portal Next
        {
            get => _next;
            set => _next = value;
        }

        public void SetConnection(Portal next)
        {
            _next = next;
            next._next = this;
        }

        private void Reset()
        {
            _chunk = GetComponentInParent<Chunk>();
        }

        private void OnDrawGizmos()
        {
            var size = new Vector3(_width, _height, _depth);
            Handles.zTest = CompareFunction.LessEqual;
            Handles.color = LineColor;
            Handles.matrix = transform.localToWorldMatrix;
            Handles.DrawWireCube(new Vector3(0, 0, _depth * 0.5f), size);
            
            if (Application.isPlaying)
                return;

            if (_next != null)
            {
                var socketSize = Vector2.one * _width * 0.25f;
                Handles.matrix = Matrix4x4.identity;

                var start = transform.TransformPoint(GetLocalOutPosition(this));
                var end = _next.transform.TransformPoint(GetLocalInPosition(_next));
                HandlesDraw.Rectangle(start, socketSize, transform.rotation);
                HandlesDraw.Rectangle(end, socketSize, _next.transform.rotation);
                HandlesDraw.Arrow(end, _next.transform.rotation * MathHelper.RotateY180, LineThickness);

                var tanLen = Vector3.Distance(start, end) * 0.45f;
                var forward = transform.forward;
                var nextForward = _next.transform.forward;
                var startTan = start + forward * tanLen;
                var endTan = end + nextForward * tanLen;
                Handles.DrawBezier(start, end, startTan, endTan, LineColor, null, LineThickness);
            }
        }

        private static Vector3 GetLocalInPosition(Portal portal)
        {
            var x = portal._width * -0.25f;
            return new Vector3(x, 0f);
        }

        private static Vector3 GetLocalOutPosition(Portal portal)
        {
            var localIn = GetLocalInPosition(portal);
            localIn.x = -localIn.x;
            return localIn;
        }
    }
}