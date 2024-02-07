using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public class ObservableValue<T> : IObservableValue<T>
    {
        #region Fields

        private T _value;

        #endregion

        #region Properties

        public LiteEvent<ObservableValueChangedArgs<T>> ValueChanged { get; } = new();

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
                ValueChanged.Invoke(new ObservableValueChangedArgs<T>(this, oldValue, _value));
            }
        }

        #endregion

        #region Constructors

        public ObservableValue() { }

        public ObservableValue(T initialValue)
        {
            SetWithoutNotify(initialValue);
        }

        #endregion

        #region Methods

        public void SetWithoutNotify(T newValue)
        {
            _value = newValue;
        }

        public static implicit operator T(ObservableValue<T> observableValue)
        {
            return observableValue.Value;
        }

        #endregion
    }
}