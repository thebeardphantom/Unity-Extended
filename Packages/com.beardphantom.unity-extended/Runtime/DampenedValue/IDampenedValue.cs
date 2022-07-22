namespace BeardPhantom.UnityExtended
{
    public interface IDampenedValue
    {
        #region Properties

        float SmoothTime { get; }

        float MaxSpeed { get; }

        #endregion
    }
}