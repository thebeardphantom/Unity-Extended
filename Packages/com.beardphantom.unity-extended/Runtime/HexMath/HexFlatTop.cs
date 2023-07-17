using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Unity uses odd-r for pointy-top hex grids and odd-q for flat top hex grids.
    /// This class is only designed to work with flat-top.
    /// Math from https://www.redblobgames.com/grids/hexagons/#neighbors-offset
    /// </summary>
    public static partial class HexFlatTop
    {
        #region Methods

        public static Vector2Int CubeToCellFlat(this CubeCoord cubeCoord)
        {
            var col = cubeCoord.Q;
            var row = cubeCoord.R + (cubeCoord.Q - (cubeCoord.Q & 1)) / 2;
            return new Vector2Int(row, col);
        }

        public static CubeCoord CellToCubeFlat(this Vector2Int cell)
        {
            var cellSwizzled = new Vector2Int(cell.y, cell.x);
            var q = cellSwizzled.x;
            var r = cellSwizzled.y - (cellSwizzled.x - (cellSwizzled.x & 1)) / 2;
            return new CubeCoord(q, r, -q - r);
        }

        public static int CellToIndex(this Vector3Int cell, BoundsInt cellBounds)
        {
            return CellToIndex(cell.To2D(), cellBounds);
        }

        public static int CellToIndex(this Vector2Int cell, BoundsInt cellBounds)
        {
            var offset = cellBounds.position.To2D().Abs();
            cell += offset;
            return cell.x + cell.y * cellBounds.size.x;
        }

        public static Vector2Int IndexToCell(int index, BoundsInt cellBounds)
        {
            var size = cellBounds.size.To2D();
            var cell = new Vector2Int(index % size.x, index / size.x);
            var offset = cellBounds.position.To2D().Abs();
            cell -= offset;
            return cell;
        }

        #endregion
    }
}