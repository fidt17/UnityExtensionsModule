using System.Collections;
using UnityEngine;

namespace fidt17.UnityExtensionsModule.Runtime
{
    public static class TweenExtensions
    {
        #region Move
        
        public static IEnumerator Move(Transform transform, Vector3 start, Vector3 target, float duration)
        {
            yield return EnumeratorExtensions.Progress(duration, progress =>
            {
                transform.position = Vector3.Lerp(start, target, progress);
            });
        }
        
        public static IEnumerator Move(Transform transform, Vector3 target, float duration)
        {
            var start = transform.position;
            yield return EnumeratorExtensions.Progress(duration, progress =>
            {
                transform.position = Vector3.Lerp(start, target, progress);
            });
        }
        
        #endregion

        #region Scale
        
        public static IEnumerator Scale(Transform transform, Vector3 start, Vector3 target, float duration)
        {
            yield return EnumeratorExtensions.Progress(duration, progress =>
            {
                transform.localScale = Vector3.Lerp(start, target, progress);
            });
        }
        
        public static IEnumerator Scale(Transform transform, Vector3 target, float duration)
        {
            var start = transform.localScale;
            yield return EnumeratorExtensions.Progress(duration, progress =>
            {
                transform.localScale = Vector3.Lerp(start, target, progress);
            });
        }

        public static IEnumerator ScaleY(Transform transform, float target, float duration)
        {
            var start = transform.localScale;
            yield return EnumeratorExtensions.Progress(duration, progress =>
            {
                transform.localScale = new Vector3(start.x, Mathf.Lerp(start.y, target, progress), start.z);
            });
        }
        
        #endregion
    }
}