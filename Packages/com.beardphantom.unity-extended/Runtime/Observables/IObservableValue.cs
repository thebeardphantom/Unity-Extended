namespace BeardPhantom.UnityExtended
{
    public interface IObservableValue<T> : IReadOnlyObservableValue<T>
    {
        #region Properties

        new T Value { get; set; }

        #endregion

        #region Methods

        void SetValueWithoutNotify(T newValue);

        internal void OffsetChangeScopeCount(int offset);

        internal void InvokeValueChange(T oldValue);

        #endregion
    }
}