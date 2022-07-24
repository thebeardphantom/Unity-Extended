using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    [RequireComponent(typeof(AudioSource))]
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
            if (PlayOnAwake)
            {
                Play(new AudioCueAsset.PlayArgs(Loop, position: PlayAtPosition ? transform.position : null));
            }
        }

        private void OnValidate()
        {
            AudioSource = GetComponent<AudioSource>();
            AudioSource.hideFlags = HideFlags.NotEditable;
        }

        #endregion
    }
}