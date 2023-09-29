using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class ParticleSystemExtensions
    {
        #region Methods

        public static async UniTask PlayAsync(this ParticleSystem particleSystem, bool withChildren = false)
        {
            particleSystem.Play(withChildren);

            bool IsPlaying()
            {
                return particleSystem.isPlaying;
            }

            await UniTask.WaitUntil(IsPlaying);
            await UniTask.WaitWhile(IsPlaying);
        }

        #endregion
    }
}