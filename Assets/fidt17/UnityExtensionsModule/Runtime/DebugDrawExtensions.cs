using UnityEngine;

namespace fidt17.UnityExtensionsModule.Runtime
{
    public static class DebugDrawExtensions
    {
        /// <summary>
        /// Draws a vertical cross
        /// </summary>
        public static void DrawCross(Vector3 position, float size, Color color, float duration = 0)
        {
            float sizeHalf = size / 2;
            Debug.DrawLine(new Vector3(position.x, position.y - sizeHalf), new Vector3(position.x, position.y + size / 2), color, duration);
            Debug.DrawLine(new Vector3(position.x - sizeHalf, position.y), new Vector3(position.x + size / 2, position.y), color, duration);
        }

        /// <summary>
        /// Draws a diagonal cross in XY
        /// </summary>
        public static void DrawDiagonalCross(Vector2 position, float size, Color color, float duration = 0)
        {
            float sizeHalf = size / 2;
            Debug.DrawLine(new Vector3(position.x - sizeHalf, position.y + sizeHalf), new Vector3(position.x + sizeHalf, position.y - sizeHalf), color, duration);
            Debug.DrawLine(new Vector3(position.x - sizeHalf, position.y - sizeHalf), new Vector3(position.x + sizeHalf, position.y + sizeHalf), color, duration);
        }
        
        /// <summary>
        /// Draws a Rect
        /// </summary>
        public static void DrawRect(Rect rect, Color color, float t = 0)
        {
            Vector3 leftDown = new Vector3(rect.xMin, rect.yMin);
            Vector3 leftUp = new Vector3(rect.xMin, rect.yMax);
            Vector3 rightDown = new Vector3(rect.xMax, rect.yMin);
            Vector3 rightUp = new Vector3(rect.xMax, rect.yMax);
            Debug.DrawLine(leftDown, rightDown, color, t);
            Debug.DrawLine(leftUp, rightUp, color, t);
            Debug.DrawLine(leftDown, leftUp, color, t);
            Debug.DrawLine(rightDown, rightUp, color, t);
        }

        /// <summary>
        /// Draws a Circle
        /// </summary>
        public static void DrawCircle(Vector3 position, float radius, Color color, float duration = 0)
        {
            Vector3 up = Vector3.forward * radius;
            Vector3 forward = Vector3.Slerp(up, -up, 0.5f);
            Vector3 right = Vector3.Cross(up, forward).normalized * radius;

            Matrix4x4 matrix = new Matrix4x4
            {
                [0] = right.x,
                [1] = right.y,
                [2] = right.z,
                [4] = up.x,
                [5] = up.y,
                [6] = up.z,
                [8] = forward.x,
                [9] = forward.y,
                [10] = forward.z
            };

            Vector3 _lastPoint = position + matrix.MultiplyPoint3x4(new Vector3(Mathf.Cos(0), 0, Mathf.Sin(0)));
            Vector3 _nextPoint = Vector3.zero;

            for (var i = 0; i < 91; i++)
            {
                _nextPoint.x = Mathf.Cos((i * 4) * Mathf.Deg2Rad);
                _nextPoint.z = Mathf.Sin((i * 4) * Mathf.Deg2Rad);
                _nextPoint.y = 0;

                _nextPoint = position + matrix.MultiplyPoint3x4(_nextPoint);

                Debug.DrawLine(_lastPoint, _nextPoint, color, duration);
                _lastPoint = _nextPoint;
            }
        }
    }
}