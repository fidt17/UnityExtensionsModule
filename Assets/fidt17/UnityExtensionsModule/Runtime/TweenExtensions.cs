using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace fidt17.UnityExtensionsModule.Runtime
{
    public static class TweenExtensions
    {
        #region Progress
        
        /// <summary>
        /// Enumerator that last for <paramref name="duration"/> seconds.
        /// After each yield invokes <paramref name="progressAction"/> with float argument [0->1].
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
        /// /// Enumerator that last for <paramref name="duration"/> seconds.
        /// After each yield invokes <paramref name="progressAction"/> with float argument [1->0].
        /// Can specify yield function.
        /// </summary>
        public static IEnumerator ReverseProgress(float duration, Action<float> progressAction, Func<IEnumerator> yieldFunc = null)
        {
            yield return Progress(duration, f =>
            {
                progressAction?.Invoke(1 - f);
            }, yieldFunc);
        }

        #endregion
            
        #region Move
        
        /// <summary>
        /// Moves transform from <paramref name="startPosition"/> to <paramref name="targetPosition"/> in <paramref name="duration"/> seconds.
        /// </summary>
        public static IEnumerator Move(Transform transform, Vector3 startPosition, Vector3 targetPosition, float duration)
        {
            yield return Progress(duration, progress =>
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
            });
        }
        
        /// <summary>
        /// Moves transform  to <paramref name="targetPosition"/> in <paramref name="duration"/> seconds.
        /// </summary>
        public static IEnumerator Move(Transform transform, Vector3 targetPosition, float duration)
        {
            yield return Move(transform, transform.position, targetPosition, duration);
        }
        
        #endregion

        #region Rotate

        /// <summary>
        /// Rotate transform from <paramref name="start"/> to <paramref name="target"/> in <paramref name="duration"/> seconds.
        /// </summary>
        public static IEnumerator Rotate(Transform transform, Quaternion start, Quaternion target, float duration)
        {
            yield return Progress(duration, progress =>
            {
                transform.rotation = Quaternion.Slerp(start, target, progress);
            });
        }
        
        /// <summary>
        /// Rotate transform  to <paramref name="target"/> in <paramref name="duration"/> seconds.
        /// </summary>
        public static IEnumerator Rotate(Transform transform, Quaternion target, float duration)
        {
            yield return Rotate(transform, transform.rotation, target, duration);
        }

        #endregion

        #region Scale
        
        /// <summary>
        /// Scales transform from <paramref name="start"/> to <paramref name="target"/> in <paramref name="duration"/> seconds.
        /// </summary>
        public static IEnumerator Scale(Transform transform, Vector3 start, Vector3 target, float duration)
        {
            yield return Progress(duration, progress =>
            {
                transform.localScale = Vector3.Lerp(start, target, progress);
            });
        }
        
        /// <summary>
        /// Scales transform from to <paramref name="target"/> in <paramref name="duration"/> seconds.
        /// </summary>
        public static IEnumerator Scale(Transform transform, Vector3 target, float duration)
        {
            yield return Scale(transform, transform.localScale, target, duration);
        }

        /// <summary>
        /// Scales transform in Y axis to to <paramref name="target"/> in <paramref name="duration"/> seconds.
        /// </summary>
        public static IEnumerator ScaleY(Transform transform, float target, float duration)
        {
            var start = transform.localScale;
            yield return Progress(duration, progress =>
            {
                transform.localScale = new Vector3(start.x, Mathf.Lerp(start.y, target, progress), start.z);
            });
        }
        
        #endregion
        
        #region Color

        public static IEnumerator Color(Color startColor, Color endColor, float duration, Action<Color> progressAction)
        {
            yield return Progress(duration, f =>
            {
                progressAction?.Invoke(UnityEngine.Color.Lerp(startColor, endColor, f));
            });
        }

        public static IEnumerator Color(SpriteRenderer spriteRenderer, Color targetColor, float duration)
        {
            yield return Color(spriteRenderer, spriteRenderer.color, targetColor, duration);
        }
        
        public static IEnumerator Color(SpriteRenderer spriteRenderer, Color startColor, Color targetColor, float duration)
        {
            yield return Color(startColor, targetColor, duration, color =>
            {
                spriteRenderer.color = color;
            });
        }

        public static IEnumerator Color(Image image, Color targetColor, float duration)
        {
            yield return Color(image, image.color, targetColor, duration);
        }
        
        public static IEnumerator Color(Image image, Color startColor, Color targetColor, float duration)
        {
            yield return Color(startColor, targetColor, duration, color =>
            {
                image.color = color;
            });
        }

        #endregion
    }
}