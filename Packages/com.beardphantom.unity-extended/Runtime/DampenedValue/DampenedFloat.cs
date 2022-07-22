using System;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    [Serializable]
    public class DampenedFloat : DampenedValue<float>
    {
        #region Properties

        [field: SerializeField]
        public bool AsAngle { get; private set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override float Simulate(float deltaTime)
        {
            var velocity = Velocity;
            Value = AsAngle
                ? Mathf.SmoothDampAngle(Value, TargetValue, ref velocity, SmoothTime, MaxSpeed, deltaTime)
                : Mathf.SmoothDamp(Value, TargetValue, ref velocity, SmoothTime, MaxSpeed, deltaTime);
            Velocity = velocity;
            return Value;
        }

        #endregion
    }
}