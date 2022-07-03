using System;
using System.Collections.Generic;
using fidt17.UnityExtensionsModule.Runtime;
using NUnit.Framework;

namespace fidt17.UnityExtensionsModule.Tests
{
    public class CollectionExtensionsTests
    {
        [Test]
        public void GetRandomElementFromListTest()
        {
            var l = new List<int>();
            Assert.Throws<ArgumentException>(() => l.GetRandomElement());
            
            l.Add(1);
            Assert.That(l.GetRandomElement() == 1);
            
            l = null;
            Assert.Throws<ArgumentNullException>(() => l.GetRandomElement());
        }

        [Test]
        public void GetRandomElementFromArrayTest()
        {
            var arr = Array.Empty<int>();
            Assert.Throws<ArgumentException>(() => arr.GetRandomElement());

            arr = new int[1];
            arr[0] = 1;
            Assert.That(arr.GetRandomElement() == 1);

            arr = null;
            Assert.Throws<ArgumentNullException>(() => arr.GetRandomElement());
        }

        [Test]
        public void ShuffleListTest()
        {
            var l = new List<int>();
            l = null;
            
            Assert.Throws<ArgumentNullException>(() => l.Shuffle());
        }

        [Test]
        public void ShuffleArrayTest()
        {
            var arr = new int[1];
            arr = null;

            Assert.Throws<ArgumentNullException>(() => arr.Shuffle());
        }
    }
}