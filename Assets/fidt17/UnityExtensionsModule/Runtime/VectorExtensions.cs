using UnityEngine;

namespace fidt17.UnityExtensionsModule.Runtime
{
    public static class VectorExtensions
    {
        public static Vector2 RandomOffset(Vector2 v2, float range)
        {
            v2.x += Random.Range(-range, range);
            v2.y += Random.Range(-range, range);
            return v2;
        }

        public static Vector3 RandomOffset(Vector3 v3, float range)
        {
            v3.x += Random.Range(-range, range);
            v3.y += Random.Range(-range, range);
            v3.z += Random.Range(-range, range);
            return v3;
        }
    }
}