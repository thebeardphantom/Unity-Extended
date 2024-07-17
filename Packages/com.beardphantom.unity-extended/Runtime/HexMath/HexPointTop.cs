using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Unity uses odd-r for pointy-top hex grids and odd-q for flat top hex grids.
    /// This class is only designed to work with pointy-top.
    /// Math from https://www.redblobgames.com/grids/hexagons/#neighbors-offset
    /// </summary>
    public static partial class HexPointTop
    {
        public static Vector2Int CubeToOffsetPoint(this CubeCoord cubeCoord)
        {
            var col = cubeCoord.Q + (cubeCoord.R - (cubeCoord.R & 1)) / 2;
            var row = cubeCoord.R;
            return new Vector2Int(col, row);
        }

        public static CubeCoord OffsetToCubePoint(this Vector2Int offset)
        {
            var q = offset.x - (offset.y - (offset.y & 1)) / 2;
            var r = offset.y;
            return new CubeCoord(q, r, -q - r);
        }
    }
}