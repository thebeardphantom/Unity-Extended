using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public class UnityRandomAdapter : IRandomAdapter
    {
        #region Methods

        /// <inheritdoc />
        public int Next(int minInclusive, int maxExclusive)
        {
            return Random.Range(minInclusive, maxExclusive);
        }

        #endregion
    }
}