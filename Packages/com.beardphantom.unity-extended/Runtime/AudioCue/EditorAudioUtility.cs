#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    [InitializeOnLoad]
    public static class EditorAudioUtility
    {
        private static readonly List<AudioSource> _playingSources = new();

        static EditorAudioUtility()
        {
            EditorApplication.update -= EditorUpdate;
            EditorApplication.update += EditorUpdate;

            EditorApplication.playModeStateChanged += OnPlaymodeStateChanged;
        }

        public static AudioSource Play(AudioCueAsset cue)
        {
            if (cue == null)
            {
                return null;
            }

            var source = GetAudioSource();
            var camera = SceneView.GetAllSceneCameras().First().transform;
            var position = camera.position + camera.forward * 5f;
            cue.Play(source, new AudioCueAsset.PlayArgs(position: position));
            return source;
        }

        private static void OnPlaymodeStateChanged(PlayModeStateChange obj)
        {
            for (var i = _playingSources.Count - 1; i >= 0; i--)
            {
                var source = _playingSources[i];
                if (source != null)
                {
                    Object.DestroyImmediate(source.gameObject);
                }

                _playingSources.RemoveAt(i);
            }
        }

        private static AudioSource GetAudioSource()
        {
            var audioSource = EditorUtility.CreateGameObjectWithHideFlags(
                    "AUDIOSOURCE",
                    HideFlags.HideAndDontSave,
                    typeof(AudioSource))
                .GetComponent<AudioSource>();
            _playingSources.Add(audioSource);
            return audioSource;
        }

        private static void EditorUpdate()
        {
            for (var i = _playingSources.Count - 1; i >= 0; i--)
            {
                var source = _playingSources[i];
                var nullSource = source == null;
                if (!nullSource && source.isPlaying)
                {
                    continue;
                }

                if (!nullSource)
                {
                    Object.DestroyImmediate(source.gameObject);
                }

                _playingSources.RemoveAt(i);
            }
        }
    }
}

#endif