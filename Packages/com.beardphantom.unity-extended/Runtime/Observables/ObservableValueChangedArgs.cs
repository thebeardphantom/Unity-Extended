namespace BeardPhantom.UnityExtended
{
    public readonly struct ObservableValueChangedArgs<T>
    {
        public readonly IReadOnlyObservableValue<T> Observable;

        public readonly T OldValue;

        public readonly T NewValue;

        public ObservableValueChangedArgs(IReadOnlyObservableValue<T> observable, T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
            Observable = observable;
        }
    }
}