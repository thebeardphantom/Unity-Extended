using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

#if UNITASK_SUPPORT
namespace BeardPhantom.UnityExtended
{
    public static class AnimatorExtensions
    {
        #region Methods

        public static async UniTask PlayAsync(
            this Animator animator,
            int stateHash,
            CancellationToken cancellationToken = default)
        {
            bool IsPlaying()
            {
                var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                return stateInfo.shortNameHash == stateHash && stateInfo.normalizedTime < 1f;
            }

            var destroyCancellationToken = animator.gameObject.GetDestroyCancellationToken();
            if (cancellationToken.Equals(default))
            {
                cancellationToken = destroyCancellationToken;
            }
            else
            {
                var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
                    destroyCancellationToken,
                    cancellationToken);
                cancellationToken = linkedTokenSource.Token;
            }

            animator.Play(stateHash, -1, 0f);
            await UniTask.WaitUntil(IsPlaying, cancellationToken: cancellationToken);
            await UniTask.WaitWhile(IsPlaying, cancellationToken: cancellationToken);
        }

        #endregion
    }
}
#endif