using System;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityExtended.Editor
{
    public readonly struct AssetEditingScope : IDisposable
    {
        #region Fields

        private readonly bool _isValid;

        #endregion

        #region Constructors

        private AssetEditingScope(int _)
        {
            _isValid = true;
            AssetDatabase.StartAssetEditing();
        }

        #endregion

        #region Methods

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

        #endregion
    }
}