using System;
using System.Collections.Generic;

public abstract class PhysicsRaycastComparer<TComparer, TElement> : IComparer<TElement>
    where TComparer : PhysicsRaycastComparer<TComparer, TElement>, new()
{
    #region Fields

    public static readonly TComparer Instance = new();

    #endregion

    #region Methods

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

    #endregion
}