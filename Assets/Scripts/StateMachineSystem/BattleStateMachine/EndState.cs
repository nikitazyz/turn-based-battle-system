using UnityEngine;

namespace StateMachineSystem.BattleStateMachine
{
    public class EndState : IState
    {
        public void Enter()
        {
            Debug.Log("End state");
        }

        public void Exit()
        {
            
        }
    }
}