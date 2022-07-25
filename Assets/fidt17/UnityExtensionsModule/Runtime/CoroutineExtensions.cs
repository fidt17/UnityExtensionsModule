using System;
using System.Collections;
using UnityEngine;

namespace fidt17.UnityExtensionsModule.Runtime
{
    public static class CoroutineExtensions
    {
        /// <summary>
        /// Schedule an action to execute on complete.
        /// </summary>
        public static IEnumerator SetOnComplete(this IEnumerator enumerator, Action action)
        {
            yield return enumerator;
            action?.Invoke();
        }

        public static IEnumerator SetOnComplete(this Coroutine coroutine, Action action)
        {
            yield return coroutine;
            action?.Invoke();
        }
    }
}