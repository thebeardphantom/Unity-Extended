using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public class ObservableValue<T>
    {
        #region Types

        public readonly struct ValueChangedArgs
        {
            #region Fields

            public readonly T OldValue;

            public readonly T NewValue;

            #endregion

            #region Constructors

            public ValueChangedArgs(T oldValue, T newValue)
            {
                OldValue = oldValue;
                NewValue = newValue;
            }

            #endregion
        }

        #endregion

        #region Fields

        public readonly LiteEvent<ValueChangedArgs> ValueChanged = new();

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
                ValueChanged.Invoke(new ValueChangedArgs(oldValue, _value));
            }
        }

        #endregion

        #region Methods

        public static implicit operator T(ObservableValue<T> observableValue)
        {
            return observableValue.Value;
        }

        #endregion
    }
}