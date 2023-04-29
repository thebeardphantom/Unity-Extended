using BeardPhantom.UnityExtended.ShapeMath;
using NUnit.Framework;
using UnityEngine;

namespace BeardPhantom.UnityExtended.Tests.Editor.ShapeMath
{
    public class AABBTests
    {
        #region Methods

        [Test]
        public void OverlapAABB_ReturnsFalse()
        {
            var a = AABB.FromCenterAndSize(Vector3.zero, Vector3.one * 0.25f);
            var b = AABB.FromCenterAndSize(Vector3.one, Vector3.one * 0.25f);
            Assert.IsFalse(a.Overlaps(b), "a.Overlaps(b)");
        }

        [Test]
        public void OverlapAABB_ReturnsTrue()
        {
            var a = AABB.FromCenterAndSize(Vector3.zero, Vector3.one * 1.5f);
            var b = AABB.FromCenterAndSize(Vector3.one, Vector3.one);
            Assert.IsTrue(a.Overlaps(b), "a.Overlaps(b)");
        }

        [Test]
        public void OverlapPoint_ReturnsFalse()
        {
            var a = AABB.FromCenterAndSize(Vector3.zero, Vector3.one);
            var b = new Point3D(Vector3.one);
            Assert.IsFalse(a.Overlaps(b), "a.Overlaps(b)");
        }

        [Test]
        public void OverlapPoint_ReturnsTrue()
        {
            var a = AABB.FromCenterAndExtents(Vector3.zero, Vector3.one);
            var b = new Point3D(Vector3.one);
            Assert.IsTrue(a.Overlaps(b), "a.Overlaps(b)");
        }

        [Test]
        public void OverlapRay3D_ReturnsFalse()
        {
            var a = AABB.FromCenterAndSize(Vector3.zero, Vector3.one * 0.25f);
            var b = new Ray3D(Vector3.back, Vector3.right);
            Assert.IsFalse(a.Overlaps(b, out _), "a.Overlaps(b)");
        }

        [Test]
        public void OverlapRay3D_ReturnsTrue()
        {
            var a = AABB.FromCenterAndSize(Vector3.zero, Vector3.one * 0.25f);
            var b = new Ray3D(Vector3.back, Vector3.forward);
            Assert.IsTrue(a.Overlaps(b, out _), "a.Overlaps(b)");
        }

        [Test]
        public void OverlapSphere_ReturnsFalse()
        {
            var a = AABB.FromCenterAndSize(Vector3.zero, Vector3.one);
            var b = new Sphere(Vector3.one, 0.25f);
            Assert.IsFalse(a.Overlaps(b), "a.Overlaps(b)");
        }

        [Test]
        public void OverlapSphere_ReturnsTrue()
        {
            var a = AABB.FromCenterAndSize(Vector3.zero, Vector3.one);
            var b = new Sphere(Vector3.one, 1f);
            Assert.IsTrue(a.Overlaps(b), "a.Overlaps(b)");
        }

        #endregion
    }
}