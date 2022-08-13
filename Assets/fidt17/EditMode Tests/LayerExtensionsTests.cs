using System;
using fidt17.UnityExtensionsModule.Runtime;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace fidt17.EditMode_Tests
{
    public class LayerExtensionsTests
    {
        [Test]
        public void ContainsTest()
        {
            LayerMask layerMask = LayerMask.GetMask("Default");
            Assert.That(!layerMask.Contains(LayerMask.NameToLayer("UI")));
            Assert.That(layerMask.Contains(LayerMask.NameToLayer("Default")));
        }

        [Test]
        public void SetLayerRecursiveTest()
        {
            var gObj = new GameObject();
            
            var firstEpochChild = new GameObject();
            firstEpochChild.transform.parent = gObj.transform;

            var secondEpochChild = new GameObject();
            secondEpochChild.transform.parent = firstEpochChild.transform;
            
            gObj.SetLayerRecursive(LayerMask.NameToLayer("UI"));
            Assert.That(() => gObj.layer == LayerMask.NameToLayer("UI"));
            Assert.That(() => firstEpochChild.layer == LayerMask.NameToLayer("UI"));
            Assert.That(() => secondEpochChild.layer == LayerMask.NameToLayer("UI"));
            
            Object.DestroyImmediate(gObj);
        }

        [Test]
        public void SetLayerRecursiveArgsTest()
        {
            var nullObj = new GameObject();
            Object.DestroyImmediate(nullObj);
            Assert.Throws<ArgumentNullException>(() => nullObj.SetLayerRecursive(LayerMask.NameToLayer("UI")));
        }
    }
}
