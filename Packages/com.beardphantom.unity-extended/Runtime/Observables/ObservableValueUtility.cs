namespace BeardPhantom.UnityExtended
{
    public static class ObservableValueUtility
    {
        public static ObservableValueChangedArgs<T> CreateEventArgsFromCurrentValue<T>(
            this IReadOnlyObservableValue<T> observableValue)
        {
            var value = observableValue.Value;
            return new ObservableValueChangedArgs<T>(observableValue, value, value);
        }

        public static ObservableChangeScope<T> BeginChangeScope<T>(this IObservableValue<T> observableValue)
        {
            observableValue.OffsetChangeScopeCount(1);
            return new ObservableChangeScope<T>(observableValue);
        }

        public static ObservableChangeScope<T> BeginChangeScope<T>(
            this IObservableValue<T> observableValue,
            out IObservableValue<T> outObservableValue)
        {
            observableValue.OffsetChangeScopeCount(1);
            outObservableValue = observableValue;
            return new ObservableChangeScope<T>(observableValue);
        }

        public static ObservableChangeScope<T> BeginChangeScope<T>(
            this ObservableValue<T> observableValue,
            out ObservableValue<T> outObservableValue)
        {
            var scope = BeginChangeScope(observableValue);
            outObservableValue = observableValue;
            return scope;
        }
    }
}