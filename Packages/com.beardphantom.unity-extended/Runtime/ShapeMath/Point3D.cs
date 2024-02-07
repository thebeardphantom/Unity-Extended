using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public readonly struct Point3D
    {
        #region Fields

        public readonly Vector3 Position;

        #endregion

        #region Constructors

        public Point3D(Vector3 position)
        {
            Position = position;
        }

        #endregion

        #region Methods

        public bool Overlaps(in Sphere sphere)
        {
            return sphere.Overlaps(this);
        }

        #endregion
    }
}