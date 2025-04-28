using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class GridExtensions
    {
        public static Vector2Int WorldToCell2D(this Grid grid, Vector2 position)
        {
            return grid.WorldToCell(position).To2D();
        }

        public static Vector2 GetCellCenterWorld2D(this Grid grid, Vector2Int cell)
        {
            return GetCellCenterWorld2D(grid, cell.To3D());
        }

        public static Vector2 GetCellCenterWorld2D(this Grid grid, Vector3Int cell3D)
        {
            return grid.GetCellCenterWorld(cell3D);
        }

        public static Vector2 WorldToCellCenterWorld2D(this Grid grid, Vector2 position)
        {
            return WorldToCellCenterWorld2D(grid, (Vector3)position);
        }

        public static Vector2 WorldToCellCenterWorld2D(this Grid grid, Vector3 position3D)
        {
            Vector3Int cell = grid.WorldToCell(position3D);
            return grid.GetCellCenterWorld2D(cell);
        }
    }
}