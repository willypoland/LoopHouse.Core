using UnityEngine;


namespace CsUtility.Extensions
{
    public static class VectorExtension
    {
        public static Vector3 Scaled(in this Vector3 self, in Vector3 scale) =>
            new Vector3 {x = self.x * scale.x, y = self.y * scale.y, z = self.z * scale.z};

        public static Vector3 Scaled(in this Vector3 self, float x, float y, float z) =>
            new Vector3 {x = self.x * x, y = self.y * y, z = self.z * z};
        
        public static Vector3 Scaled(in this Vector3 self, float value) =>
            new Vector3 {x = self.x * value, y = self.y * value, z = self.z * value};

        public static Vector2 Scaled(in this Vector2 self, in Vector2 scale) =>
            new Vector2 {x = self.x * scale.x, y = self.y * scale.y};

        public static Vector2 Scaled(in this Vector2 self, float x, float y) =>
            new Vector2 {x = self.x * x, y = self.y * y};
        
        public static Vector2 Scaled(in this Vector2 self, float value) =>
            new Vector2 {x = self.x * value, y = self.y * value};

        public static Vector2 ToXY(in this Vector3 self) =>
            new Vector2 {x = self.x, y = self.y};

    }
}