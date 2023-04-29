using System;

namespace BeardPhantom.UnityExtended
{
    public interface ILiteEvent<in TDelegate> where TDelegate : Delegate
    {
        #region Properties

        int ListenerCount { get; }

        #endregion

        #region Methods

        void Clear();

        bool Register(TDelegate listener);

        bool Unregister(TDelegate listener);

        #endregion
    }
}