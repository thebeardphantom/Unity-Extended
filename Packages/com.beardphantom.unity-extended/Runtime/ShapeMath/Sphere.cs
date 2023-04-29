using UnityEngine;

namespace BeardPhantom.UnityExtended.ShapeMath
{
    public readonly struct Sphere
    {
        #region Fields

        public readonly Vector3 Center;

        public readonly float Radius;

        #endregion

        #region Constructors

        public Sphere(Vector3 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        #endregion

        #region Methods

        public bool Overlaps(in AABB aabb)
        {
            return aabb.Overlaps(this);
        }

        public bool Overlaps(in Point3D point3D)
        {
            var distanceSqr = Vector3.SqrMagnitude(Center - point3D.Position);
            var radiiSqr = Radius * Radius;
            radiiSqr *= radiiSqr;
            return distanceSqr <= radiiSqr;
        }

        public bool Overlaps(in Ray3D ray3D, out float distanceToOverlap)
        {
            return ray3D.Overlaps(this, out distanceToOverlap);
        }

        public bool Overlaps(in Sphere other)
        {
            var distanceSqr = Vector3.SqrMagnitude(Center - other.Center);
            var radiiSqr = Radius + other.Radius;
            radiiSqr *= radiiSqr;
            return distanceSqr <= radiiSqr;
        }

        #endregion
    }
}