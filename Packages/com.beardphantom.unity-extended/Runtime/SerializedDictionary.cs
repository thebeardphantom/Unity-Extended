using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    [Serializable]
    public class SerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        public const string SerializedKeyValuePairsPropertyName = nameof(SerializedKeyValuePairs);

        [field: SerializeField]
        private List<SerializedKeyValuePair<TKey, TValue>> SerializedKeyValuePairs { get; set; } = new();

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
    }
}