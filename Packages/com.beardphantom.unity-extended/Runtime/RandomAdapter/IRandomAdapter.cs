namespace BeardPhantom.UnityExtended
{
    public interface IRandomAdapter
    {
        #region Properties

        int Seed { set; }

        float Value { get; }

        #endregion

        #region Methods

        int Next(int minInclusive, int maxExclusive);

        float Next(float minInclusive, float maxInclusive);

        #endregion
    }
}