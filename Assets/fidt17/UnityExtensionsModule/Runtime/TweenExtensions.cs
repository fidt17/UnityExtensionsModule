using System;
using System.Collections;
using UnityEngine;

namespace fidt17.UnityExtensionsModule.Runtime
{
    public static class TweenExtensions
    {
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
        
        #region Move
        
        public static IEnumerator Move(Transform transform, Vector3 start, Vector3 target, float duration)
        {
            yield return Progress(duration, progress =>
            {
                transform.position = Vector3.Lerp(start, target, progress);
            });
        }
        
        public static IEnumerator Move(Transform transform, Vector3 target, float duration)
        {
            var start = transform.position;
            yield return Progress(duration, progress =>
            {
                transform.position = Vector3.Lerp(start, target, progress);
            });
        }
        
        #endregion

        #region Scale
        
        public static IEnumerator Scale(Transform transform, Vector3 start, Vector3 target, float duration)
        {
            yield return Progress(duration, progress =>
            {
                transform.localScale = Vector3.Lerp(start, target, progress);
            });
        }
        
        public static IEnumerator Scale(Transform transform, Vector3 target, float duration)
        {
            var start = transform.localScale;
            yield return Progress(duration, progress =>
            {
                transform.localScale = Vector3.Lerp(start, target, progress);
            });
        }

        public static IEnumerator ScaleY(Transform transform, float target, float duration)
        {
            var start = transform.localScale;
            yield return Progress(duration, progress =>
            {
                transform.localScale = new Vector3(start.x, Mathf.Lerp(start.y, target, progress), start.z);
            });
        }
        
        #endregion
    }
}