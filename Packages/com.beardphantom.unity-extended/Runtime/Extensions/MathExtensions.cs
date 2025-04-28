using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class MathExtensions
    {
        public static int Mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        public static int Repeat(int value, int min, int max)
        {
            int range = max - min;
            return Repeat(value - min, range) + min;
        }

        public static int Repeat(int t, int length)
        {
            return Mathf.Clamp(t - Mathf.FloorToInt((float)t / length) * length, 0, length);
        }
    }
}