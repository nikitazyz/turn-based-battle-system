using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace StateMachineSystem
{
    public sealed class StateMachine : MonoBehaviour
    {
        private IState CurrentState { get; set; }

        private Dictionary<Type, IState> _states;
        private Dictionary<Type, StateTransition> _transitions;

        public StateMachine()
        {
            _states = new Dictionary<Type, IState>();
            _transitions = new Dictionary<Type, StateTransition>();
        }

        private void Update()
        {
            foreach (var stateTransition in _transitions)
            {
                if (stateTransition.Value.State && stateTransition.Key == CurrentState.GetType())
                {
                    ChangeState(stateTransition.Value.TransitTo);
                }
            }
        }

        public void AddState(Type stateType, IState state)
        {
            // if (!stateType.IsSubclassOf(typeof(IState)))
            // {
            //     throw new ArgumentException($"The {nameof(stateType)} doesn't derives from the {typeof(IState)}.", nameof(stateType));
            // }

            if (_states.ContainsKey(stateType))
            {
                throw new ArgumentException($"State machine already has {stateType}");
            }
            
            _states.Add(stateType, state);
        }

        public void AddState<TState>(TState state) where TState : IState => AddState(typeof(TState), state);

        public void ChangeState(Type stateType)
        {
            // if (!stateType.IsSubclassOf(typeof(IState)))
            // {
            //     throw new ArgumentException($"The {nameof(stateType)} doesn't derives from the {typeof(IState)}.", nameof(stateType));
            // }
            
            if (!_states.ContainsKey(stateType))
            {
                throw new ArgumentException($"State machine doesn't have {stateType}");
            }

            SwitchState(_states[stateType]);
        }

        public void ChangeState<TState>() where TState : IState => ChangeState(typeof(TState));

        private void SwitchState([NotNull] IState state)
        {
            CurrentState?.Exit();
            CurrentState = state;
            CurrentState.Enter();
        }

        public void AddTransition(Type transitFrom, Type transitTo, Func<bool> condition)
        {
            bool containsTransitFrom = _states.ContainsKey(transitFrom);
            bool containsTransitTo = _states.ContainsKey(transitTo);
            if (!containsTransitFrom)
            {
                throw new ArgumentException($"State machine doesn't have {transitFrom}.");
            }

            if (!containsTransitTo)
            {
                throw new AggregateException($"State machine doesn't have {transitTo}.");
            }

            _transitions.Add(transitFrom, new StateTransition(transitTo, condition));
        }
    }
}
