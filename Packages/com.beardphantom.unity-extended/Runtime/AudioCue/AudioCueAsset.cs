using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Represents an audio event. A clip will be randomly selected on playback, using the specified AudioSourceSettings
    /// </summary>
    [CreateAssetMenu(menuName = "CUSTOM/" + nameof(AudioCueAsset))]
    public class AudioCueAsset : ScriptableObject
    {
        [SerializeField]
        private AudioSourceSettings _settings;

        [NonReorderable]
        [SerializeField]
        private List<AudioData> _audio;

        public List<AudioData> Audio => _audio;

        /// <summary>
        /// Play cue using source.
        /// </summary>
        public AudioClip Play(AudioSource audioSrc, in PlayArgs args = default)
        {
            Assert.IsTrue(Audio.Count > 0, $"No assigned audio for {name}");
            _settings.ApplyTo(audioSrc);

            var index = Audio.ChooseIndexFromWeighted();
            Assert.IsFalse(index < 0, "index < 0");

            var audio = Audio[index];
            Assert.IsNotNull(audio.Clip, $"Audio entry {index} for {name} has no assigned clip");

            if (args.OneShot)
            {
                audioSrc.PlayOneShot(audio.Clip);
            }
            else
            {
                if (args.Position.HasValue)
                {
                    audioSrc.transform.position = args.Position.Value;
                }

                audioSrc.volume = 1f + audio.VolumeOffset;
                audioSrc.loop = args.Loop;
                audioSrc.clip = audio.Clip;
            }

            audioSrc.Play();
            return audioSrc.clip;
        }

        [Serializable]
        public class AudioData : IWeightedChoice
        {
            [SerializeField]
            [FormerlySerializedAs("Clip")]
            private AudioClip _clip;

            [SerializeField]
            [FormerlySerializedAs("VolumeOffset")]
            private float _volumeOffset;

            [SerializeField]
            private int _weight = 1;

            public AudioClip Clip => _clip;

            public float VolumeOffset => _volumeOffset;

            /// <inheritdoc />
            int IWeightedChoice.Weight => _weight;

            public static AudioData Create(AudioClip clip)
            {
                return new AudioData
                {
                    _weight = 1,
                    _clip = clip,
                };
            }
        }

        public readonly struct PlayArgs
        {
            public readonly bool Loop;

            public readonly bool OneShot;

            public readonly Vector3? Position;

            public PlayArgs(bool loop = false, bool oneShot = false, Vector3? position = default)
            {
                Loop = loop;
                OneShot = oneShot;
                Position = position;
            }
        }
    }
}