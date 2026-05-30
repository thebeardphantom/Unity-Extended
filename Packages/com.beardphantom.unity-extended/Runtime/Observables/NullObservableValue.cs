namespace BeardPhantom.UnityExtended
{
    public class NullObservableValue<T> : IObservableValue<T>
    {
        event LiteEvent<ObservableValueChangedArgs<T>>.OnEventInvoked IReadOnlyObservableValue<T>.ValueChanged
        {
            add { }
            remove { }
        }

        event LiteEvent<ObservableValueChangedArgs<T>>.OnEventInvoked IReadOnlyObservableValue<T>.ValueChangedWithRegisterInvoke
        {
            add { }
            remove { }
        }

        public static readonly NullObservableValue<T> Instance = new();

        private T _value;

        T IObservableValue<T>.Value
        {
            get => default;
            set { }
        }

        T IReadOnlyObservableValue<T>.Value => default;

        private NullObservableValue() { }

        void IObservableValue<T>.SetValueWithoutNotify(T newValue) { }

        void IObservableValue<T>.OffsetChangeScopeCount(int offset) { }

        void IObservableValue<T>.InvokeValueChange(T oldValue) { }
    }
}