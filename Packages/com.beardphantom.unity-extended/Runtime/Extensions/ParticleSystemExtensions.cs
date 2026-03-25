using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides extension methods for Unity's <see cref="ParticleSystem"/> class.
    /// </summary>
    public static class ParticleSystemExtensions
    {
        /// <summary>
        /// Plays the specified ParticleSystem asynchronously and waits until it has stopped playing.
        /// </summary>
        /// <param name="particleSystem">The ParticleSystem to play.</param>
        /// <param name="withChildren">
        /// If true, the ParticleSystem and any child ParticleSystems are played.
        /// If false, only the specified ParticleSystem is played.
        /// </param>
        /// <returns>An awaitable task that completes once the ParticleSystem has stopped playing.</returns>
        public static async Awaitable PlayAsync(this ParticleSystem particleSystem, bool withChildren = false)
        {
            particleSystem.Play(withChildren);

            while (!particleSystem.isPlaying)
            {
                await Awaitable.NextFrameAsync();
            }

            while (particleSystem.isPlaying)
            {
                await Awaitable.NextFrameAsync();
            }
        }
    }
}