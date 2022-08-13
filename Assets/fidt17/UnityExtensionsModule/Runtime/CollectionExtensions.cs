using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace fidt17.UnityExtensionsModule.Runtime
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Returns one random element from <paramref name="collection"/>
        /// </summary>
        public static T SampleOne<T>(this IReadOnlyList<T> collection)
        {
            if (collection == null) throw new ArgumentNullException();
            if (collection.Count == 0) throw new ArgumentException("Collection is empty");

            return collection[Random.Range(0, collection.Count)];
        }

        /// <summary>
        /// Yields <paramref name="N"/> random elements.
        /// </summary>
        public static IEnumerable<T> Sample<T>(this IReadOnlyList<T> collection, int N)
        {
            if (collection == null) throw new ArgumentNullException();
            if (collection.Count == 0) throw new ArgumentException("Collection is empty");

            for (int i = 0; i < N; i++)
            {
                yield return collection.SampleOne();
            }
        }

        /// <summary>
        /// Yields a maximum of <paramref name="N"/> unique random elements.
        /// </summary>
        public static IEnumerable<T> SampleUnique<T>(this IReadOnlyList<T> collection, int N)
        {
            if (collection == null) throw new ArgumentNullException();
            if (collection.Count == 0) throw new ArgumentException("Collection is empty");

            N = Mathf.Min(collection.Count, N);
            float req = N;
            float left = collection.Count;
            
            for (int i = 0; i < N;)
            {
                if (Random.value <= req / left)
                {
                    yield return collection[i];
                    req--;
                    i++;
                }
                left--;
            }
        }

        /// <summary>
        /// Shuffles a <paramref name="collection"/>
        /// </summary>
        public static void Shuffle<T>(this IList<T> collection)
        {
            if (collection == null) throw new ArgumentNullException();
            
            var rng = new System.Random();
            int n = collection.Count;  
            while (n > 1)
            {  
                n--;  
                int k = rng.Next(n + 1);  
                (collection[k], collection[n]) = (collection[n], collection[k]);
            }  
        }
    }
}