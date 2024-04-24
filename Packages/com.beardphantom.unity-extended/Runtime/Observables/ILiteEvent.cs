using System;

namespace BeardPhantom.UnityExtended
{
    public interface ILiteEvent<in TDelegate> where TDelegate : Delegate
    {
        #region Properties

        int ListenerCount { get; }
        
        bool Enabled { get; }

        #endregion

        #region Methods

        void Clear();

        bool Add(TDelegate listener);

        bool Remove(TDelegate listener);

        #endregion
    }
}