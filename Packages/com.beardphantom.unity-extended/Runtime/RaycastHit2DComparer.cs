using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public class RaycastHit2DComparer : PhysicsRaycastComparer<RaycastHit2DComparer, RaycastHit2D>
    {
        public override int Compare(RaycastHit2D x, RaycastHit2D y)
        {
            return x.distance.CompareTo(y.distance);
        }
    }
}