using System;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityExtended.Editor
{
    public readonly struct AssetEditingScope : IDisposable
    {
        private readonly bool _isValid;

        private AssetEditingScope(int _)
        {
            _isValid = true;
            AssetDatabase.StartAssetEditing();
        }

        public static AssetEditingScope Create()
        {
            return new AssetEditingScope(0);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_isValid)
            {
                AssetDatabase.StopAssetEditing();
            }
            else
            {
                Debug.LogError("AssetEditingScope must be created through Create() method.");
            }
        }
    }
}