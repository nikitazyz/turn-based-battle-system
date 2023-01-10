using System;
using Guardians;
using PlayerSystem;
using StateMachineSystem;
using StateMachineSystem.BattleStateMachine;
using UnityEngine;

namespace Core
{
    public class Battle
    {
        public event Action Acted;
        
        private readonly StateMachine _stateMachine;
        private readonly GuardianList _guardianList;
        public int MaxActions { get; }
        public int Actions { get; private set; }
        public GuardianList GuardianList => _guardianList;
        public BattlePlayer Player { get; }

        public Battle(StateMachine stateMachine, int maxActions, GuardianList guardianList, BattlePlayer player)
        {
            _stateMachine = stateMachine;
            _guardianList = guardianList;
            Player = player;
            MaxActions = maxActions;
            Actions = maxActions;
            Debug.Log(Actions);
        }

        public void EndMove()
        {
            Actions = MaxActions;
            _stateMachine.ChangeState<EnemyAttackState>();
        }

        public void Act()
        {
            if (Actions == 0)
            {
                throw new InvalidOperationException("No actions left");
            }
            Actions--;
            Acted?.Invoke();
        }
    }
}
