#if UNITASK_SUPPORT
using Cysharp.Threading.Tasks;

namespace BeardPhantom.UnityExtended
{
    public partial class LiteEvent
    {
        #region Methods

        public UniTask WaitForInvocationAsync()
        {
            // TODO: Use AutoResetUniTaskCompletionSource?
            var completionSource = new UniTaskCompletionSource();

            void WaitForComplete()
            {
                Event -= WaitForComplete;
                completionSource.TrySetResult();
            }

            Event += WaitForComplete;
            return completionSource.Task;
        }

        #endregion
    }

    public sealed partial class LiteEvent<TArgs>
    {
        #region Methods

        public UniTask<TArgs> WaitForInvocationAsync()
        {
            var completionSource = new UniTaskCompletionSource<TArgs>();

            void WaitForComplete(in TArgs args)
            {
                Event -= WaitForComplete;
                completionSource.TrySetResult(args);
            }

            Event += WaitForComplete;
            return completionSource.Task;
        }

        #endregion
    }
}
#endif