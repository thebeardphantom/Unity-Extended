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

        public static float Repeat(float value, float min, float max)
        {
            float range = max - min;
            return Mathf.Repeat(value - min, range) + min;
        }

        public static float ClampMagnitude(float value, float magnitude)
        {
            magnitude = Mathf.Abs(magnitude);
            return Mathf.Clamp(value, -magnitude, magnitude);
        }

        public static float NormalizeAngle180(float angle)
        {
            angle = NormalizeAngle(angle);
            if (angle > 180.0f)
            {
                angle -= 360f;
            }

            return angle;
        }

        public static float NormalizeAngle(float angle)
        {
            return Mathf.Repeat(angle, 360f);
        }
    }
}