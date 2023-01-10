using System.Linq;
using Dices;
using Enemies;
using EventBusSystem;
using Guardians;
using Initialization;
using PlayerSystem;
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

        private AttackProcessor _attackProcessor;

        private void Awake()
        {
        }

        public void Initialize(BattleStatus battleStatus)
        {
            _battleStatus = battleStatus;
            var guardianList = new GuardianList(new[]
            {
                GuardianCellFactory.CreateGuardianCell(_battleStatus.Guardians[0], battleStatus.Dice, battleStatus.Dice, battleStatus.Dice),
                GuardianCellFactory.CreateGuardianCell(_battleStatus.Guardians[1], battleStatus.Dice, battleStatus.Dice, battleStatus.Dice),
                GuardianCellFactory.CreateGuardianCell(_battleStatus.Guardians[2], battleStatus.Dice, battleStatus.Dice, battleStatus.Dice),
                GuardianCellFactory.CreateGuardianCell(_battleStatus.Guardians[3], battleStatus.Dice, battleStatus.Dice, battleStatus.Dice),
            });

            var enemyList = new EnemyList(battleStatus.Enemies.Select((enemy, i) => new BattleEnemy(enemy, 30)).ToArray());
            var player = new BattlePlayer(battleStatus.Player, battleStatus.Player.MaxHealth,
                battleStatus.PlayerHealth);
            _attackProcessor = new AttackProcessor(enemyList, player);
            
            _battle = new Battle(_stateMachine, _battleStatus.GameSettings.Battle.MaxActions, guardianList, player);

            var endEvent = new EndMoveEventBus(_battle);
            var useEvent = new UseEventBus(_battle, _attackProcessor);
            var rerollEvent = new RerollEventBus(_battle);
            
            _battleUIBootstrapper.Initialize(_battle, endEvent, useEvent, rerollEvent);
            _stateMachine.AddState(new UserMoveState(guardianList, endEvent, useEvent, rerollEvent));
            _stateMachine.AddState(new EnemyAttackState());
            _stateMachine.ChangeState<UserMoveState>();
            foreach (GuardianCell guardianCell in guardianList)
            {
                guardianCell.RerollDices();
            }
        }
    }
}