using UnityEngine;

namespace StateMachineSystem.BattleStateMachine
{
    public class EnemyAttackState : IState
    {
        public EnemyAttackState()
        {
            
        }
        
        public void Enter()
        {
            Debug.Log("EnemyAttackState");
        }

        public void Exit()
        {
            
        }
    }
}