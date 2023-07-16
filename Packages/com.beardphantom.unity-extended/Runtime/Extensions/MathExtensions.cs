namespace BeardPhantom.UnityExtended
{
    public static class MathExtensions
    {
        #region Methods

        public static int Mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        #endregion
    }
}