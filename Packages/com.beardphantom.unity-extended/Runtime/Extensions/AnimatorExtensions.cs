using System.Threading;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides extension methods for working with the <see cref="Animator" /> class.
    /// </summary>
    public static class AnimatorExtensions
    {
        /// <summary>
        /// Plays the specified animation state on the given <see cref="Animator" /> asynchronously.
        /// The method waits for the animation to start playing and continues until it finishes.
        /// </summary>
        /// <param name="animator">
        /// The <see cref="Animator" /> that will play the animation state.
        /// </param>
        /// <param name="stateHash">
        /// The hash of the animation state to play.
        /// </param>
        /// <param name="cancellationToken">
        /// An optional <see cref="CancellationToken" /> that can be used to cancel the operation.
        /// If no token is provided, the method will use a cancellation token tied to the lifecycle of the
        /// <see cref="GameObject" />.
        /// </param>
        /// <returns>
        /// An awaitable operation that completes when the animation has finished playing or is canceled.
        /// </returns>
        public static async Awaitable PlayAsync(
            this Animator animator,
            int stateHash,
            CancellationToken cancellationToken = default)
        {
            CancellationToken destroyCancellationToken = animator.gameObject.GetDestroyCancellationToken();
            if (cancellationToken.Equals(CancellationToken.None))
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
            await AwaitableUtility.WaitUntilAsync(IsPlaying, cancellationToken);
            await AwaitableUtility.WaitWhileAsync(IsPlaying, cancellationToken);
            return;

            bool IsPlaying()
            {
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                return stateInfo.shortNameHash == stateHash && stateInfo.normalizedTime < 1f;
            }
        }
    }
}