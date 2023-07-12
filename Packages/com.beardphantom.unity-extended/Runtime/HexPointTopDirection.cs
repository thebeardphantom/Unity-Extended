using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public enum HexPointTopDirection
    {
        UpRight = 0,
        Right = 1,
        DownRight = 2,
        DownLeft = 3,
        Left = 4,
        UpLeft = 5
    }

    /*
     * Unity uses odd-r for pointy-top hex grids and odd-q for flat top hex grids.
     * This class is only designed to work with pointy-top.
     *
     * Math from https://www.redblobgames.com/grids/hexagons/#neighbors-offset
     */
    public static class HexPointTopNeighborDirectionUtility
    {
        #region Fields

        private static readonly Vector3Int[] _evenRowOffsets =
        {
            // Up Right
            new(0, -1),
            // Right
            new(1, 0),
            // Down Right
            new(0, 1),
            // Down Left
            new(-1, 1),
            // Left
            new(-1, 0),
            // Up Left
            new(-1, -1)
        };

        private static readonly Vector3Int[] _oddRowOffsets =
        {
            // Up Right
            new(1, -1),
            // Right
            new(1, 0),
            // Down Right
            new(1, 1),
            // Down Left
            new(0, 1),
            // Left
            new(-1, 0),
            // Up Left
            new(0, -1)
        };

        #endregion

        #region Methods

        public static Vector2Int GetNeighborOffset(this Vector2Int cell, HexPointTopDirection direction)
        {
            return GetNeighborOffset(cell.To3D(), direction).To2D();
        }

        public static Vector3Int GetNeighborOffset(this Vector3Int cell, HexPointTopDirection direction)
        {
            var isEvenRow = cell.y % 2 == 0;
            var offsets = isEvenRow ? _evenRowOffsets : _oddRowOffsets;
            var offset = offsets[(int)direction];
            return offset;
        }

        public static Vector2Int GetNeighborCell(this Vector2Int cell, HexPointTopDirection direction)
        {
            return GetNeighborCell(cell.To3D(), direction).To2D();
        }

        public static Vector3Int GetNeighborCell(this Vector3Int cell, HexPointTopDirection direction)
        {
            var offset = GetNeighborOffset(cell, direction);
            return cell + offset;
        }

        #endregion
    }
}