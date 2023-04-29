using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public class ObservableValue<T>
    {
        #region Types

        #endregion

        #region Fields

        public readonly LiteEvent<ObservableValueChangedArgs<T>> ValueChanged = new();

        private T _value;

        #endregion

        #region Properties

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
                ValueChanged.Invoke(new ObservableValueChangedArgs<T>(oldValue, _value));
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