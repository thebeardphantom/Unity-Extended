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
        private static readonly List<AudioSource> s_playingSources = new();

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

            AudioSource source = GetAudioSource();
            Transform camera = SceneView.GetAllSceneCameras().First().transform;
            Vector3 position = camera.position + camera.forward * 5f;
            cue.Play(source, new AudioCueAsset.PlayArgs(position: position));
            return source;
        }

        private static void OnPlaymodeStateChanged(PlayModeStateChange obj)
        {
            for (int i = s_playingSources.Count - 1; i >= 0; i--)
            {
                AudioSource source = s_playingSources[i];
                if (source != null)
                {
                    Object.DestroyImmediate(source.gameObject);
                }

                s_playingSources.RemoveAt(i);
            }
        }

        private static AudioSource GetAudioSource()
        {
            var audioSource = EditorUtility.CreateGameObjectWithHideFlags(
                    "AUDIOSOURCE",
                    HideFlags.HideAndDontSave,
                    typeof(AudioSource))
                .GetComponent<AudioSource>();
            s_playingSources.Add(audioSource);
            return audioSource;
        }

        private static void EditorUpdate()
        {
            for (int i = s_playingSources.Count - 1; i >= 0; i--)
            {
                AudioSource source = s_playingSources[i];
                bool nullSource = source == null;
                if (!nullSource && source.isPlaying)
                {
                    continue;
                }

                if (!nullSource)
                {
                    Object.DestroyImmediate(source.gameObject);
                }

                s_playingSources.RemoveAt(i);
            }
        }
    }
}

#endif