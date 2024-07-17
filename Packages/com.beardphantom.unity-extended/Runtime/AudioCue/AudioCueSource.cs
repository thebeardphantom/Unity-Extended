using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public class AudioCueSource : MonoBehaviour
    {
        [field: SerializeField]
        public AudioCueAsset AudioCueAsset { get; set; }

        [field: Header("Play on Awake Settings")]
        [field: SerializeField]
        public bool PlayOnAwake { get; set; }

        [field: SerializeField]
        public bool Loop { get; set; }

        [field: SerializeField]
        public bool PlayAtPosition { get; private set; }

        private AudioSource AudioSource { get; set; }

        public void Play(AudioCueAsset.PlayArgs args = default)
        {
            AudioCueAsset.Play(AudioSource, args);
        }

        private void Awake()
        {
            AudioSource = gameObject.AddComponent<AudioSource>();
            AudioSource.hideFlags = HideFlags.HideInInspector | HideFlags.NotEditable;

            if (PlayOnAwake)
            {
                Play(new AudioCueAsset.PlayArgs(Loop, position: PlayAtPosition ? transform.position : null));
            }
        }

        private void OnDestroy()
        {
            Destroy(AudioSource);
        }
    }
}