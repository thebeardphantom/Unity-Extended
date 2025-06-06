﻿using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityExtended.Editor
{
    [CustomEditor(typeof(AudioCueAsset))]
    public class AudioCueAssetEditor : UnityEditor.Editor
    {
        private AudioSource _lastPlayingSource;

        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var cueAsset = (AudioCueAsset)target;
            bool cachedEnabled = GUI.enabled;
            GUI.enabled = cueAsset.Audio != null && cueAsset.Audio.Any(a => a.Clip != null);
            Color cachedColor = GUI.color;
            if (_lastPlayingSource == null)
            {
                GUI.color = Color.green;
                if (GUILayout.Button("Play"))
                {
                    _lastPlayingSource = EditorAudioUtility.Play(cueAsset);
                }
            }
            else
            {
                GUI.color = Color.red;
                if (GUILayout.Button("Stop"))
                {
                    _lastPlayingSource.Stop();
                }
            }

            GUI.color = cachedColor;
            GUI.enabled = cachedEnabled;

            DrawPropertiesExcluding(serializedObject, "m_Script");
            serializedObject.ApplyModifiedProperties();
            if (_lastPlayingSource != null)
            {
                Repaint();
            }
        }

        private void OnDisable()
        {
            if (_lastPlayingSource != null)
            {
                _lastPlayingSource.Stop();
            }
        }
    }
}