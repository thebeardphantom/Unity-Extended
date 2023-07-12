using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public enum HexFlatTopDirection
    {
        Up = 0,
        UpRight = 1,
        DownRight = 2,
        Down = 3,
        DownLeft = 4,
        UpLeft = 5
    }

    /*
     * Unity uses odd-r for pointy-top hex grids and odd-q for flat top hex grids.
     * This class is only designed to work with flat-top.
     *
     * Math from https://www.redblobgames.com/grids/hexagons/#neighbors-offset
     */
    public static class HexFlatTopDirectionUtility
    {
        #region Fields

        private static readonly Vector3Int[] _evenColumnOffsets =
        {
            // Up
            new(1, 0),
            // Up Right
            new(0, 1),
            // Down Right
            new(-1, 1),
            // Down
            new(-1, 0),
            // Down Left
            new(-1, -1),
            // Up Left
            new(0, -1)
        };

        private static readonly Vector3Int[] _oddColumnOffsets =
        {
            // Up
            new(1, 0),
            // Up Right
            new(1, 1),
            // Down Right
            new(0, 1),
            // Down
            new(-1, 0),
            // Down Left
            new(0, -1),
            // Up Left
            new(1, -1)
        };

        #endregion

        #region Methods

        public static Vector2Int GetNeighborOffset(this Vector2Int cell, HexFlatTopDirection direction)
        {
            return GetNeighborOffset(cell.To3D(), direction).To2D();
        }

        public static Vector3Int GetNeighborOffset(this Vector3Int cell, HexFlatTopDirection direction)
        {
            var isEvenColumn = cell.y % 2 == 0;
            var offsets = isEvenColumn ? _evenColumnOffsets : _oddColumnOffsets;
            var offset = offsets[(int)direction];
            return offset;
        }

        public static Vector2Int GetNeighborCell(this Vector2Int cell, HexFlatTopDirection direction)
        {
            return GetNeighborCell(cell.To3D(), direction).To2D();
        }

        public static Vector3Int GetNeighborCell(this Vector3Int cell, HexFlatTopDirection direction)
        {
            var offset = GetNeighborOffset(cell, direction);
            return cell + offset;
        }

        #endregion
    }
}