using System;
using System.Collections;
using fidt17.UnityExtensionsModule.Runtime.ExtendedCoroutine;
using fidt17.UnityExtensionsModule.Runtime.ExtendedEventHandling;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace fidt17.PlayMode_Tests.ExtendedEventHandlingTests
{
    public class WaitForEventTests
    {
        [UnityTest]
        public IEnumerator TestConstructorOnUnityEvent()
        {
            UnityEvent unityEvent = null;
            Assert.Throws<ArgumentNullException>(() => new WaitForEvent(unityEvent));

            unityEvent = new UnityEvent();
            Assert.DoesNotThrow(() => new WaitForEvent(unityEvent));
            
            yield break;
        }

        [UnityTest]
        public IEnumerator TestConstructorOnAction()
        {
            void foo()
            {
            }

            Assert.Throws<ArgumentNullException>(() => new WaitForEvent(null, null));
            Assert.Throws<ArgumentNullException>(() => new WaitForEvent(w => foo(), null));
            Assert.Throws<ArgumentNullException>(() => new WaitForEvent(null, w => foo()));
            Assert.DoesNotThrow(() => new WaitForEvent(w => foo(), w => foo()));

            yield break;
        }

        [UnityTest]
        public IEnumerator TestWaitForEventOnUnityEvent()
        {
            var unityEvent = new UnityEvent();
            var w = new WaitForEvent(unityEvent);

            bool onTriggerB = false;
            w.OnTrigger += () => onTriggerB = true;
            
            unityEvent.Invoke();
            
            Assert.AreEqual(onTriggerB, true);
            Assert.AreEqual(w.HasBeenTriggered, true);
            Assert.That(() => unityEvent.GetPersistentEventCount() == 0);
            yield break;
        }

        private event Action _myEvent;
        [UnityTest]
        public IEnumerator TestWaitForEventOnAction()
        {
            var w = new WaitForEvent(w => _myEvent += w.OnEvent, w => _myEvent -= w.OnEvent);
            
            bool onTriggerB = false;
            w.OnTrigger += () => onTriggerB = true;
            
            _myEvent.Invoke();
            
            Assert.AreEqual(onTriggerB, true);
            Assert.AreEqual(w.HasBeenTriggered, true);
            Assert.That(_myEvent == null);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestWaitForEventUnsubscribe()
        {
            var unityEvent = new UnityEvent();
            var w = new WaitForEvent(unityEvent);
            w.Unsubscribe();
            unityEvent.Invoke();
            Assert.That(w.HasBeenTriggered == false);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestWaitForEventAsYieldInstruction()
        {
            var unityEvent = new UnityEvent();
            var w = new WaitForEvent(unityEvent);

            CoroutineRunner.Instance.StartCoroutine(CoroutineExtensions.Delay(1, unityEvent.Invoke));
            yield return w;
            CoroutineRunner.Destroy();
            
            Assert.That(w.HasBeenTriggered);
        }
    }
}