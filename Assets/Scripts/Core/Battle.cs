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
        
        //FMOD events
        private static readonly string DiceRerollAudioEvent  = "event:/DiceReroll";
        private static readonly string EndMoveAudioEvent  = "event:/EndMove";
        private static readonly string DiceUsedAudioEvent  = "event:/DiceUsed";


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
            FMODUnity.RuntimeManager.PlayOneShot(EndMoveAudioEvent);
        }

        public void UseDice(BattleDice battleDice)
        {
            if (!CanAct())
            {
                return;
            }
            _attackProcessor.UseDice(battleDice);
            Act();
            FMODUnity.RuntimeManager.PlayOneShot(DiceUsedAudioEvent);
        }

        public void RerollDices(GuardianCell guardianCell)
        {
            if (!CanAct())
            {
                return;
            }
            guardianCell.RerollDices();
            Act();
            FMODUnity.RuntimeManager.PlayOneShot(DiceRerollAudioEvent);
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
