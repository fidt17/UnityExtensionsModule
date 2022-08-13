using System.Collections;
using fidt17.UnityExtensionsModule.Runtime.ExtendedCoroutine;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace fidt17.PlayMode_Tests.ExtendedCoroutineTests
{
    public class CoroutineExtensionsTests
    {
        [UnityTest]
        public IEnumerator TestSetOnCompleteOnIEnumerator()
        {
            IEnumerator ie()
            {
                yield break;
            }
            
            bool b = false;
            yield return ie().SetOnComplete(() => b = true);
            Assert.That(b == true);
        }

        [UnityTest]
        public IEnumerator TestMultipleSetOnCompleteOnIEnumerator()
        {
            IEnumerator ie()
            {
                yield break;
            }

            int counter = 0;
            yield return ie().SetOnComplete(() => counter++).SetOnComplete(() => counter++);
            Assert.That(counter == 2);
        }
        
        [UnityTest]
        public IEnumerator TestWaitForFrames()
        {
            var counter = 0;
            IEnumerator ie()
            {
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                
                counter++;
            }

            CoroutineRunner.Instance.StartCoroutine(ie());

            yield return CoroutineExtensions.WaitForFrames(3);
            Assert.AreEqual(counter, 0);
            yield return CoroutineExtensions.WaitForFrames(2);
            Assert.AreEqual(counter, 1);
            
            CoroutineRunner.Destroy();
        }

        [UnityTest]
        public IEnumerator TestDelay()
        {
            var b = false;
            CoroutineRunner.Instance.StartCoroutine(CoroutineExtensions.Delay(0.5f, () => b = true));
            yield return new WaitForSeconds(0.4f);
            Assert.AreEqual(b, false);
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(b, true);
            CoroutineRunner.Destroy();
        }
    }
}