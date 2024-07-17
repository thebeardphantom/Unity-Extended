namespace BeardPhantom.UnityExtended
{
    public interface IObservableValue<T> : IReadOnlyObservableValue<T>
    {
        new T Value { get; set; }

        void SetValueWithoutNotify(T newValue);

        internal void OffsetChangeScopeCount(int offset);

        internal void InvokeValueChange(T oldValue);
    }
}