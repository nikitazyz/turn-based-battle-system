using System.Linq;
using CoroutineManagement;
using Dices;
using Enemies;
using Guardians;
using Initialization;
using PlayerSystem;
using StateMachineSystem;
using StateMachineSystem.BattleStateMachine;
using UnityEngine;
using UserInterface.UIBootstrappers;

namespace Core
{
    public class BattleBootstrapper : MonoBehaviour, IInitializable<BattleStatus>
    {
        [SerializeField] private BattleUIBootstrapper _battleUIBootstrapper;
        private StateMachine _stateMachine;
        private BattleStatus _battleStatus;
        private Battle _battle;

        private AttackProcessor _attackProcessor;
        private CoroutineService _coroutineService;

        private EnemyAttack _enemyAttack;

        public void Initialize(BattleStatus battleStatus)
        {
            _stateMachine = new StateMachine();
            _battleStatus = battleStatus;
            var guardianList = new GuardianList(new[]
            {
                GuardianCellFactory.CreateGuardianCell(_battleStatus.Guardians[0]),
                GuardianCellFactory.CreateGuardianCell(_battleStatus.Guardians[1]),
                GuardianCellFactory.CreateGuardianCell(_battleStatus.Guardians[2]),
                GuardianCellFactory.CreateGuardianCell(_battleStatus.Guardians[3]),
            });
            _coroutineService = CoroutineService.CreateInstance();

            var enemyList = new EnemyList(battleStatus.Enemies.Select((enemy, _) => new BattleEnemy(enemy, 5)).ToArray());
            _enemyAttack = new EnemyAttack(_coroutineService, enemyList);

            _enemyAttack.AllEnemyAttacked += () => _stateMachine.ChangeState<UserMoveState>();
            
            var player = new BattlePlayer(battleStatus.Player, battleStatus.Player.MaxHealth,
                battleStatus.PlayerHealth);
            _attackProcessor = new AttackProcessor(enemyList, player);
            
            _battle = new Battle(_stateMachine, _battleStatus.GameSettings.Battle.MaxActions, guardianList, player, enemyList, _attackProcessor);

            _battleUIBootstrapper.Initialize(_battle);
            _stateMachine.AddState(new UserMoveState(guardianList));
            _stateMachine.AddState(new EnemyAttackState(_enemyAttack));
            _stateMachine.ChangeState<UserMoveState>();
            foreach (GuardianCell guardianCell in guardianList)
            {
                guardianCell.RerollDices();
            }
        }
    }
}