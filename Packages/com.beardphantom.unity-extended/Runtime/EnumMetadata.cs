using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Statically and lazily stores information about an enum type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class EnumData<T> where T : struct, Enum, IConvertible
    {
        #region Fields

        /// <summary>
        /// Names of all values in the type
        /// </summary>
        public static readonly string[] Names;

        /// <summary>
        /// The actual type of this enum
        /// </summary>
        public static readonly Type Type;

        /// <summary>
        /// All valid values in this type
        /// </summary>
        public static readonly T[] Values;

        public static readonly T AllFlagsValue;

        public static readonly bool IsFlagsEnum;

        #endregion

        #region Properties

        /// <summary>
        /// The length of valid values of this type
        /// </summary>
        public static int Count => Values.Length;

        #endregion

        #region Constructors

        /// <summary>
        /// Typechecks T and creates information about this enum type
        /// </summary>
        static EnumData()
        {
            Type = typeof(T);
            Values = (T[])Enum.GetValues(Type);
            Names = Enum.GetNames(Type);
            IsFlagsEnum = true;
            var allFlagsValue = 0;
            foreach (var value in Values)
            {
                var intValue = UnsafeUtility.EnumToInt(value);
                allFlagsValue |= intValue;
                if (!Mathf.IsPowerOfTwo(intValue))
                {
                    IsFlagsEnum = false;
                }
            }

            AllFlagsValue = (T)Enum.ToObject(Type, allFlagsValue);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves the name for a value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetName(T value)
        {
            return Names[Array.IndexOf(Values, value)];
        }

        /// <summary>
        /// Retrieves a value by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetValue(string name)
        {
            return Values[Array.IndexOf(Names, name)];
        }

        #endregion
    }
}