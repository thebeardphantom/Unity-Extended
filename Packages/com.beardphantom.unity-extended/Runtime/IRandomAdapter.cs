namespace BeardPhantom.UnityExtended
{
    public interface IRandomAdapter
    {
        #region Methods

        int Next(int minInclusive, int maxExclusive);

        #endregion
    }
}