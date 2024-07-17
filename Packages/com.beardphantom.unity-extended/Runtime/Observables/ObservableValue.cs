using System.Collections.Generic;
using UnityEngine.Assertions;

namespace BeardPhantom.UnityExtended
{
    public class ObservableValue<T> : IObservableValue<T>
    {
        public event LiteEvent<ObservableValueChangedArgs<T>>.OnEventInvoked ValueChanged
        {
            add => _valueChanged.Add(value);
            remove => _valueChanged.Remove(value);
        }

        /// <inheritdoc />
        public event LiteEvent<ObservableValueChangedArgs<T>>.OnEventInvoked ValueChangedWithRegisterInvoke
        {
            add
            {
                ValueChanged += value;
                value?.Invoke(this.CreateEventArgsFromCurrentValue());
            }
            remove => ValueChanged -= value;
        }

        private readonly LiteEvent<ObservableValueChangedArgs<T>> _valueChanged = new();

        private T _value;

        private int _changeScopeCount;

        public T Value
        {
            get => _value;
            set
            {
                if (EqualityComparer<T>.Default.Equals(_value, value))
                {
                    return;
                }

                var oldValue = _value;
                _value = value;
                if (_changeScopeCount > 0)
                {
                    return;
                }

                ((IObservableValue<T>)this).InvokeValueChange(oldValue);
            }
        }

        public ObservableValue() { }

        public ObservableValue(T initialValue)
        {
            SetValueWithoutNotify(initialValue);
        }

        /// <inheritdoc />
        public void SetValueWithoutNotify(T newValue)
        {
            _value = newValue;
        }

        void IObservableValue<T>.InvokeValueChange(T oldValue)
        {
            _valueChanged.Invoke(new ObservableValueChangedArgs<T>(this, oldValue, _value));
        }

        /// <inheritdoc />
        void IObservableValue<T>.OffsetChangeScopeCount(int offset)
        {
            _changeScopeCount += offset;
            Assert.IsFalse(_changeScopeCount < 0, "_changeScopeCount < 0");
        }

        public static implicit operator T(ObservableValue<T> observableValue)
        {
            return observableValue.Value;
        }
    }
}