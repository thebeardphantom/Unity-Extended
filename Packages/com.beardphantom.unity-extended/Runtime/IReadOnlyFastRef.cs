namespace BeardPhantom.UnityExtended
{
    public interface IReadOnlyFastRef<out T> where T : class
    {
        T Value { get; }

        bool HasValue { get; }
    }
}