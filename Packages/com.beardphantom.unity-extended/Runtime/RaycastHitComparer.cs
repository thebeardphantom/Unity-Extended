using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public class RaycastHitComparer : PhysicsRaycastComparer<RaycastHitComparer, RaycastHit>
    {
        public override int Compare(RaycastHit x, RaycastHit y)
        {
            return x.distance.CompareTo(y.distance);
        }
    }
}