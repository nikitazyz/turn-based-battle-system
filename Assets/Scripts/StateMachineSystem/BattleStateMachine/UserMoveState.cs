using EventBusSystem;
using Guardians;
using UnityEngine;

namespace StateMachineSystem.BattleStateMachine
{
    public class UserMoveState : IState
    {
        private readonly GuardianList _guardianList;
        private readonly EventBus[] _events;

        public UserMoveState(GuardianList guardianList, params EventBus[] events)
        {
            _guardianList = guardianList;
            _events = events;
        }
        
        public void Enter()
        {
            Debug.Log("User Move State");
            foreach (var guardianCell in _guardianList)
            {
                guardianCell.ResetDices();
            }

            foreach (var eventBus in _events)
            {
                eventBus.Enabled = true;
            }
        }

        public void Exit()
        {
            foreach (var eventBus in _events)
            {
                eventBus.Enabled = false;
            }

        }
    }
}