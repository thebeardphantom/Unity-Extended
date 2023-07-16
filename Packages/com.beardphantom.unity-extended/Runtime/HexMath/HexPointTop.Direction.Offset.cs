using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static partial class HexPointTop
    {
        #region Fields

        private static readonly Vector3Int[] _evenRowOffsets =
        {
            // Up Left
            new(-1, -1),
            // Up Right
            new(0, -1),
            // Right
            new(1, 0),
            // Down Right
            new(0, 1),
            // Down Left
            new(-1, 1),
            // Left
            new(-1, 0)
        };

        private static readonly Vector3Int[] _oddRowOffsets =
        {
            // Up Left
            new(0, -1),
            // Up Right
            new(1, -1),
            // Right
            new(1, 0),
            // Down Right
            new(1, 1),
            // Down Left
            new(0, 1),
            // Left
            new(-1, 0)
        };

        #endregion

        #region Methods

        public static Vector2Int GetNeighborOffset(this Vector2Int cell, Direction direction)
        {
            return GetNeighborOffset(cell.To3D(), direction).To2D();
        }

        public static Vector3Int GetNeighborOffset(this Vector3Int cell, Direction direction)
        {
            var isEvenRow = cell.y % 2 == 0;
            var offsets = isEvenRow ? _evenRowOffsets : _oddRowOffsets;
            var offset = offsets[(int)direction];
            return offset;
        }

        public static Vector2Int GetNeighborCell(this Vector2Int cell, Direction direction)
        {
            return GetNeighborCell(cell.To3D(), direction).To2D();
        }

        public static Vector3Int GetNeighborCell(this Vector3Int cell, Direction direction)
        {
            var offset = GetNeighborOffset(cell, direction);
            return cell + offset;
        }

        #endregion
    }
}