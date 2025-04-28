using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public static class DictionarySerializationUtility
    {
        public static void ToSerialized<TKey, TValue>(
            this ICollection<SerializedKeyValuePair<TKey, TValue>> collection,
            IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            foreach (KeyValuePair<TKey, TValue> keyValuePair in keyValuePairs)
            {
                collection.Add(new SerializedKeyValuePair<TKey, TValue>(keyValuePair));
            }
        }

        public static bool FromSerialized<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            IEnumerable<SerializedKeyValuePair<TKey, TValue>> serializedKeyValuePairs)
        {
            var addedAll = true;
            foreach (SerializedKeyValuePair<TKey, TValue> serializedKeyValuePair in serializedKeyValuePairs)
            {
                bool didAdd = dictionary.TryAdd(serializedKeyValuePair.Key, serializedKeyValuePair.Value);
                if (!didAdd)
                {
                    addedAll = false;
                }
            }

            return addedAll;
        }
    }
}