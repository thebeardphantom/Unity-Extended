using System.Collections.Generic;

namespace BeardPhantom.UnityExtended.HexMath
{
    public static class HexUtility
    {
        public static IEnumerable<AxialCoords> Ring(in AxialCoords center, in int ringRadius)
        {
            return center.Ring(ringRadius);
        }

        public static IEnumerable<AxialCoords> Radius(in AxialCoords center, in int radius)
        {
            return center.Radius(radius);
        }

        /// <summary>
        /// Returns the total number of hexes in rings 0 to <paramref name="radius" />.
        /// </summary>
        public static int GetHexCountInRadius(int radius)
        {
            return 1 + 3 * radius * (radius + 1);
        }

        /// <summary>
        /// Returns the number of hexes in a single ring.
        /// </summary>
        public static int GetHexCountInRing(int ringRadius)
        {
            return ringRadius * 6;
        }
    }
}