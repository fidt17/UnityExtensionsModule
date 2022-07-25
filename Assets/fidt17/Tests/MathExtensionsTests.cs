using fidt17.UnityExtensionsModule.Runtime;
using NUnit.Framework;

namespace fidt17.UnityExtensionsModule.Tests
{
    public class MathExtensionsTests
    {
        [Test]
        public void AdvSignTest()
        {
            Assert.That(() => MathExtensions.AdvSign(2) == 1);
            Assert.That(() => MathExtensions.AdvSign(-2) == -1);
            Assert.That(() => MathExtensions.AdvSign(0) == 0);
        }

        [Test]
        public void RemapTest()
        {
            Assert.That(() => MathExtensions.Remap(0, 0, 1, 10, 20) == 10);
            Assert.That(() => MathExtensions.Remap(0.5f, 0, 1, 10, 20) == 15);
            Assert.That(() => MathExtensions.Remap(1, 0, 1, 10, 20) == 20);
        }
    }
}