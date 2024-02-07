using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    [SuppressMessage("ReSharper", "PossiblyImpureMethodCallOnReadonlyVariable")]
    public readonly struct AABB
    {
        #region Fields

        internal readonly Bounds Bounds;

        #endregion

        #region Constructors

        public AABB(Bounds bounds)
        {
            Bounds = bounds;
        }

        #endregion

        #region Methods

        public static AABB FromMinMax(Vector3 min, Vector3 max)
        {
            return new AABB(
                new Bounds
                {
                    min = min,
                    max = max
                });
        }

        public static AABB FromCenterAndSize(Vector3 center, Vector3 size)
        {
            return new AABB(new Bounds(center, size));
        }

        public static AABB FromCenterAndExtents(Vector3 center, Vector3 extents)
        {
            return new AABB(
                new Bounds
                {
                    center = center,
                    extents = extents
                });
        }

        public bool Overlaps(in AABB other)
        {
            return Bounds.Intersects(other.Bounds);
        }

        public bool Overlaps(in Point3D point3D)
        {
            return Bounds.Contains(point3D.Position);
        }

        public bool Overlaps(in Ray3D ray3D, out float distanceToOverlap)
        {
            return Bounds.IntersectRay(ray3D.Ray, out distanceToOverlap);
        }

        public bool Overlaps(in Sphere sphere)
        {
            var closestPoint = Bounds.ClosestPoint(sphere.Center);
            var closestPoint3D = new Point3D(closestPoint);
            return sphere.Overlaps(closestPoint3D);
        }

        #endregion
    }
}