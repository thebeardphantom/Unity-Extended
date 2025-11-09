using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public class UnityRandomNumberGenerator : IRandomNumberGenerator
    {
        public static readonly UnityRandomNumberGenerator Instance = new();

        /// <inheritdoc />
        public int Seed
        {
            set => Random.InitState(value);
        }

        /// <inheritdoc />
        public float Value => Random.value;

        private UnityRandomNumberGenerator() { }

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