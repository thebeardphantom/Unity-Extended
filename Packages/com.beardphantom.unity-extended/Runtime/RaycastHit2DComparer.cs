using UnityEngine;

public class RaycastHit2DComparer : PhysicsRaycastComparer<RaycastHit2DComparer, RaycastHit2D>
{
    #region Methods

    public override int Compare(RaycastHit2D x, RaycastHit2D y)
    {
        return x.distance.CompareTo(y.distance);
    }

    #endregion
}