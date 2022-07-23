using System;

namespace BeardPhantom.UnityExtended
{
    public class SystemRandomAdapter : IRandomAdapter
    {
        #region Properties

        public Random Rnd { get; set; }

        #endregion

        #region Constructors

        public SystemRandomAdapter(Random rnd)
        {
            Rnd = rnd;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public int Next(int minInclusive, int maxExclusive)
        {
            return Rnd.Next(minInclusive, maxExclusive);
        }

        #endregion
    }
}