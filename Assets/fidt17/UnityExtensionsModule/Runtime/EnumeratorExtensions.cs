using System;
using System.Collections;
using UnityEngine;

namespace fidt17.UnityExtensionsModule.Runtime
{
    public static class EnumeratorExtensions
    {
        /// <summary>
        /// Schedule an action to execute on complete.
        /// </summary>
        public static IEnumerator SetOnComplete(this IEnumerator enumerator, Action action)
        {
            yield return enumerator;
            action?.Invoke();
        }
        
        /// <summary>
        /// Enumerator that last for *DURATION* seconds.
        /// After each yield calls *PROGRESS_ACTION* with progress input (0 -> 1).
        /// Can specify yield function.
        /// </summary>
        public static IEnumerator Progress(float duration, Action<float> progressAction, Func<IEnumerator> yieldFunc = null)
        {
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                var progress = t / duration;
                progressAction?.Invoke(progress);
                yield return yieldFunc;
            }
            progressAction?.Invoke(1);
        }

        /// <summary>
        /// Enumerator that last for *DURATION* seconds.
        /// After each yield calls *PROGRESS_ACTION* with progress input (1 -> 0).
        /// Can specify yield function.
        /// </summary>
        public static IEnumerator ReverseProgress(float duration, Action<float> progressAction, Func<IEnumerator> yieldFunc = null)
        {
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                var progress = t / duration;
                progressAction?.Invoke(1-progress);
                yield return yieldFunc;
            }
            progressAction?.Invoke(0);
        }
    }
}