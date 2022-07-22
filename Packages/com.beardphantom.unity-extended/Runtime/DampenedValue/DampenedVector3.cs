using System;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    [Serializable]
    public class DampenedVector3 : DampenedValue<Vector3>
    {
        #region Methods

        /// <inheritdoc />
        public override Vector3 Simulate(float deltaTime)
        {
            var velocity = Velocity;
            Value = Vector3.SmoothDamp(Value, TargetValue, ref velocity, SmoothTime, MaxSpeed, deltaTime);
            Velocity = velocity;
            return Value;
        }

        #endregion
    }
}