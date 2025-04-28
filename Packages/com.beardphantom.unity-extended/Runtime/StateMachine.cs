using System;
using System.Linq;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public class StateMachine : MonoBehaviour
    {
        private bool _isTransitioning;

        public State CurrentState { get; private set; }

        [field: SerializeField]
        [field: ReferenceDrawer]
        [field: SerializeReference]
        private State[] States { get; set; }

        public void ChangeStateAsync<T>()
        {
            ChangeState(typeof(T));
        }

        public void ChangeState(Type type)
        {
            _isTransitioning = true;
            State nextState = States.Single(type.IsInstanceOfType);
            CurrentState.Exit();
            CurrentState = nextState;
            CurrentState.Enter();
            _isTransitioning = false;
        }

        private void Awake()
        {
            CurrentState = NullState.Instance;
            ChangeState(States[0].GetType());
        }

        private void Update()
        {
            if (_isTransitioning)
            {
                return;
            }

            CurrentState.Tick();
        }

        [Serializable]
        public abstract class State
        {
            public virtual void Enter() { }

            public virtual void Exit() { }

            public virtual void Tick() { }
        }

        private class NullState : State
        {
            public static readonly NullState Instance = new();
        }
    }
}