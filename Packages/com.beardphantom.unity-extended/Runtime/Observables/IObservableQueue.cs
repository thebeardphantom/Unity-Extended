using System;
using System.Collections;

namespace BeardPhantom.UnityExtended
{
    public interface IObservableQueue : ICollection
    {
        void CopyFrom(Array array);
    }
}