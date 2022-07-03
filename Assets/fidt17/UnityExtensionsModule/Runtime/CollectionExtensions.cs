using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace fidt17.UnityExtensionsModule.Runtime
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Returns random element from provided List
        /// </summary>
        public static T GetRandomElement<T>(this List<T> list)
        {
            if (list == null) throw new ArgumentNullException();
            if (list.Count == 0) throw new ArgumentException("List is empty");

            return list[Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Returns random element from provided array
        /// </summary>
        public static T GetRandomElement<T>(this T[] array)
        {
            if (array == null) throw new ArgumentNullException();
            if (array.Length == 0) throw new ArgumentException("Array is empty");
            
            return array[Random.Range(0, array.Length)];   
        }
        
        /// <summary>
        /// Shuffles a list
        /// </summary>
        public static void Shuffle<T>(this List<T> list)
        {
            if (list == null) throw new ArgumentNullException();
            
            var rng = new System.Random();
            int n = list.Count;  
            while (n > 1)
            {  
                n--;  
                int k = rng.Next(n + 1);  
                (list[k], list[n]) = (list[n], list[k]);
            }  
        }

        /// <summary>
        /// Shuffles an array
        /// </summary>
        public static void Shuffle<T>(this T[] array)
        {
            if (array == null) throw new ArgumentNullException();
            
            var rng = new System.Random();
            int n = array.Length;  
            while (n > 1)
            {  
                n--;  
                int k = rng.Next(n + 1);  
                (array[k], array[n]) = (array[n], array[k]);
            }  
        }
    }
}