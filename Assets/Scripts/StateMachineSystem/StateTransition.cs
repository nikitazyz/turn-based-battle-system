using System;

namespace StateMachineSystem
{
    public class StateTransition
    {
        private readonly Func<bool> _condition;

        public bool State => _condition();
        public Type TransitTo { get; }
        

        public StateTransition(Type transitTo, Func<bool> condition)
        {
            TransitTo = transitTo;
            _condition = condition;
        }
    }
}