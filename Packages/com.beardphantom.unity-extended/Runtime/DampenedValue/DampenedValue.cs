using System;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    [Serializable]
    public abstract class DampenedValue<T> : IDampenedValue
    {
        public T Value { get; set; }

        public T TargetValue { get; set; }

        public T Velocity { get; set; }

        [field: SerializeField]
        public float SmoothTime { get; private set; }

        [field: SerializeField]
        public float MaxSpeed { get; private set; } = float.PositiveInfinity;

        public abstract T Simulate(float deltaTime);

        public T Simulate()
        {
            return Simulate(Time.deltaTime);
        }
    }
}