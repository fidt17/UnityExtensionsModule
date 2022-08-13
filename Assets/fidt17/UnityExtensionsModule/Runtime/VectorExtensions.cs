using UnityEngine;

namespace fidt17.UnityExtensionsModule.Runtime
{
    public static class VectorExtensions
    {
        /// <summary>
        /// Returns a new Vector2 that lies inside a circle of <paramref name="radius"/> with center in <paramref name="origin"/>
        /// </summary>
        public static Vector2 RandomOffset(Vector2 origin, float radius)
        {
            origin.x += Random.Range(-radius, radius);
            origin.y += Random.Range(-radius, radius);
            return origin;
        }

        /// <summary>
        /// Returns a new Vector3 that lies inside a sphere of <paramref name="radius"/> with center in <paramref name="origin"/>
        /// </summary>
        public static Vector3 RandomOffset(Vector3 origin, float radius)
        {
            origin.x += Random.Range(-radius, radius);
            origin.y += Random.Range(-radius, radius);
            origin.z += Random.Range(-radius, radius);
            return origin;
        }
    }
}