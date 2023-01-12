using Guardians;
using UnityEngine;

namespace StateMachineSystem.BattleStateMachine
{
    public class UserMoveState : IState
    {
        private readonly GuardianList _guardianList;

        public UserMoveState(GuardianList guardianList)
        {
            _guardianList = guardianList;
        }
        
        public void Enter()
        {
            Debug.Log("User Move State");
            foreach (var guardianCell in _guardianList)
            {
                guardianCell.ResetDices();
            }
        }

        public void Exit()
        {

        }
    }
}