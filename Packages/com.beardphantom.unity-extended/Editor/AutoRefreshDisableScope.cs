using System;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityExtended.Editor
{
    public readonly struct AutoRefreshDisableScope : IDisposable
    {
        private readonly bool _isValid;

        private AutoRefreshDisableScope(int _)
        {
            _isValid = true;
            AssetDatabase.DisallowAutoRefresh();
        }

        public static AutoRefreshDisableScope Create()
        {
            return new AutoRefreshDisableScope(0);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_isValid)
            {
                AssetDatabase.AllowAutoRefresh();
            }
            else
            {
                Debug.LogError("AutoRefreshDisableScope must be created through Create() method.");
            }
        }
    }
}