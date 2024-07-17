using System;
using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public readonly struct ObservableChangeScope<T> : IDisposable
    {
        private readonly IObservableValue<T> _observableValue;

        private readonly T _initialValue;

        public ObservableChangeScope(IObservableValue<T> observableValue)
        {
            _observableValue = observableValue;
            _initialValue = observableValue.Value;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _observableValue.OffsetChangeScopeCount(-1);
            if (EqualityComparer<T>.Default.Equals(_initialValue, _observableValue.Value))
            {
                return;
            }

            _observableValue.InvokeValueChange(_initialValue);
        }
    }
}