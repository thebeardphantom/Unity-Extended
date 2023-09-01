using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    [Serializable]
    public class SerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        #region Fields

        public const string SERIALIZED_KEY_VALUE_PAIRS_PROPERTY_NAME = nameof(SerializedKeyValuePairs);

        #endregion

        #region Properties

        [field: SerializeField]
        private List<SerializedKeyValuePair<TKey, TValue>> SerializedKeyValuePairs { get; set; } = new();

        #endregion

        #region Methods

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            SerializedKeyValuePairs.Clear();
            SerializedKeyValuePairs.ToSerialized(this);
        }

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Clear();
            this.FromSerialized(SerializedKeyValuePairs);
            SerializedKeyValuePairs.Clear();
        }

        #endregion
    }
}