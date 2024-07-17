using System;

namespace BeardPhantom.UnityExtended
{
    public interface ILiteEvent<in TDelegate> where TDelegate : Delegate
    {
        int ListenerCount { get; }

        bool Enabled { get; }

        void Clear();

        bool Add(TDelegate listener);

        bool Remove(TDelegate listener);
    }
}