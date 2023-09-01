using UnityEngine;

public class RaycastHitComparer : PhysicsRaycastComparer<RaycastHitComparer, RaycastHit>
{
    #region Methods

    public override int Compare(RaycastHit x, RaycastHit y)
    {
        return x.distance.CompareTo(y.distance);
    }

    #endregion
}