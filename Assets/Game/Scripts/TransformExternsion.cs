using UnityEngine;


namespace Game.Scripts
{
    public static class TransformExternsion
    {
        public static Pose AsPose(this Transform self)
        {
            return new Pose(self.position, self.rotation);
        }

        public static Pose AsPose<T>(this T self) where T : Component
        {
            var transform = self.transform;
            return new Pose(transform.position, transform.rotation);
        }

        public static T SetPose<T>(this T self, in Pose pose) where T : Component
        {
            var transform = self.transform;
            transform.SetPositionAndRotation(pose.Position, pose.Rotation);
            return self;
        }

        public static Transform SetPose(this Transform self, in Pose pose)
        {
            self.SetPositionAndRotation(pose.Position, pose.Rotation);
            return self;
        }

        public static Transform SnapItself(this Transform self, Transform from, Transform to)
        {
            return SetPose(self, Util.Snap(self.AsPose(), @from.AsPose(), to.AsPose()));
        }
    }
}