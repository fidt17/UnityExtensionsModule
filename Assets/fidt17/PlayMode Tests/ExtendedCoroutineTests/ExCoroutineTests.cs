using System;
using System.Collections;
using System.Collections.Generic;
using fidt17.UnityExtensionsModule.Runtime.ExtendedCoroutine;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace fidt17.PlayMode_Tests.ExtendedCoroutineTests
{
    public class ExCoroutineTests
    {
        [UnityTest]
        public IEnumerator TestWaitForExCoroutine()
        {
            var counter = 0;
            IEnumerator ie()
            {
                counter++;
                yield break;
            }

            var coroutineRunner = CoroutineRunner.Instance;
            var exCoroutine = coroutineRunner.StartExCoroutine(ie());
            yield return new WaitForExCoroutine(exCoroutine);
            CoroutineRunner.Destroy();
            
            Assert.That(counter == 1);
        }
        
        [UnityTest]
        public IEnumerator TestExCoroutineConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var exCoroutine = new ExCoroutine(null);
            });
            
            Assert.DoesNotThrow(() =>
            {
                new ExCoroutine(CoroutineRunner.Instance);
                CoroutineRunner.Destroy();
            });
            
            yield break;
        }

        [UnityTest]
        public IEnumerator TestExCoroutineAddSingle()
        {
            var counter = 0;
            IEnumerator ie()
            {
                counter++;
                yield break;
            }

            var coroutineRunner = CoroutineRunner.Instance;
            var exCoroutine = new ExCoroutine(coroutineRunner);
            exCoroutine.Start(ie());
            
            while (exCoroutine.HasFinished == false)
            {
                yield return null;
            }

            CoroutineRunner.Destroy();
            
            Assert.That(counter == 1);
        }

        [UnityTest]
        public IEnumerator TestExCoroutineAddMultiple()
        {
            var exCoroutine = new ExCoroutine(CoroutineRunner.Instance);

            int counter = 0;
            IEnumerator ieAdd1()
            {
                counter++;
                yield break;
            }

            IEnumerator ieAdd2()
            {
                counter += 2;
                yield break;
            }

            exCoroutine.Start(new List<IEnumerator> {ieAdd1(), ieAdd2()}, ExCoroutine.ExecutionOrder.Sequential);

            while (exCoroutine.HasFinished == false)
            {
                yield return null;
            }
            
            Assert.That(counter == 3);

            CoroutineRunner.Destroy();
        }

        [UnityTest]
        public IEnumerator TestExCoroutineAddWithSetOnComplete()
        {
            int counter = 0;
            IEnumerator ie()
            {
                counter++;
                yield break;
            }
            
            var exCoroutine = new ExCoroutine(CoroutineRunner.Instance);
            exCoroutine.Start(ie().SetOnComplete(() => counter++));
            exCoroutine.SetOnStop(() => counter++);
            
            yield return exCoroutine.WaitFor();
            
            Assert.That(counter == 3);

            CoroutineRunner.Destroy();
        }

        [UnityTest]
        public IEnumerator TestExCoroutineStart()
        {
            int counter = 0;
            IEnumerator ie()
            {
                counter++;
                yield break;
            }
            
            var exCoroutine = new ExCoroutine(CoroutineRunner.Instance);
            exCoroutine.Start(ie());

            yield return exCoroutine.WaitFor();
            
            Assert.That(counter == 1);

            CoroutineRunner.Destroy();
        }

        [UnityTest]
        public IEnumerator TestExCoroutineRestart()
        {
            int counterA = 0;
            int counterB = 0;

            IEnumerator ieA()
            {
                for (int i = 0; i < 3; i++)
                {
                    counterA++;
                    yield return null;
                }
            }

            IEnumerator ieB()
            {
                for (int i = 0; i < 3; i++)
                {
                    counterB++;
                    yield return null;
                }
            }

            var exCoroutine = new ExCoroutine(CoroutineRunner.Instance);
            exCoroutine.Start(ieA());
            yield return null;
            Assert.That(counterA == 2);
            exCoroutine.Start(ieB());
            yield return null;
            yield return null;
            Assert.That(counterA == 2 && counterB == 3);
            
            CoroutineRunner.Destroy();
        }
    }
}