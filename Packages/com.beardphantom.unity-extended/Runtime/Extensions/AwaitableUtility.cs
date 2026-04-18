#if UNITY_6000_0_OR_NEWER
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Unity.Jobs;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides static utility methods for <see cref="Awaitable" />.
    /// </summary>
    public static partial class AwaitableUtility
    {
        /// <summary>
        /// Provides a pre-completed <see cref="Awaitable" /> instance, which resolves immediately when awaited.
        /// </summary>
        public static Awaitable Completed => GetCompleted();

        /// <summary>
        /// Converts a <see cref="Task" /> into an <see cref="Awaitable" /> that can be awaited.
        /// </summary>
        /// <param name="task">
        /// The <see cref="Task" /> to convert to an awaitable.
        /// </param>
        /// <returns>
        /// An <see cref="Awaitable" /> that resolves when the task is completed.
        /// </returns>
        public static async Awaitable ToAwaitable(this Task task)
        {
            await task.ConfigureAwait(false);
        }

        /// <summary>
        /// Converts a <see cref="ValueTask" /> to an <see cref="Awaitable" />.
        /// </summary>
        /// <param name="task">
        /// The <see cref="ValueTask" /> to convert.
        /// </param>
        /// <returns>
        /// An <see cref="Awaitable" /> that resolves when the <see cref="ValueTask" /> is completed.
        /// </returns>
        public static async Awaitable ToAwaitable(this ValueTask task)
        {
            await task.ConfigureAwait(false);
        }

        /// <summary>
        /// Forgets the provided <see cref="Awaitable" />, allowing it to proceed
        /// without awaiting its completion.
        /// </summary>
        /// <param name="awaitable">
        /// The <see cref="Awaitable" /> to execute.
        /// </param>
        /// <param name="silenceOperationCancelledExceptions">
        /// If set to true, exceptions of type
        /// <see cref="OperationCanceledException" /> will be suppressed.
        /// </param>
        public static void Forget(this Awaitable awaitable, bool silenceOperationCancelledExceptions = true)
        {
            _ = ForgetImpl(awaitable, silenceOperationCancelledExceptions);
        }

        /// <summary>
        /// Forgets the provided <see cref="Awaitable{T}" />, allowing it to proceed
        /// without awaiting its completion.
        /// </summary>
        /// <param name="awaitable">
        /// The <see cref="Awaitable" /> to forget.
        /// </param>
        /// <param name="silenceOperationCancelledExceptions">
        /// If true, suppresses <see cref="OperationCanceledException" /> exceptions
        /// that may occur during the execution of the <see cref="Awaitable" />.
        /// </param>
        public static void Forget<T>(this Awaitable<T> awaitable, bool silenceOperationCancelledExceptions = true)
        {
            _ = ForgetImpl(awaitable, silenceOperationCancelledExceptions);
        }

        /// <summary>
        /// Waits for a specified number of real-time seconds to pass asynchronously.
        /// </summary>
        /// <param name="seconds">
        /// The number of seconds to wait in real-time. If the value is less than or equal to zero, the method returns immediately.
        /// </param>
        /// <param name="cancellationToken">
        /// A token that can be used to cancel the waiting operation. If cancellation is requested, an exception is thrown.
        /// </param>
        /// <returns>
        /// An <see cref="Awaitable" /> that resolves after the specified number of real-time seconds has passed.
        /// </returns>
        public static async Awaitable WaitForSecondsRealtimeAsync(float seconds, CancellationToken cancellationToken = default)
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

        /// <summary>
        /// Waits asynchronously until the specified predicate returns true.
        /// </summary>
        /// <param name="predicate">
        /// A function that evaluates to true when the wait condition is met. This function is evaluated repeatedly until it
        /// returns true.
        /// </param>
        /// <param name="cancellationToken">
        /// A token to observe while waiting, which can be used to cancel the wait operation.
        /// </param>
        /// <returns>
        /// An <see cref="Awaitable" /> that resolves once the predicate returns true.
        /// </returns>
        public static async Awaitable WaitUntilAsync(Func<bool> predicate, CancellationToken cancellationToken = default)
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

        /// <summary>
        /// Waits asynchronously while the specified condition evaluates to true.
        /// </summary>
        /// <param name="predicate">
        /// A function that represents the condition to evaluate.
        /// The wait operation continues until this function returns false.
        /// </param>
        /// <param name="cancellationToken">
        /// A token to observe while waiting, which can be used to cancel the wait operation.
        /// </param>
        /// <returns>
        /// An <see cref="Awaitable" /> that resolves when the specified condition evaluates to false or the operation is canceled.
        /// </returns>
        public static async Awaitable WaitWhileAsync(Func<bool> predicate, CancellationToken cancellationToken = default)
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

        /// <summary>
        /// Waits asynchronously for the specified number of frames to pass.
        /// </summary>
        /// <param name="frameCount">
        /// The number of frames to wait. Must be a non-negative value.
        /// </param>
        /// <param name="cancellationToken">
        /// A token to observe while waiting, which can be used to cancel the wait operation.
        /// </param>
        /// <returns>
        /// An <see cref="Awaitable" /> that resolves after the specified number of frames have passed.
        /// </returns>
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
        /// Waits asynchronously for a <see cref="JobHandle" /> to complete.
        /// </summary>
        /// <param name="jobHandle">
        /// The <see cref="JobHandle" /> to wait on.
        /// </param>
        /// <param name="complete">
        /// If true, completes the job handle after waiting for it to finish.
        /// </param>
        /// <param name="cancellationToken">
        /// A token to observe while waiting, which can be used to cancel the wait operation.
        /// </param>
        /// <returns>
        /// An <see cref="Awaitable" /> that resolves when the job handle is completed.
        /// </returns>
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

        /// <summary>
        /// Returns an <see cref="Awaitable" /> that is already completed.
        /// </summary>
        /// <returns>
        /// A completed Awaitable that immediately resolves when awaited.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private static async Awaitable GetCompleted() { }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

        // ReSharper disable Unity.PerformanceAnalysis
        private static async Awaitable ForgetImpl(Awaitable awaitable, bool silenceOperationCancelledExceptions)
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

        private static async Awaitable ForgetImpl<T>(Awaitable<T> awaitable, bool silenceOperationCancelledExceptions)
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

    /// <summary>
    /// Provides utility methods to work with <see cref="Awaitable{T}" />.
    /// </summary>
    public static class AwaitableUtility<T>
    {
        /// <summary>
        /// Returns an <see cref="Awaitable{T}" /> that is already completed with the default value of <typeparamref name="T" /> as
        /// the result.
        /// </summary>
        public static Awaitable<T> Completed => FromResult();

        /// <summary>
        /// Returns an <see cref="Awaitable{T}" /> that is already completed with the specified result.
        /// </summary>
        /// <param name="result">
        /// The result to use for the completed <see cref="Awaitable{T}" />. If not specified, the default
        /// value of <typeparamref name="T" /> is used.
        /// </param>
        /// <returns>An <see cref="Awaitable{T}" /> that is already completed with the specified result.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public static async Awaitable<T> FromResult(T result = default)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return result;
        }
    }
}
#endif