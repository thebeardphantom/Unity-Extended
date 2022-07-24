using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioCueSource : MonoBehaviour
    {
        #region Properties

        [field: SerializeField]
        public AudioCueAsset AudioCueAsset { get; private set; }

        [field: SerializeField]
        private AudioSource AudioSource { get; set; }

        [field: SerializeField]
        private bool PlayOnAwake { get; set; }

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
                Play();
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