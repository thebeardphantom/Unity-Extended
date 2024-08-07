﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    [Serializable]
    public struct SerializedKeyValuePair<TKey, TValue>
    {
        [field: SerializeField]
        public TKey Key { get; private set; }

        [field: SerializeField]
        public TValue Value { get; private set; }

        public SerializedKeyValuePair(KeyValuePair<TKey, TValue> keyValuePair)
        {
            Key = keyValuePair.Key;
            Value = keyValuePair.Value;
        }
    }
}