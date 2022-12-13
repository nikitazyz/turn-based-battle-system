using Dices;
using EventBusSystem;
using Guardians;
using Initialization;
using StateMachineSystem;
using StateMachineSystem.BattleStateMachine;
using UnityEngine;
using UnityEngine.Serialization;
using UserInterface.UIBootstrappers;

namespace Core
{
    public class BattleBootstrapper : MonoBehaviour, IInitializable<BattleStatus>
    {
        [SerializeField] private BattleUIBootstrapper _battleUIBootstrapper;
        [SerializeField] private StateMachine _stateMachine;
        private BattleStatus _battleStatus;
        private Battle _battle;

        private void Awake()
        {
        }

        public void Initialize(BattleStatus battleStatus)
        {
            _battleStatus = battleStatus;
            var guardianList = new GuardianList(new[]
            {
                new GuardianCell(battleStatus.Guardians[0], new BattleDice(battleStatus.Dice),
                    new BattleDice(battleStatus.Dice), new BattleDice(battleStatus.Dice)),
                new GuardianCell(battleStatus.Guardians[1], new BattleDice(battleStatus.Dice),
                    new BattleDice(battleStatus.Dice), new BattleDice(battleStatus.Dice)),
                new GuardianCell(battleStatus.Guardians[2], new BattleDice(battleStatus.Dice),
                    new BattleDice(battleStatus.Dice), new BattleDice(battleStatus.Dice)),
                new GuardianCell(battleStatus.Guardians[3], new BattleDice(battleStatus.Dice),
                    new BattleDice(battleStatus.Dice), new BattleDice(battleStatus.Dice)),
            });
            _battle = new Battle(_stateMachine, _battleStatus.GameSettings.Battle.MaxActions, guardianList);

            var endEvent = new EndMoveEventBus(_battle);
            var useEvent = new UseEventBus(_battle);
            
            _battleUIBootstrapper.Initialize(_battle, endEvent, useEvent);
            _stateMachine.AddState(new UserMoveState(guardianList, endEvent, useEvent));
            _stateMachine.AddState(new EnemyAttackState());
            _stateMachine.ChangeState<UserMoveState>();
            foreach (GuardianCell guardianCell in guardianList)
            {
                guardianCell.RerollDices();
            }
        }
    }
}