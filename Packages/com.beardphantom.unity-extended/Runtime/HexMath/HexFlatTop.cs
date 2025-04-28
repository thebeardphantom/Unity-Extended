using UnityEngine;

namespace BeardPhantom.UnityExtended.HexMath
{
    public static class HexFlatTop
    {
        private static readonly Vector3Int[] s_evenColumnOffsets =
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

        private static readonly Vector3Int[] s_oddColumnOffsets =
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

        public static Vector2Int GetNeighborOffsetFlat(this Vector2Int cell, Direction direction)
        {
            return GetNeighborOffsetFlat(cell.To3D(), direction).To2D();
        }

        public static Vector3Int GetNeighborOffsetFlat(this Vector3Int cell, Direction direction)
        {
            // Not a mistake, Unity swizzles cell coordinates for flat top grids
            bool isEvenColumn = cell.y % 2 == 0;
            Vector3Int[] offsets = isEvenColumn ? s_evenColumnOffsets : s_oddColumnOffsets;
            Vector3Int offset = offsets[(int)direction];
            return offset;
        }

        public static Vector2Int GetNeighborCellFlat(this Vector2Int cell, Direction direction)
        {
            return GetNeighborCellFlat(cell.To3D(), direction).To2D();
        }

        public static Vector3Int GetNeighborCellFlat(this Vector3Int cell, Direction direction)
        {
            Vector3Int offset = GetNeighborOffsetFlat(cell, direction);
            return cell + offset;
        }

        public enum Direction
        {
            Up = 0,
            UpRight = 1,
            DownRight = 2,
            Down = 3,
            DownLeft = 4,
            UpLeft = 5,
        }
    }
}