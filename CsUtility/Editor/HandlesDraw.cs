using System;
using CsUtility.Extensions;
using CsUtility.Helpers;
using UnityEditor;
using UnityEngine;


namespace CsUtility.Editor
{
    public static class HandlesDraw
    {
        public static void Arrow(Vector3 point, Quaternion rot, float thickness, float scale = 0.2f,
                                 bool viewScale = true)
        {
            var size = viewScale ? HandleUtility.GetHandleSize(point) * scale : scale;
            var left = rot * new Vector3(-size * 0.5f, 0f, -size) + point;
            var right = rot * new Vector3(size * 0.5f, 0f, -size) + point;

            Handles.DrawLine(point, left, thickness);
            Handles.DrawLine(point, right, thickness);
        }

        public static void Rectangle(Vector3 center, Vector2 size, Quaternion rotation)
        {
            var halfSize = size * 0.5f;
            var leftTop = rotation * new Vector3(-halfSize.x, halfSize.y, 0f) + center;
            var rightTop = rotation * new Vector3(halfSize.x, halfSize.y, 0f) + center;
            var rightDown = rotation * new Vector3(halfSize.x, -halfSize.y, 0f) + center;
            var leftDown = rotation * new Vector3(-halfSize.x, -halfSize.y, 0f) + center;

            Handles.DrawLine(leftTop, rightTop);
            Handles.DrawLine(rightTop, rightDown);
            Handles.DrawLine(rightDown, leftDown);
            Handles.DrawLine(leftDown, leftTop);
        }

        public static void Rectangle(Vector2 size)
        {
            var halfSize = size * 0.5f;
            var leftTop = new Vector3(-halfSize.x, halfSize.y, 0f);
            var rightTop = new Vector3(halfSize.x, halfSize.y, 0f);
            var rightDown = new Vector3(halfSize.x, -halfSize.y, 0f);
            var leftDown = new Vector3(-halfSize.x, -halfSize.y, 0f);

            Handles.DrawLine(leftTop, rightTop);
            Handles.DrawLine(rightTop, rightDown);
            Handles.DrawLine(rightDown, leftDown);
            Handles.DrawLine(leftDown, leftTop);
        }

        public static void Corner(Vector3 center, Vector3 size, Quaternion rotation)
        {
            var top = center + rotation * new Vector3(0, size.y, 0);
            var right = center + rotation * new Vector3(size.x, 0, 0);
            var forward = center + rotation * new Vector3(0, 0, size.z);

            Handles.DrawLine(center, top);
            Handles.DrawLine(center, right);
            Handles.DrawLine(center, forward);
        }

        public static void BoxCorner(Vector3 center, Vector3 size, Quaternion rotation, float cornersSize)
        {
            Span<Vector3> verts = stackalloc Vector3[MathHelper.BoxVertices.Count];

            for (int i = 0; i < verts.Length; i++)
                verts[i] = rotation * size.Scaled(MathHelper.BoxVertices[i]) + center;

            for (int i = 0; i < MathHelper.BoxLineIndices.Count; i += 2)
            {
                var start = verts[MathHelper.BoxLineIndices[i + 0]];
                var end = verts[MathHelper.BoxLineIndices[i + 1]];
                var mid1 = Vector3.MoveTowards(start, end, cornersSize);
                var mid2 = Vector3.MoveTowards(end, start, cornersSize);
                Handles.DrawLine(start, mid1);
                Handles.DrawLine(mid2, end);
            }
        }
    }
}