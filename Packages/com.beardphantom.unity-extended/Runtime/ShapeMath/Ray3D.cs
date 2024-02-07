using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public readonly struct Ray3D
    {
        #region Fields

        internal readonly Ray Ray;

        #endregion

        #region Constructors

        public Ray3D(Ray ray)
        {
            Ray = ray;
        }

        public Ray3D(Vector3 origin, Vector3 direction)
        {
            Ray = new Ray(origin, direction);
        }

        #endregion

        #region Methods

        public bool Overlaps(in AABB aabb, out float distanceToOverlap)
        {
            return aabb.Overlaps(this, out distanceToOverlap);
        }

        public bool Overlaps(in Sphere sphere, out float distanceToOverlap)
        {
            var vectorToSphereCenter = sphere.Center - Ray.origin;
            var distanceFromRayOriginToRayToSphereCenter = Vector3.Dot(vectorToSphereCenter, Ray.direction);
            var distanceFromRayToSphereCenterSqr = Vector3.Dot(vectorToSphereCenter, vectorToSphereCenter)
                - distanceFromRayOriginToRayToSphereCenter * distanceFromRayOriginToRayToSphereCenter;
            var sphereRadiusSqr = sphere.Radius * sphere.Radius;

            if (distanceFromRayToSphereCenterSqr > sphereRadiusSqr)
            {
                distanceToOverlap = -1;
                return false;
            }

            var distanceFromRayToSphereIntersection = Mathf.Sqrt(sphereRadiusSqr - distanceFromRayToSphereCenterSqr);
            var possibleIntersection0 = distanceFromRayOriginToRayToSphereCenter - distanceFromRayToSphereIntersection;
            var possibleIntersection1 = distanceFromRayOriginToRayToSphereCenter + distanceFromRayToSphereIntersection;

            if (possibleIntersection0 > possibleIntersection1)
            {
                (possibleIntersection0, possibleIntersection1) = (possibleIntersection1, possibleIntersection0);
            }

            if (possibleIntersection0 < 0)
            {
                possibleIntersection0 = possibleIntersection1;

                if (possibleIntersection0 < 0)
                {
                    distanceToOverlap = -1;
                    return false;
                }
            }

            distanceToOverlap = possibleIntersection0;
            return true;
        }

        #endregion
    }
}