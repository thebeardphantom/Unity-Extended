using System;
using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public abstract class PhysicsRaycastComparer<TComparer, TElement> : IComparer<TElement>
        where TComparer : PhysicsRaycastComparer<TComparer, TElement>, new()
    {
        public static readonly TComparer Instance = new();

        public static void Sort(TElement[] input, int length)
        {
            Array.Sort(input, 0, length, Instance);
        }

        public static void Sort(List<TElement> input, int length)
        {
            input.Sort(0, length, Instance);
        }

        /// <inheritdoc />
        public abstract int Compare(TElement x, TElement y);
    }
}