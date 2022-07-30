namespace BeardPhantom.UnityExtended
{
    public readonly struct ObservableValueChangedArgs<T>
    {
        #region Fields

        public readonly T OldValue;

        public readonly T NewValue;

        #endregion

        #region Constructors

        public ObservableValueChangedArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        #endregion
    }
}