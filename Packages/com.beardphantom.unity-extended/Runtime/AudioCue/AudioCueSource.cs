using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BeardPhantom.UnityExtended
{
    [ExecuteAlways]
    public class AudioCueSource : MonoBehaviour
    {
        #region Properties

        [field: SerializeField]
        public AudioCueAsset AudioCueAsset { get; set; }

        [field: Header("Play on Awake Settings")]
        [field: SerializeField]
        public bool PlayOnAwake { get; set; }

        [field: SerializeField]
        public bool Loop { get; set; }

        [field: SerializeField]
        public bool PlayAtPosition { get; private set; }

        [field: HideInInspector]
        [field: SerializeField]
        private AudioSource AudioSource { get; set; }

        #endregion

        #region Methods

        public void Play(AudioCueAsset.PlayArgs args = default)
        {
            AudioCueAsset.Play(AudioSource, args);
        }

        private void Awake()
        {
            if (AudioSource == null)
            {
                if (Application.isPlaying)
                {
                    throw new Exception("No AudioSource created at edit time.");
                }

                AudioSource = gameObject.AddComponent<AudioSource>();
                AudioSource.hideFlags = HideFlags.HideInInspector | HideFlags.NotEditable;
            }

            if (PlayOnAwake)
            {
                Play(new AudioCueAsset.PlayArgs(Loop, position: PlayAtPosition ? transform.position : null));
            }
        }

        private void OnDestroy()
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                Destroy(AudioSource);
            }
            else if (!EditorApplication.isPlayingOrWillChangePlaymode)
            {
                AudioSource = null;
                var allCueSources = GetComponents<AudioCueSource>();
                var allSources = GetComponents<AudioSource>();
                foreach (var audioSource in allSources)
                {
                    var didFind = false;
                    foreach (var cueSource in allCueSources)
                    {
                        if (cueSource.AudioSource == audioSource)
                        {
                            didFind = true;
                        }
                    }

                    if (!didFind)
                    {
                        Debug.Log($"Destroying hidden AudioSource: {audioSource}");
                        DestroyImmediate(audioSource);
                    }
                }
            }
#else
            Destroy(AudioSource);
#endif
        }

        #endregion
    }
}