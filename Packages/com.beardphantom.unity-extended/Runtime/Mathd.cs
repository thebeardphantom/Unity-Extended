using System;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Cloned Math functions for doubles.
    /// </summary>
    public static class Mathd
    {
        #region Fields

        public const double PI = Math.PI;

        public const double INFINITY = double.PositiveInfinity;

        public const double NEGATIVE_INFINITY = double.NegativeInfinity;

        public const double DEG_2_RAD = PI * 2.0 / 360.0;

        public const double RAD_2_DEG = 1.0 / DEG_2_RAD;

        public const double EQUALITY_TOLERANCE_DEFAULT = 0.0000000001;

        public static double EqualityTolerance = EQUALITY_TOLERANCE_DEFAULT;

        #endregion

        #region Methods

        // Returns the sine of angle /f/ in radians.
        public static double Sin(double f)
        {
            return Math.Sin(f);
        }

        // Returns the cosine of angle /f/ in radians.
        public static double Cos(double f)
        {
            return Math.Cos(f);
        }

        // Returns the tangent of angle /f/ in radians.
        public static double Tan(double f)
        {
            return Math.Tan(f);
        }

        // Returns the arc-sine of /f/ - the angle in radians whose sine is /f/.
        public static double Asin(double f)
        {
            return Math.Asin(f);
        }

        // Returns the arc-cosine of /f/ - the angle in radians whose cosine is /f/.
        public static double Acos(double f)
        {
            return Math.Acos(f);
        }

        // Returns the arc-tangent of /f/ - the angle in radians whose tangent is /f/.
        public static double Atan(double f)
        {
            return Math.Atan(f);
        }

        // Returns the angle in radians whose ::ref::Tan is @@y/x@@.
        public static double Atan2(double y, double x)
        {
            return Math.Atan2(y, x);
        }

        // Returns square root of /f/.
        public static double Sqrt(double f)
        {
            return Math.Sqrt(f);
        }

        // Returns the absolute value of /f/.
        public static double Abs(double f)
        {
            return Math.Abs(f);
        }

        // Returns the absolute value of /value/.
        public static int Abs(int value)
        {
            return Math.Abs(value);
        }

        /// *listonly*
        public static double Min(double a, double b)
        {
            return a < b ? a : b;
        }

        // Returns the smallest of two or more values.
        public static double Min(params double[] values)
        {
            var len = values.Length;
            if (len == 0)
            {
                return 0;
            }

            var m = values[0];
            for (var i = 1; i < len; i++)
            {
                if (values[i] < m)
                {
                    m = values[i];
                }
            }

            return m;
        }

        /// *listonly*
        public static int Min(int a, int b)
        {
            return a < b ? a : b;
        }

        // Returns the smallest of two or more values.
        public static int Min(params int[] values)
        {
            var len = values.Length;
            if (len == 0)
            {
                return 0;
            }

            var m = values[0];
            for (var i = 1; i < len; i++)
            {
                if (values[i] < m)
                {
                    m = values[i];
                }
            }

            return m;
        }

        /// *listonly*
        public static double Max(double a, double b)
        {
            return a > b ? a : b;
        }

        // Returns largest of two or more values.
        public static double Max(params double[] values)
        {
            var len = values.Length;
            if (len == 0)
            {
                return 0;
            }

            var m = values[0];
            for (var i = 1; i < len; i++)
            {
                if (values[i] > m)
                {
                    m = values[i];
                }
            }

            return m;
        }

        /// *listonly*
        public static int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        // Returns the largest of two or more values.
        public static int Max(params int[] values)
        {
            var len = values.Length;
            if (len == 0)
            {
                return 0;
            }

            var m = values[0];
            for (var i = 1; i < len; i++)
            {
                if (values[i] > m)
                {
                    m = values[i];
                }
            }

            return m;
        }

        // Returns /f/ raised to power /p/.
        public static double Pow(double f, double p)
        {
            return Math.Pow(f, p);
        }

        // Returns e raised to the specified power.
        public static double Exp(double power)
        {
            return Math.Exp(power);
        }

        // Returns the logarithm of a specified number in a specified base.
        public static double Log(double f, double p)
        {
            return Math.Log(f, p);
        }

        // Returns the natural (base e) logarithm of a specified number.
        public static double Log(double f)
        {
            return Math.Log(f);
        }

        // Returns the base 10 logarithm of a specified number.
        public static double Log10(double f)
        {
            return Math.Log10(f);
        }

        // Returns the smallest integer greater to or equal to /f/.
        public static double Ceil(double f)
        {
            return Math.Ceiling(f);
        }

        // Returns the largest integer smaller to or equal to /f/.
        public static double Floor(double f)
        {
            return Math.Floor(f);
        }

        // Returns /f/ rounded to the nearest integer.
        public static double Round(double f)
        {
            return Math.Round(f);
        }

        // Returns the smallest integer greater to or equal to /f/.
        public static int CeilToInt(double f)
        {
            return (int)Math.Ceiling(f);
        }

        // Returns the largest integer smaller to or equal to /f/.
        public static int FloorToInt(double f)
        {
            return (int)Math.Floor(f);
        }

        // Returns /f/ rounded to the nearest integer.
        public static int RoundToInt(double f)
        {
            return (int)Math.Round(f);
        }

        // Returns the sign of /f/.
        public static double Sign(double f)
        {
            return f >= 0 ? 1 : -1;
        }

        // Clamps a value between a minimum double and maximum double value.
        public static double Clamp(double value, double min, double max)
        {
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }

            return value;
        }

        // Clamps value between min and max and returns value.
        // Set the position of the transform to be that of the time
        // but never less than 1 or more than 3
        //
        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }

            return value;
        }

        // Clamps value between 0 and 1 and returns value
        public static double Clamp01(double value)
        {
            if (value < 0)
            {
                return 0;
            }

            if (value > 1)
            {
                return 1;
            }

            return value;
        }

        // Interpolates between /a/ and /b/ by /t/. /t/ is clamped between 0 and 1.
        public static double Lerp(double a, double b, double t)
        {
            return a + (b - a) * Clamp01(t);
        }

        // Interpolates between /a/ and /b/ by /t/ without clamping the interpolant.
        public static double LerpUnclamped(double a, double b, double t)
        {
            return a + (b - a) * t;
        }

        // Same as ::ref::Lerp but makes sure the values interpolate correctly when they wrap around 360 degrees.
        public static double LerpAngle(double a, double b, double t)
        {
            var delta = Repeat(b - a, 360);
            if (delta > 180)
            {
                delta -= 360;
            }

            return a + delta * Clamp01(t);
        }

        // Moves a value /current/ towards /target/.
        public static double MoveTowards(double current, double target, double maxDelta)
        {
            if (Math.Abs(target - current) <= maxDelta)
            {
                return target;
            }

            return current + Math.Sign(target - current) * maxDelta;
        }

        // Same as ::ref::MoveTowards but makes sure the values interpolate correctly when they wrap around 360 degrees.
        public static double MoveTowardsAngle(double current, double target, double maxDelta)
        {
            var deltaAngle = DeltaAngle(current, target);
            if (-maxDelta < deltaAngle && deltaAngle < maxDelta)
            {
                return target;
            }

            target = current + deltaAngle;
            return MoveTowards(current, target, maxDelta);
        }

        // Interpolates between /min/ and /max/ with smoothing at the limits.
        public static double SmoothStep(double from, double to, double t)
        {
            t = Clamp01(t);
            t = -2.0 * t * t * t + 3.0 * t * t;
            return to * t + from * (1 - t);
        }

        //*undocumented
        public static double Gamma(double value, double absmax, double gamma)
        {
            var negative = value < 0;
            var absval = Abs(value);
            if (absval > absmax)
            {
                return negative ? -absval : absval;
            }

            var result = Pow(absval / absmax, gamma) * absmax;
            return negative ? -result : result;
        }

        // Compares two doubleing point values if they are similar.
        public static bool Approximately(double a, double b)
        {
            return Math.Abs(a - b) < EqualityTolerance;
        }

        public static double SmoothDamp(
            double current,
            double target,
            ref double currentVelocity,
            double smoothTime,
            double maxSpeed)
        {
            var deltaTime = Time.deltaTime;
            return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        public static double SmoothDamp(double current, double target, ref double currentVelocity, double smoothTime)
        {
            var deltaTime = Time.deltaTime;
            var maxSpeed = INFINITY;
            return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        // Gradually changes a value towards a desired goal over time.
        public static double SmoothDamp(
            double current,
            double target,
            ref double currentVelocity,
            double smoothTime,
            double maxSpeed,
            double deltaTime)
        {
            // Based on Game Programming Gems 4 Chapter 1.10
            smoothTime = Math.Max(0.0001, smoothTime);
            var omega = 2 / smoothTime;

            var x = omega * deltaTime;
            var exp = 1 / (1 + x + 0.48 * x * x + 0.235 * x * x * x);
            var change = current - target;
            var originalTo = target;

            // Clamp maximum speed
            var maxChange = maxSpeed * smoothTime;
            change = Math.Clamp(change, -maxChange, maxChange);
            target = current - change;

            var temp = (currentVelocity + omega * change) * deltaTime;
            currentVelocity = (currentVelocity - omega * temp) * exp;
            var output = target + (change + temp) * exp;

            // Prevent overshooting
            if (originalTo - current > 0.0 == output > originalTo)
            {
                output = originalTo;
                currentVelocity = (output - originalTo) / deltaTime;
            }

            return output;
        }

        public static double SmoothDampAngle(
            double current,
            double target,
            ref double currentVelocity,
            double smoothTime,
            double maxSpeed)
        {
            var deltaTime = Time.deltaTime;
            return SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        public static double SmoothDampAngle(double current, double target, ref double currentVelocity, double smoothTime)
        {
            var deltaTime = Time.deltaTime;
            var maxSpeed = INFINITY;
            return SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        // Gradually changes an angle given in degrees towards a desired goal angle over time.
        public static double SmoothDampAngle(
            double current,
            double target,
            ref double currentVelocity,
            double smoothTime,
            double maxSpeed,
            double deltaTime)
        {
            target = current + DeltaAngle(current, target);
            return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        // Loops the value t, so that it is never larger than length and never smaller than 0.
        public static double Repeat(double t, double length)
        {
            return Clamp(t - Math.Floor(t / length) * length, 0.0, length);
        }

        // PingPongs the value t, so that it is never larger than length and never smaller than 0.
        public static double PingPong(double t, double length)
        {
            t = Repeat(t, length * 2);
            return length - Math.Abs(t - length);
        }

        // Calculates the ::ref::Lerp parameter between of two values.
        public static double InverseLerp(double a, double b, double value)
        {
            if (!Approximately(a, b))
            {
                return Clamp01((value - a) / (b - a));
            }

            return 0.0;
        }

        // Calculates the shortest difference between two given angles.
        public static double DeltaAngle(double current, double target)
        {
            var delta = Repeat(target - current, 360.0);
            if (delta > 180.0)
            {
                delta -= 360.0;
            }

            return delta;
        }

        public static double RoundToMultipleOf(double value, double roundingValue)
        {
            if (roundingValue == 0)
            {
                return value;
            }

            return Math.Round(value / roundingValue) * roundingValue;
        }

        public static double GetClosestPowerOfTen(double positiveNumber)
        {
            if (positiveNumber <= 0)
            {
                return 1;
            }

            return Math.Pow(10, RoundToInt(Math.Log10(positiveNumber)));
        }

        public static int GetNumberOfDecimalsForMinimumDifference(double minDifference)
        {
            return (int)Math.Max(0.0, -Math.Floor(Math.Log10(Math.Abs(minDifference))));
        }

        public static double RoundBasedOnMinimumDifference(double valueToRound, double minDifference)
        {
            if (minDifference == 0)
            {
                return DiscardLeastSignificantDecimal(valueToRound);
            }

            return Math.Round(
                valueToRound,
                GetNumberOfDecimalsForMinimumDifference(minDifference),
                MidpointRounding.AwayFromZero);
        }

        public static double DiscardLeastSignificantDecimal(double v)
        {
            var decimals = Math.Max(0, (int)(5 - Math.Log10(Math.Abs(v))));
            try
            {
                return Math.Round(v, decimals);
            }
            catch (ArgumentOutOfRangeException)
            {
                // This can happen for very small numbers.
                return 0;
            }
        }

        public static float ClampToFloat(double value)
        {
            if (double.IsPositiveInfinity(value))
            {
                return float.PositiveInfinity;
            }

            if (double.IsNegativeInfinity(value))
            {
                return float.NegativeInfinity;
            }

            if (value < float.MinValue)
            {
                return float.MinValue;
            }

            if (value > float.MaxValue)
            {
                return float.MaxValue;
            }

            return (float)value;
        }

        #endregion

        // A tiny doubleing point value (RO).
    }
}