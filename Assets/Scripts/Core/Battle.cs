using System;
using AttackSystem;
using AudioSystem;
using Dices;
using Enemies;
using FMODUnity;
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
        
        private readonly PlayerAttackProcessor _attackProcessor;
        private readonly EventReferencesAsset _eventReferencesAsset;


        public Battle(StateMachine stateMachine, int maxActions, GuardianList guardianList, BattlePlayer player, EnemyList enemyList, PlayerAttackProcessor attackProcessor)
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
            FMODUnity.RuntimeManager.PlayOneShot(_eventReferencesAsset.MoveEvents.EndMove);
        }

        public void UseDice(BattleDice battleDice)
        {
            if (!CanAct())
            {
                FMODUnity.RuntimeManager.PlayOneShot(_eventReferencesAsset.MoveEvents.NoActions);
                return;
            }
            _attackProcessor.UseDice(battleDice);
            Act();
            FMODUnity.RuntimeManager.PlayOneShot(_eventReferencesAsset.DiceEvents.Use);
        }

        public void RerollDices(GuardianCell guardianCell)
        {
            if (StateMachine.CurrentState != typeof(UserMoveState) || guardianCell.RerollAmount <= 0)
            {
                FMODUnity.RuntimeManager.PlayOneShot(_eventReferencesAsset.MoveEvents.NoActions);
                return;
            }
            guardianCell.RerollDices();
            FMODUnity.RuntimeManager.PlayOneShot(_eventReferencesAsset.DiceEvents.Reroll);
        }

        public bool CanAct()
        {
            return StateMachine.CurrentState == typeof(UserMoveState) && Actions != 0;
        }

        public void Act()
        {
            if (!CanAct())
            {
                return;
            }
            Actions--;
            Acted?.Invoke();
        }
    }
}
