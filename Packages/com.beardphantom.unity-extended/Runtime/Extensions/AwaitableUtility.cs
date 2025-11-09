#if UNITY_6000_0_OR_NEWER
using System;
using System.Threading;
using System.Threading.Tasks;
using Unity.Jobs;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static partial class AwaitableUtility
    {
        public static async Awaitable ToAwaitable(this Task task)
        {
            // Avoid context capture; Awaitable resumes on Unity’s player loop when awaited.
            await task.ConfigureAwait(false);
        }

        public static async Awaitable ToAwaitable(this ValueTask task)
        {
            // Same rationale.
            await task.ConfigureAwait(false);
        }

        public static async Awaitable<T> FromResult<T>(T result)
        {
            // Keep a minimal await to avoid CS1998 warnings; already completed.
            await Task.CompletedTask.ConfigureAwait(false);
            return result;
        }

        public static async Awaitable GetCompleted()
        {
            // Already-completed awaitable.
            await Task.CompletedTask.ConfigureAwait(false);
        }

        public static void Forget(this Awaitable awaitable, bool silenceOperationCancelledExceptions = true)
        {
            _ = ForgetImpl(awaitable, silenceOperationCancelledExceptions);
        }

        public static void Forget<T>(this Awaitable<T> awaitable, bool silenceOperationCancelledExceptions = true)
        {
            _ = ForgetImpl(awaitable, silenceOperationCancelledExceptions);
        }

        public static async Awaitable WaitForSecondsRealtime(float seconds, CancellationToken cancellationToken = default)
        {
            if (seconds <= 0f)
            {
                return;
            }

            cancellationToken.ThrowIfCancellationRequested();
            double end = Time.realtimeSinceStartupAsDouble + seconds;
            while (Time.realtimeSinceStartupAsDouble < end)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Awaitable.NextFrameAsync(cancellationToken);
            }
        }

        public static async Awaitable WaitUntil(Func<bool> predicate, CancellationToken cancellationToken = default)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            cancellationToken.ThrowIfCancellationRequested();
            while (!predicate())
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Awaitable.NextFrameAsync(cancellationToken);
            }
        }

        public static async Awaitable WaitWhile(Func<bool> predicate, CancellationToken cancellationToken = default)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            cancellationToken.ThrowIfCancellationRequested();
            while (predicate())
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Awaitable.NextFrameAsync(cancellationToken);
            }
        }

        public static async Awaitable WaitForFramesAsync(int frameCount, CancellationToken cancellationToken = default)
        {
            if (frameCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(frameCount), frameCount, "Frame count cannot be negative.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            for (var i = 0; i < frameCount; i++)
            {
                await Awaitable.NextFrameAsync(cancellationToken);
            }
        }

        /// <summary>
        /// Await a JobHandle. Optionally call Complete() after it’s done.
        /// </summary>
        public static async Awaitable WaitAsync(
            this JobHandle jobHandle,
            bool complete = true,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Poll once per frame; avoids busy-wait and plays nice with the player loop.
            while (!jobHandle.IsCompleted)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Awaitable.NextFrameAsync(cancellationToken);
            }

            if (complete)
            {
                jobHandle.Complete();
            }
        }

        private static async Task ForgetImpl(Awaitable awaitable, bool silenceOperationCancelledExceptions)
        {
            try
            {
                await awaitable;
            }
            catch (OperationCanceledException) when (silenceOperationCancelledExceptions) { }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private static async Task ForgetImpl<T>(Awaitable<T> awaitable, bool silenceOperationCancelledExceptions)
        {
            try
            {
                await awaitable;
            }
            catch (OperationCanceledException) when (silenceOperationCancelledExceptions) { }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
#endif