using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class LiteEventUtility
    {
        public static Awaitable WaitForInvocationAsync(this LiteEvent liteEvent)
        {
            var completionSource = new AwaitableCompletionSource();

            liteEvent.Event += WaitForComplete;
            return completionSource.Awaitable;

            void WaitForComplete()
            {
                liteEvent.Event -= WaitForComplete;
                completionSource.TrySetResult();
            }
        }

        public static Awaitable<TArgs> WaitForInvocationAsync<TArgs>(this LiteEvent<TArgs> liteEvent) where TArgs : struct
        {
            var completionSource = new AwaitableCompletionSource<TArgs>();

            liteEvent.Event += WaitForComplete;
            return completionSource.Awaitable;

            void WaitForComplete(in TArgs args)
            {
                liteEvent.Event -= WaitForComplete;
                completionSource.TrySetResult(args);
            }
        }
    }
}