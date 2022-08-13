using System.Collections;
using fidt17.UnityExtensionsModule.Runtime.ExtendedCoroutine;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace fidt17.PlayMode_Tests.ExtendedCoroutineTests
{
    public class CoroutineRunnerTests
    {
        [UnityTest]
        public IEnumerator TestCoroutineRunnerGetter()
        {
            var coroutineRunner = CoroutineRunner.Instance;
            Assert.That(coroutineRunner != null);
            CoroutineRunner.Destroy();
            yield break;
        }

        [UnityTest]
        public IEnumerator TestCoroutineRunnerDestroy()
        {
            var coroutineRunner = CoroutineRunner.Instance;
            CoroutineRunner.Destroy();
            Assert.That(coroutineRunner == null);
            yield break;
        }
    }
}