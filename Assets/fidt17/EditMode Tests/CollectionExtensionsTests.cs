using System;
using System.Collections.Generic;
using System.Linq;
using fidt17.UnityExtensionsModule.Runtime;
using NUnit.Framework;

namespace fidt17.EditMode_Tests
{
    public class CollectionExtensionsTests
    {
        [Test]
        public void SampleOneTest()
        {
            var l = new List<int>();
            Assert.Throws<ArgumentException>(() => l.SampleOne());
            
            l.Add(1);
            Assert.That(l.SampleOne() == 1);
            
            l = null;
            Assert.Throws<ArgumentNullException>(() => l.SampleOne());
        }

        [Test]
        public void SampleTest()
        {
            var l = new List<int>();
            Assert.Throws<ArgumentException>(() => l.Sample(1).ToList());
            
            l.Add(1);
            l.Add(2);
            var r = l.Sample(2).ToList();
            Assert.That(r.Count == 2);

            l = null;
            Assert.Throws<ArgumentNullException>(() => l.Sample(1).ToList());
        }

        [Test]
        public void SampleUniqueTest()
        {
            var l = new List<int>();
            Assert.Throws<ArgumentException>(() => l.SampleUnique(1).ToList());

            const int count = 100;
            for (int i = 0; i < count; i++)
            {
                l.Add(i);
            }
            Assert.AreEqual(l.SampleUnique(count).Count(), count);
            Assert.AreEqual(l.SampleUnique(count - 1).Count(), count - 1);
            Assert.AreEqual(l.SampleUnique(count + 1).Count(), count);
            
            Assert.AreEqual(l.SampleUnique(count).Select(x => x).Distinct().Count(), count);
            Assert.AreEqual(l.SampleUnique(count - 1).Select(x => x).Distinct().Count(), count - 1);
            Assert.AreEqual(l.SampleUnique(count + 1).Select(x => x).Distinct().Count(), count);

            l = null;
            Assert.Throws<ArgumentNullException>(() => l.SampleUnique(1).ToList());
        }
        
        [Test]
        public void ShuffleListTest()
        {
            Assert.Throws<ArgumentNullException>(() => ((List<int>)null).Shuffle());
        }

        [Test]
        public void ShuffleArrayTest()
        {
            Assert.Throws<ArgumentNullException>(() => ((int[])null).Shuffle());
        }
    }
}