using System.Linq;
using Guardians;
using PlayerSystem;
using UnityEngine;

namespace StateMachineSystem.BattleStateMachine
{
    public class UserMoveState : IState
    {
        private readonly GuardianList _guardianList;
        private readonly BattlePlayer _battlePlayer;

        public UserMoveState(GuardianList guardianList, BattlePlayer battlePlayer)
        {
            _guardianList = guardianList;
            _battlePlayer = battlePlayer;
        }
        
        public void Enter()
        {
            Debug.Log("User Move State");
            _battlePlayer.Health.TakeDamage(_battlePlayer.PoisonEffect.PoisonCounter--);
        }

        public void Exit()
        {
            foreach (var guardian in _guardianList)
            {
                guardian.ResetDices();
            }
        }
    }
}