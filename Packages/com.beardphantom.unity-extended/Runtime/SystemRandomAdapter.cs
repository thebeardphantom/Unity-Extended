using System;

namespace BeardPhantom.UnityExtended
{
    public class SystemRandomAdapter : IRandomAdapter
    {
        #region Properties

        /// <inheritdoc />
        public int Seed
        {
            set => Random = new Random(value);
        }

        /// <inheritdoc />
        public float Value => (float)Random.NextDouble();

        internal Random Random { get; set; }

        #endregion

        #region Constructors

        public SystemRandomAdapter(Random random)
        {
            Random = random;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public int Next(int minInclusive, int maxExclusive)
        {
            return Random.Next(minInclusive, maxExclusive);
        }

        /// <inheritdoc />
        public float Next(float minInclusive, float maxInclusive)
        {
            return (float)Mathd.Lerp(minInclusive, maxInclusive, Random.NextDouble());
        }

        #endregion
    }
}