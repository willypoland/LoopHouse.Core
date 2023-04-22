using UnityEngine;


namespace Game.Scripts
{
    public struct Pose
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public Pose(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        public Vector3 Forward => Rotation * Vector3.forward;
        
        public Vector3 Right => Rotation * Vector3.right;
        
        public Vector3 Up => Rotation * Vector3.up;

        public override string ToString() => $"<{Position} :: {Forward}>";

        public static Pose Identity => new Pose(Vector3.zero, Quaternion.identity);
    }
}