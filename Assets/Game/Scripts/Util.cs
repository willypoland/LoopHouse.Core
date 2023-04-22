using UnityEngine;

namespace Game.Scripts
{
    public static class Util
    {
        public static Pose ToWorld(this Pose root, Pose target)
        {
            return new Pose(root.Position + root.Rotation * target.Position, root.Rotation * target.Rotation);
        }

        public static Pose ToLocal(this Pose root, Pose target)
        {
            var inverse = Quaternion.Inverse(root.Rotation);
            return new Pose(inverse * (target.Position - root.Position), inverse * target.Rotation);
        }
        
        public static Pose SnapPoint(Pose to)
        {
            var rot = Quaternion.LookRotation(-to.Forward, to.Up);
            return new Pose(to.Position, rot);
        }

        public static Pose Snap(Pose body, Pose from, Pose to)
        {
            var end = SnapPoint(to);
            var local = from.ToLocal(body);
            return end.ToWorld(local);
        }

        public static bool IsFrontOf(Vector3 point, in Pose pose)
        {
            var dirToPose = pose.Position - point;
            var dot = Vector3.Dot(dirToPose, pose.Forward);
            return dot < 0f;
        }

        public static bool IsFrontOf(in this Pose pose, in Pose other)
        {
            var dirToPose = pose.Position - other.Position;
            var dotA = Vector3.Dot(dirToPose, pose.Forward);
            var dotB = Vector3.Dot(-dirToPose, other.Forward);
            return dotA < 0f && dotB < 0;
        }

        public static bool IsFrontOfSquare(in Vector3 point, in Pose squarePose, float width, float height, float depth)
        {
            var localPoint = point - squarePose.Position;
            var localRotatedPoint = Quaternion.Inverse(squarePose.Rotation) * localPoint;
            var dot = Vector3.Dot(localPoint, squarePose.Forward);

            var res =
                dot > 0f &&
                localRotatedPoint.z < depth &&
                Mathf.Abs(localRotatedPoint.x * 2f) < width &&
                Mathf.Abs(localRotatedPoint.y * 2f) < height;
            
            return res;
        }

        public static bool IsFrontOfSquare(in Vector3 point, in Pose boxPose, Vector3 size)
        {
            return IsFrontOfSquare(point, boxPose, size.x, size.y, size.z);
        }
    }
}