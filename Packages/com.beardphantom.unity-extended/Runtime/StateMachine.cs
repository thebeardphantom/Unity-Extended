using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public class StateMachine : MonoBehaviour
    {
        #region Types

        [Serializable]
        public abstract class State
        {
            #region Methods

            public virtual UniTask EnterAsync(CancellationToken cancellationToken)
            {
                return default;
            }

            public virtual UniTask ExitAsync(CancellationToken cancellationToken)
            {
                return default;
            }

            public virtual void Tick() { }

            #endregion
        }

        private class NullState : State
        {
            #region Fields

            public static readonly NullState Instance = new();

            #endregion
        }

        #endregion

        #region Fields

        private bool _isTransitioning;

        private CancellationTokenSource _changeStateCancelTokenSrc = new();

        #endregion

        #region Properties

        public State CurrentState { get; private set; }

        [field: SerializeField]
        [field: ReferenceDrawer]
        [field: SerializeReference]
        private State[] States { get; set; }

        #endregion

        #region Methods

        public UniTask ChangeStateAsync<T>(CancellationToken cancellationToken = default)
        {
            return ChangeStateAsync(typeof(T), cancellationToken);
        }

        public async UniTask ChangeStateAsync(Type type, CancellationToken cancellationToken = default)
        {
            _changeStateCancelTokenSrc?.Cancel();
            _changeStateCancelTokenSrc =
                CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, destroyCancellationToken);

            cancellationToken = _changeStateCancelTokenSrc.Token;

            _isTransitioning = true;
            var nextState = States.SingleOrDefault(type.IsInstanceOfType);
            await CurrentState.ExitAsync(cancellationToken);
            CurrentState = nextState;
            await CurrentState.EnterAsync(cancellationToken);
            _isTransitioning = false;
        }

        private void Awake()
        {
            CurrentState = NullState.Instance;
            EnterFirstStateAsync().Forget();
        }

        private async UniTaskVoid EnterFirstStateAsync()
        {
            await ChangeStateAsync(States[0].GetType());
        }

        private void Update()
        {
            if (_isTransitioning)
            {
                return;
            }

            CurrentState.Tick();
        }

        #endregion
    }
}