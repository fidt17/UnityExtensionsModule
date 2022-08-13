using System;
using System.Collections;
using UnityEngine;

namespace fidt17.UnityExtensionsModule.Runtime.ExtendedCoroutine
{
    public static class CoroutineExtensions
    {
        /// <summary>
        /// Schedule an <paramref name="action"/> to execute on complete.
        /// </summary>
        public static IEnumerator SetOnComplete(this IEnumerator enumerator, Action action)
        {
            yield return enumerator;
            action?.Invoke();
        }

        /// <summary>
        /// Skip <paramref name="N"/> amount of frames
        /// </summary>
        public static IEnumerator WaitForFrames(int N)
        {
            for (int i = 0; i < N; i++)
            {
                yield return null;
            }
        }

        /// <summary>
        /// Executes <paramref name="action"/> after <paramref name="seconds"/>.
        /// </summary>
        public static IEnumerator Delay(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);
            action?.Invoke();
        }
    }
}