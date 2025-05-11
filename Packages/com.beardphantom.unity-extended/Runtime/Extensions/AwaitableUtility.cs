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
            await task;
        }

        public static async Awaitable ToAwaitable(this ValueTask task)
        {
            await task;
        }

        public static async Awaitable<T> FromResult<T>(T result)
        {
            await GetCompleted();
            return result;
        }

        public static async Awaitable GetCompleted()
        {
            await Task.CompletedTask;
        }

        public static async void Forget(this Awaitable awaitable)
        {
            try
            {
                await awaitable;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static async Awaitable WaitForSecondsRealtime(float seconds, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            double timeEnd = Time.realtimeSinceStartupAsDouble + seconds;
            while (Time.realtimeSinceStartupAsDouble < timeEnd)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }
        }

        public static async Awaitable WaitUntil(Func<bool> predicate, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            while (!predicate())
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }
        }

        public static async Awaitable WaitWhile(Func<bool> predicate, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            while (predicate())
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }
        }

        public static async Awaitable WaitAsync(this JobHandle jobHandle, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            while (!jobHandle.IsCompleted)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }

            jobHandle.Complete();
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

        public static async Awaitable WaitForCompleteAsync(this JobHandle jobHandle, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            while (!jobHandle.IsCompleted)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }
        }
    }
}
#endif