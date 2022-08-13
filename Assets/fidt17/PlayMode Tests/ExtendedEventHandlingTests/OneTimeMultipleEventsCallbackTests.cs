using System;
using System.Collections;
using System.Collections.Generic;
using fidt17.UnityExtensionsModule.Runtime.ExtendedEventHandling;
using NUnit.Framework;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace fidt17.PlayMode_Tests.ExtendedEventHandlingTests
{
    public class OneTimeMultipleEventsCallbackTests
    {
        [UnityTest]
        public IEnumerator TestConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OneTimeMultipleEventsCallback(null, null);
            });
            
            Assert.Throws<ArgumentException>(() =>
            {
                new OneTimeMultipleEventsCallback(Array.Empty<WaitForEvent>(), null);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                new OneTimeMultipleEventsCallback(new WaitForEvent[3], null);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                var l = new List<WaitForEvent>
                {
                    new WaitForEvent(new UnityEvent())
                };

                new OneTimeMultipleEventsCallback(l, null);
            });
            
            Assert.DoesNotThrow(() =>
            {
                var l = new List<WaitForEvent>
                {
                    new WaitForEvent(new UnityEvent())
                };

                new OneTimeMultipleEventsCallback(l, l.Clear);
            });
            yield break;
        }

        [UnityTest]
        public IEnumerator TestAll()
        {
            var unityEventA = new UnityEvent();
            var unityEventB = new UnityEvent();

            int a = 0;
            new OneTimeMultipleEventsCallback(new[]
            {
                new WaitForEvent(unityEventA),
                new WaitForEvent(unityEventB)
            },
                () => a += 1,
                OneTimeMultipleEventsCallback.ListenMethod.All);
            
            unityEventA.Invoke();
            Assert.AreEqual(a, 0);
            unityEventB.Invoke();
            Assert.AreEqual(a, 1);
            
            yield break;
        }

        [UnityTest]
        public IEnumerator TestAny()
        {
            var unityEventA = new UnityEvent();
            var unityEventB = new UnityEvent();

            int a = 0;
            new OneTimeMultipleEventsCallback(new[]
                {
                    new WaitForEvent(unityEventA),
                    new WaitForEvent(unityEventB)
                },
                () => a += 1,
                OneTimeMultipleEventsCallback.ListenMethod.Any);
            
            unityEventA.Invoke();
            Assert.AreEqual(a, 1);
            unityEventB.Invoke();
            Assert.AreEqual(a, 1);
            
            yield break;
        }
    }
}