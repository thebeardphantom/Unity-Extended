using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioCueSource : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioCueAsset _audioCue;

        #endregion

        #region Properties

        public AudioCueAsset AudioCue
        {
            get => _audioCue;
            set => _audioCue = value;
        }

        #endregion

        #region Methods

        public void Play(AudioCueAsset.PlayArgs args = default)
        {
            _audioCue.Play(_audioSource, args);
        }

        private void OnValidate()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.hideFlags = HideFlags.NotEditable;
        }

        #endregion
    }
}