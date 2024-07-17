using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public interface IReadOnlyObservableValue
    {
        bool IsDefaultValue { get; }
    }

    public interface IReadOnlyObservableValue<T> : IReadOnlyObservableValue
    {
        event LiteEvent<ObservableValueChangedArgs<T>>.OnEventInvoked ValueChanged;

        event LiteEvent<ObservableValueChangedArgs<T>>.OnEventInvoked ValueChangedWithRegisterInvoke;

        T Value { get; }

        /// <inheritdoc />
        bool IReadOnlyObservableValue.IsDefaultValue => EqualityComparer<T>.Default.Equals(Value, default);
    }
}