namespace fidt17.UnityExtensionsModule.Runtime
{
    public static class MathExtensions
    {
        /// <summary>
        /// Same as default Mathf.Sign(), but returns 0 if value is 0
        /// </summary>
        public static float AdvSign(float value)
        {
            if (value > 0) return 1;
            if (value < 0) return -1;
            return 0;
        }
        
        /// <summary>
        /// Returns the result of a non-clamping linear remapping of a <paramref name="value"/>
        /// from source range [<paramref name="low1"/>, <paramref name="high1"/>]
        /// to the destination range [<paramref name="low2"/>, <paramref name="high2"/>].
        /// </summary>
        public static float Remap(float value, float low1, float high1, float low2, float high2)
        {
            return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
        }
    }
}