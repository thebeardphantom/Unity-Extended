using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class ParticleSystemExtensions
    {
        public static async Awaitable PlayAsync(this ParticleSystem particleSystem, bool withChildren = false)
        {
            particleSystem.Play(withChildren);

            bool IsPlaying()
            {
                return particleSystem.isPlaying;
            }

            await AwaitableUtility.WaitUntil(IsPlaying);
            await AwaitableUtility.WaitWhile(IsPlaying);
        }
    }
}