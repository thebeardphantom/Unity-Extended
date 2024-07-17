using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static partial class HexFlatTop
    {
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
            new(0, -1),
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
            new(1, -1),
        };

        public static Vector2Int GetNeighborOffset(this Vector2Int cell, Direction direction)
        {
            return GetNeighborOffset(cell.To3D(), direction).To2D();
        }

        public static Vector3Int GetNeighborOffset(this Vector3Int cell, Direction direction)
        {
            // Not a mistake, Unity swizzles cell coordinates for flat top grids
            var isEvenColumn = cell.y % 2 == 0;
            var offsets = isEvenColumn ? _evenColumnOffsets : _oddColumnOffsets;
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
    }
}