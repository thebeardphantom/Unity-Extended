using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public interface IReadOnlyObservableValue
    {
        #region Properties

        bool IsDefaultValue { get; }

        #endregion
    }

    public interface IReadOnlyObservableValue<T> : IReadOnlyObservableValue
    {
        #region Events

        event LiteEvent<ObservableValueChangedArgs<T>>.OnEventInvoked ValueChanged;

        event LiteEvent<ObservableValueChangedArgs<T>>.OnEventInvoked ValueChangedWithRegisterInvoke;

        #endregion

        #region Properties

        T Value { get; }

        /// <inheritdoc />
        bool IReadOnlyObservableValue.IsDefaultValue => EqualityComparer<T>.Default.Equals(Value, default);

        #endregion
    }
}