using System.Collections.Generic;
using UnityEngine;


namespace CsUtility.Helpers
{
    public static class MathHelper
    {
        public static readonly Quaternion RotateY180 = Quaternion.AngleAxis(180f, Vector3.up);
        
        public static readonly IReadOnlyList<Vector2> SquareVertices = new Vector2[]
        {
            new Vector2(-0.5f,  0.5f),
            new Vector2(-0.5f,  0.5f),
            new Vector2(-0.5f,  0.5f),
            new Vector2(-0.5f,  0.5f),
        };

        public static readonly IReadOnlyList<int> SquareLineIndices = new int[]
        {
            0, 1,   1, 2,   2, 3,   3, 0
        };
        
        public static readonly IReadOnlyList<Vector3> BoxVertices = new Vector3[]
        {
            new Vector3(-0.5f,  0.5f, -0.5f),
            new Vector3( 0.5f,  0.5f, -0.5f),
            new Vector3( 0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
            
            new Vector3(-0.5f,  0.5f,  0.5f),
            new Vector3( 0.5f,  0.5f,  0.5f),
            new Vector3( 0.5f, -0.5f,  0.5f),
            new Vector3(-0.5f, -0.5f,  0.5f),
        };

        public static readonly IReadOnlyList<int> BoxLineIndices = new int[]
        {
            0, 1,   1, 2,   2, 3,   3, 0,
            4, 5,   5, 6,   6, 7,   7, 4,
            0, 4,   1, 5,   2, 6,   3, 7,
        };
        
    }
}