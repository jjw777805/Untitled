
using UnityEngine;

namespace MyUtils{
    public class Draw
    {
        public static void DrawSquare(Vector2 center, Vector2 size)
        {
            Vector3 center3D = new Vector3(center.x, center.y, 0);
            Vector3 halfSize = new Vector3(size.x / 2, size.y / 2, 0);

            // 计算四个顶点
            Vector3 topLeft = center3D + new Vector3(-halfSize.x, halfSize.y, 0);
            Vector3 topRight = center3D + new Vector3(halfSize.x, halfSize.y, 0);
            Vector3 bottomRight = center3D + new Vector3(halfSize.x, -halfSize.y, 0);
            Vector3 bottomLeft = center3D + new Vector3(-halfSize.x, -halfSize.y, 0);

            // 绘制四条边
            Debug.DrawLine(topLeft, topRight);
            Debug.DrawLine(topRight, bottomRight);
            Debug.DrawLine(bottomRight, bottomLeft);
            Debug.DrawLine(bottomLeft, topLeft);
        }
    }

}