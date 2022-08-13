using System;
using System.Collections;
using fidt17.UnityExtensionsModule.Runtime.ExtendedEventHandling;
using NUnit.Framework;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace fidt17.PlayMode_Tests.ExtendedEventHandlingTests
{
    public class OneTimeEventCallbackTests
    {
        [UnityTest]
        public IEnumerator TestOneTimeEventCallbackConstructor()
        {
            var unityEvent = new UnityEvent();
            
            Assert.Throws<ArgumentNullException>(() =>
            {
                UnityEvent uEvent = null;
                new OneTimeEventCallback(uEvent, null);
            });
            
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OneTimeEventCallback(new WaitForEvent(unityEvent), null);
            });

            Assert.DoesNotThrow(() =>
            {
                new OneTimeEventCallback(new WaitForEvent(unityEvent), () =>
                {
                    int a;
                });
            });
            
            yield break;
        }

        [UnityTest]
        public IEnumerator TestOneTimeEventCallback()
        {
            var unityEvent = new UnityEvent();
            var b = false;

            new OneTimeEventCallback(unityEvent, () => b = true);
            unityEvent.Invoke();
            Assert.AreEqual(b, true);
            yield break;
        }
    }
}