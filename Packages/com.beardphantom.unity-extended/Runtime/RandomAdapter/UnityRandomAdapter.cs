using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public class UnityRandomAdapter : IRandomAdapter
    {
        public static readonly UnityRandomAdapter Instance = new();

        /// <inheritdoc />
        public int Seed
        {
            set => Random.InitState(value);
        }

        /// <inheritdoc />
        public float Value => Random.value;

        /// <inheritdoc />
        public int Next(int minInclusive, int maxExclusive)
        {
            return Random.Range(minInclusive, maxExclusive);
        }

        /// <inheritdoc />
        public float Next(float minInclusive, float maxInclusive)
        {
            return Random.Range(minInclusive, maxInclusive);
        }
    }
}