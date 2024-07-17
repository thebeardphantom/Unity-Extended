#if UNITASK_SUPPORT
using Cysharp.Threading.Tasks;

namespace BeardPhantom.UnityExtended
{
    public static class LiteEventUtility
    {
        public static UniTask WaitForInvocationAsync(this LiteEvent liteEvent)
        {
            // TODO: Use AutoResetUniTaskCompletionSource?
            var completionSource = new UniTaskCompletionSource();

            liteEvent.Event += WaitForComplete;
            return completionSource.Task;

            void WaitForComplete()
            {
                liteEvent.Event -= WaitForComplete;
                completionSource.TrySetResult();
            }
        }
        
        public static UniTask<TArgs> WaitForInvocationAsync<TArgs>(this LiteEvent<TArgs> liteEvent) where TArgs : struct
        {
            var completionSource = new UniTaskCompletionSource<TArgs>();

            void WaitForComplete(in TArgs args)
            {
                liteEvent.Event -= WaitForComplete;
                completionSource.TrySetResult(args);
            }

            liteEvent.Event += WaitForComplete;
            return completionSource.Task;
        }
    }
}
#endif