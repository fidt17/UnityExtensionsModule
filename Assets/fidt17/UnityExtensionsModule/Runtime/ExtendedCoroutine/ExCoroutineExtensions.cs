using System.Collections;
using UnityEngine;

namespace fidt17.UnityExtensionsModule.Runtime.ExtendedCoroutine
{
    public static class ExCoroutineExtensions
    {
        public static ExCoroutine StartExCoroutine(this MonoBehaviour owner, IEnumerator enumerator)
        {
            var exCoroutine = new ExCoroutine(owner);
            exCoroutine.Start(enumerator);
            return exCoroutine;
        }

        public static WaitForExCoroutine WaitFor(this ExCoroutine exCoroutine)
        {
            return new WaitForExCoroutine(exCoroutine);   
        }
    }
}