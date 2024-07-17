﻿namespace BeardPhantom.UnityExtended
{
    public interface IRandomAdapter
    {
        int Seed { set; }

        float Value { get; }

        int Next(int minInclusive, int maxExclusive);

        float Next(float minInclusive, float maxInclusive);
    }
}