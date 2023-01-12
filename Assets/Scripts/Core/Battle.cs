using System;
using Dices;
using Enemies;
using Guardians;
using PlayerSystem;
using StateMachineSystem;
using StateMachineSystem.BattleStateMachine;

namespace Core
{
    public class Battle
    {
        public event Action Acted;
        
        public int MaxActions { get; }
        public int Actions { get; private set; }
        public GuardianList GuardianList { get; }
        public BattlePlayer Player { get; }
        public EnemyList EnemyList { get; }
        public StateMachine StateMachine { get; }
        

        private readonly AttackProcessor _attackProcessor;
        

        public Battle(StateMachine stateMachine, int maxActions, GuardianList guardianList, BattlePlayer player, EnemyList enemyList, AttackProcessor attackProcessor)
        {
            _attackProcessor = attackProcessor;
            StateMachine = stateMachine;
            GuardianList = guardianList;
            EnemyList = enemyList;
            Player = player;
            MaxActions = maxActions;
            Actions = maxActions;
        }

        public void EndMove()
        {
            if (StateMachine.CurrentState != typeof(UserMoveState))
            {
                return;
            }
            Actions = MaxActions;
            StateMachine.ChangeState<EnemyAttackState>();
        }

        public void UseDice(BattleDice battleDice)
        {
            if (StateMachine.CurrentState != typeof(UserMoveState))
            {
                return;
            }
            _attackProcessor.UseDice(battleDice);
            Act();
        }

        public void RerollDices(GuardianCell guardianCell)
        {
            if (StateMachine.CurrentState != typeof(UserMoveState))
            {
                return;
            }
            guardianCell.RerollDices();
            Act();
        }

        public void Act()
        {
            if (StateMachine.CurrentState != typeof(UserMoveState))
            {
                return;
            }
            if (Actions == 0)
            {
                throw new InvalidOperationException("No actions left");
            }
            Actions--;
            Acted?.Invoke();
        }
    }
}
