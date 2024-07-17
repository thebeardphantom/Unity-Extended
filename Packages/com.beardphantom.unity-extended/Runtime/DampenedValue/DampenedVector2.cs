using System;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    [Serializable]
    public class DampenedVector2 : DampenedValue<Vector2>
    {
        /// <inheritdoc />
        public override Vector2 Simulate(float deltaTime)
        {
            var velocity = Velocity;
            Value = Vector2.SmoothDamp(Value, TargetValue, ref velocity, SmoothTime, MaxSpeed, deltaTime);
            Velocity = velocity;
            return Value;
        }
    }
}