namespace BeardPhantom.UnityExtended
{
    public interface IReadOnlyFastRef<out T> where T : class
    {
        #region Properties

        T Value { get; }

        bool HasValue { get; }

        #endregion
    }
}