using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class MathfExtended
    {
        #region Methods

        public static float Wrap(float value, float min, float max)
        {
            var range = max - min;
            var wrappedValue = value - Mathf.Floor((value - min) / range) * range;

            if (wrappedValue < min)
            {
                wrappedValue += range;
            }
            else if (wrappedValue >= max)
            {
                wrappedValue -= range;
            }

            return wrappedValue;
        }

        #endregion
    }
}