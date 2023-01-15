using Enemies;
using UnityEngine;

namespace StateMachineSystem.BattleStateMachine
{
    public class EnemyAttackState : IState
    {
        private readonly EnemyAttack _enemyAttack;

        public EnemyAttackState(EnemyAttack enemyAttack)
        {
            _enemyAttack = enemyAttack;
        }

        public void Enter()
        {
            _enemyAttack.StartEnemyAttack();
        }

        public void Exit()
        {
            _enemyAttack.StopEnemyAttack();
        }
    }
}