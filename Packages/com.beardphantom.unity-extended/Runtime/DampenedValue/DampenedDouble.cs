using System;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    [Serializable]
    public class DampenedDouble : DampenedValue<double>
    {
        #region Properties

        [field: SerializeField]
        public bool AsAngle { get; private set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override double Simulate(float deltaTime)
        {
            var velocity = Velocity;
            Value = AsAngle
                ? Mathd.SmoothDampAngle(Value, TargetValue, ref velocity, SmoothTime, MaxSpeed, deltaTime)
                : Mathd.SmoothDamp(Value, TargetValue, ref velocity, SmoothTime, MaxSpeed, deltaTime);
            Velocity = velocity;
            return Value;
        }

        #endregion
    }
}