using UnityEngine;

namespace BeardPhantom.UnityExtended.HexMath
{
    public static class HexPointyTop
    {
        private static readonly Vector3Int[] s_evenRowOffsets =
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
            new(-1, 0),
        };

        private static readonly Vector3Int[] s_oddRowOffsets =
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
            new(-1, 0),
        };

        public static Vector2Int GetNeighborOffsetPointy(this Vector2Int cell, Direction direction)
        {
            return GetNeighborOffsetPointy(cell.To3D(), direction).To2D();
        }

        public static Vector3Int GetNeighborOffsetPointy(this Vector3Int cell, Direction direction)
        {
            bool isEvenRow = cell.y % 2 == 0;
            Vector3Int[] offsets = isEvenRow ? s_evenRowOffsets : s_oddRowOffsets;
            Vector3Int offset = offsets[(int)direction];
            return offset;
        }

        public static Vector2Int GetNeighborCellPointy(this Vector2Int cell, Direction direction)
        {
            return GetNeighborCellPointy(cell.To3D(), direction).To2D();
        }

        public static Vector3Int GetNeighborCellPointy(this Vector3Int cell, Direction direction)
        {
            Vector3Int offset = GetNeighborOffsetPointy(cell, direction);
            return cell + offset;
        }

        public enum Direction
        {
            UpLeft = 0,
            UpRight = 1,
            Right = 2,
            DownRight = 3,
            DownLeft = 4,
            Left = 5,
        }
    }
}