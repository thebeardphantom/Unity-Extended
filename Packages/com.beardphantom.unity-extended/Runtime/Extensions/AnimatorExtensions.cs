using System.Threading;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class AnimatorExtensions
    {
        public static async Awaitable PlayAsync(
            this Animator animator,
            int stateHash,
            CancellationToken cancellationToken = default)
        {
            bool IsPlaying()
            {
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                return stateInfo.shortNameHash == stateHash && stateInfo.normalizedTime < 1f;
            }

            CancellationToken destroyCancellationToken = animator.gameObject.GetDestroyCancellationToken();
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
            await AwaitableUtility.WaitUntil(IsPlaying, cancellationToken);
            await AwaitableUtility.WaitWhile(IsPlaying, cancellationToken);
        }
    }
}