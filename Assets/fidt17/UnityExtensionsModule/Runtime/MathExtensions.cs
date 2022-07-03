namespace fidt17.UnityExtensionsModule.Runtime
{
    public static class MathExtensions
    {
        /// <summary>
        /// Same as default Mathf.Sign(), but returns 0 if value is 0
        /// </summary>
        public static float AdvSign(float value)
        {
            return value switch
            {
                > 0 => 1,
                < 0 => -1,
                _ => 0
            };
        }
        
        public static float Remap(float value, float low1, float high1, float low2, float high2)
        {
            return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
        }
    }
}