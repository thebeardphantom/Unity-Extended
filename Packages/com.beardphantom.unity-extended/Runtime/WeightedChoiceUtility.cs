using System;
using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public static class WeightedChoiceUtility
    {
        public static readonly UnityRandomAdapter UnityRandomAdapter = new();

        private static readonly SystemRandomAdapter _systemRandomAdapter = new(null);

        public static T ChooseFromWeighted<T>(this IReadOnlyList<T> choices) where T : IWeightedChoice
        {
            return ChooseFromWeighted(choices, UnityRandomAdapter);
        }

        public static int ChooseIndexFromWeighted<T>(this IReadOnlyList<T> choices) where T : IWeightedChoice
        {
            return ChooseIndexFromWeighted(choices, UnityRandomAdapter);
        }

        public static T ChooseFromWeighted<T>(this IReadOnlyList<T> choices, Random systemRandom) where T : IWeightedChoice
        {
            _systemRandomAdapter.Random = systemRandom;
            var result = ChooseFromWeighted(choices, _systemRandomAdapter);
            _systemRandomAdapter.Random = null;
            return result;
        }

        public static int ChooseIndexFromWeighted<T>(this IReadOnlyList<T> choices, Random systemRandom) where T : IWeightedChoice
        {
            _systemRandomAdapter.Random = systemRandom;
            var index = ChooseIndexFromWeighted(choices, _systemRandomAdapter);
            _systemRandomAdapter.Random = null;
            return index;
        }

        public static T ChooseFromWeighted<T>(this IReadOnlyList<T> choices, IRandomAdapter randomAdapter)
            where T : IWeightedChoice
        {
            var index = ChooseIndexFromWeighted(choices, randomAdapter);
            return index < 0 ? default : choices[index];
        }

        public static int ChooseIndexFromWeighted<T>(this IReadOnlyList<T> choices, IRandomAdapter randomAdapter)
            where T : IWeightedChoice
        {
            var weightSum = 0;
            for (var i = 0; i < choices.Count; i++)
            {
                weightSum += choices[i].Weight;
            }

            var rng = randomAdapter.Next(0, weightSum);
            for (var i = 0; i < choices.Count; i++)
            {
                var choice = choices[i];
                if (rng < choice.Weight)
                {
                    return i;
                }

                rng -= choice.Weight;
            }

            return -1;
        }
    }
}