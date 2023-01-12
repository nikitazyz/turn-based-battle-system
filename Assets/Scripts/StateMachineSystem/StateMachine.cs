using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace StateMachineSystem
{
    public sealed class StateMachine
    {
        public Action<Type> StateChanged;

        private IState _currentState;

        public Type CurrentState => _currentState.GetType();

        private Dictionary<Type, IState> _states;

        public StateMachine(params IState[] states)
        {
            _states = new Dictionary<Type, IState>();

            foreach (var state in states)
            {
                AddState(state);
            }
        }
        
        public StateMachine() : this(Array.Empty<IState>())
        {
        }

        public void AddState(IState state)
        {
            if (_states.ContainsKey(state.GetType()))
            {
                throw new ArgumentException($"State machine already has {state.GetType()}");
            }
            
            _states.Add(state.GetType(), state);
        }

        public void ChangeState(Type stateType)
        {
            if (!typeof(IState).IsAssignableFrom(stateType))
            {
                throw new ArgumentException($"The {nameof(stateType)} doesn't derives from the {typeof(IState)}.", nameof(stateType));
            }
            
            if (!_states.ContainsKey(stateType))
            {
                throw new ArgumentException($"State machine doesn't have {stateType}");
            }

            SwitchState(_states[stateType]);
            StateChanged?.Invoke(stateType);
        }

        public void ChangeState<TState>() where TState : IState => ChangeState(typeof(TState));

        private void SwitchState([NotNull] IState state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }
    }
}
