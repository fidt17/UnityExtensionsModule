using UnityEngine;

namespace fidt17.UnityExtensionsModule.Runtime
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Generates random RGBA color with <paramref name="alpha"/>
        /// </summary>
        public static Color GenerateRandomColor(float alpha = 1)
        {
            var c = new Color(Random.value, Random.value, Random.value, alpha);
            return c;
        }
    }
}