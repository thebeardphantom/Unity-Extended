using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class GridExtensions
    {
        #region Methods

        public static Vector2 GetCellCenterWorld2D(this Grid grid, Vector2Int position)
        {
            return GetCellCenterWorld2D(grid, position.To3D());
        }

        public static Vector2 GetCellCenterWorld2D(this Grid grid, Vector3Int position)
        {
            return grid.GetCellCenterWorld(position);
        }

        #endregion
    }
}