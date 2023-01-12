using UnityEngine;

namespace StateMachineSystem.BattleStateMachine
{
    public class EnemyAttackState : IState
    {
        public void Enter()
        {
            Debug.Log("EnemyAttackState");
        }

        public void Exit()
        {
            
        }
    }
}