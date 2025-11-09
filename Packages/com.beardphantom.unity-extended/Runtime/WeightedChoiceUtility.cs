using System;
using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public static class WeightedChoiceUtility
    {
        private static readonly SystemRandomNumberGenerator s_systemRandomNumberGenerator = new(null);

        public static T ChooseFromWeighted<T>(this IReadOnlyList<T> choices) where T : IWeightedChoice
        {
            return ChooseFromWeighted(choices, UnityRandomNumberGenerator.Instance);
        }

        public static int ChooseIndexFromWeighted<T>(this IReadOnlyList<T> choices) where T : IWeightedChoice
        {
            return ChooseIndexFromWeighted(choices, UnityRandomNumberGenerator.Instance);
        }

        public static T ChooseFromWeighted<T>(this IReadOnlyList<T> choices, Random systemRandom) where T : IWeightedChoice
        {
            s_systemRandomNumberGenerator.Random = systemRandom;
            T result = ChooseFromWeighted(choices, s_systemRandomNumberGenerator);
            s_systemRandomNumberGenerator.Random = null;
            return result;
        }

        public static int ChooseIndexFromWeighted<T>(this IReadOnlyList<T> choices, Random systemRandom) where T : IWeightedChoice
        {
            s_systemRandomNumberGenerator.Random = systemRandom;
            int index = ChooseIndexFromWeighted(choices, s_systemRandomNumberGenerator);
            s_systemRandomNumberGenerator.Random = null;
            return index;
        }

        public static T ChooseFromWeighted<T>(this IReadOnlyList<T> choices, IRandomNumberGenerator rng)
            where T : IWeightedChoice
        {
            int index = ChooseIndexFromWeighted(choices, rng);
            return index < 0 ? default : choices[index];
        }

        public static int ChooseIndexFromWeighted<T>(this IReadOnlyList<T> choices, IRandomNumberGenerator rng)
            where T : IWeightedChoice
        {
            var weightSum = 0;
            for (var i = 0; i < choices.Count; i++)
            {
                weightSum += choices[i].Weight;
            }

            int value = rng.Next(0, weightSum);
            for (var i = 0; i < choices.Count; i++)
            {
                T choice = choices[i];
                if (value < choice.Weight)
                {
                    return i;
                }

                value -= choice.Weight;
            }

            return -1;
        }
    }
}