using System;
using UnityEngine;

namespace fidt17.UnityExtensionsModule.Runtime
{
    public static class LayerExtensions
    {
        /// <summary>
        /// Checks if <paramref name="mask"/> contains <paramref name="layer"/> 
        /// </summary>
        public static bool Contains(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }

        /// <summary>
        /// Sets layer of all GameObjects down the hierarchy (includes root) to <paramref name="layer"/>.
        /// </summary>
        public static void SetLayerRecursive(this GameObject gObj, int layer)
        {
            if (gObj == null) throw new ArgumentNullException();
            
            gObj.layer = layer;
            foreach (Transform child in gObj.transform)
            {
                child.gameObject.SetLayerRecursive(layer);
            }
        }
    }
}
