using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    [Serializable]
    public class SerializedHashSet<T> : HashSet<T>, ISerializationCallbackReceiver
    {
        #region Fields

        public const string SERIALIZED_VALUES_PROPERTY_NAME = nameof(SerializedValues);

        private bool _isValid;

        #endregion

        #region Properties

        [field: SerializeField]
        private List<T> SerializedValues { get; set; } = new();

        #endregion

        #region Constructors

        /*
         * Without a call to specific base constructors, some underlying arrays won't be initialized correctly.
         * The constructor that takes a capacity calls Initialize, which fixes this issue.
         */
        public SerializedHashSet() : base(4) { }

        #endregion

        #region Methods

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (!_isValid)
            {
                return;
            }

            SerializedValues.Clear();
            SerializedValues.AddRange(this);
        }

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Clear();
            _isValid = true;
            foreach (var serializedValue in SerializedValues)
            {
                if (!Add(serializedValue))
                {
                    _isValid = false;
                }
            }

            if (_isValid)
            {
                SerializedValues.Clear();
            }
        }

        #endregion
    }
}