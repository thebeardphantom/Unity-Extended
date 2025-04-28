using System;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    [Serializable]
    public class DampenedVector3 : DampenedValue<Vector3>
    {
        /// <inheritdoc />
        public override Vector3 Simulate(float deltaTime)
        {
            Vector3 velocity = Velocity;
            Value = Vector3.SmoothDamp(Value, TargetValue, ref velocity, SmoothTime, MaxSpeed, deltaTime);
            Velocity = velocity;
            return Value;
        }
    }
}