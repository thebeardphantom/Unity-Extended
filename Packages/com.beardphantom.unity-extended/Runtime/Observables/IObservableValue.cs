namespace BeardPhantom.UnityExtended
{
    public interface IObservableValue<T>
    {
        #region Properties

        T Value { get; set; }

        LiteEvent<ObservableValueChangedArgs<T>> ValueChanged { get; }

        #endregion
    }
}