using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class LiteEventUtility
    {
        public static Awaitable WaitForInvocationAsync(this LiteEvent liteEvent)
        {
            var completionSource = new AwaitableCompletionSource();

            void WaitForComplete()
            {
                liteEvent.Event -= WaitForComplete;
                completionSource.TrySetResult();
            }

            liteEvent.Event += WaitForComplete;
            return completionSource.Awaitable;
        }

        public static Awaitable<TArgs> WaitForInvocationAsync<TArgs>(this LiteEvent<TArgs> liteEvent) where TArgs : struct
        {
            var completionSource = new AwaitableCompletionSource<TArgs>();

            void WaitForComplete(in TArgs args)
            {
                liteEvent.Event -= WaitForComplete;
                completionSource.TrySetResult(args);
            }

            liteEvent.Event += WaitForComplete;
            return completionSource.Awaitable;
        }
    }
}