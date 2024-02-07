namespace BeardPhantom.UnityExtended
{
    public readonly struct ObservableValueChangedArgs<T>
    {
        #region Fields

        public readonly IObservableValue<T> Observable;

        public readonly T OldValue;

        public readonly T NewValue;

        #endregion

        #region Constructors

        public ObservableValueChangedArgs(IObservableValue<T> observable, T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
            Observable = observable;
        }

        #endregion
    }
}