using System;
using System.Linq;
using AttackSystem;
using CoroutineManagement;
using Dices;
using Enemies;
using FMOD.Studio;
using Guardians;
using HealthSystem;
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

        private PlayerAttackProcessor _attackProcessor;
        private EnemyAttackProcessor _enemyAttackProcessor;
        private CoroutineService _coroutineService;

        private EnemyAttack _enemyAttack;

        private EventInstance _fmodMusicEventInstance;
        private GuardianList _guardianList;
        private BattlePlayer _player;
        private EnemyList _enemyList;

        public void Initialize(BattleStatus battleStatus)
        {
            _battleStatus = battleStatus;

            _player = CreatePlayer(battleStatus);
            _enemyList = CreateEnemyList(battleStatus);
            _guardianList = CreateGuardianList(battleStatus);
            
            _stateMachine = new StateMachine();

            _coroutineService = CoroutineService.CreateInstance();
            
            _enemyAttackProcessor = new EnemyAttackProcessor(_enemyList, _player, _guardianList);
            _enemyAttack = new EnemyAttack(_coroutineService, _enemyList, _enemyAttackProcessor);
            _enemyAttack.AllEnemyAttacked += () => _stateMachine.ChangeState<UserMoveState>();
            
            InitializeStateMachineStates(_guardianList, _enemyAttack);

            _attackProcessor = new PlayerAttackProcessor(_enemyList, _player);
            
            _battle = InitializeBattle();
            _battleUIBootstrapper.Initialize(_battle, _attackProcessor, _enemyAttack);

            _stateMachine.ChangeState<UserMoveState>();
            
            foreach (GuardianCell guardianCell in _guardianList)
            {
                guardianCell.RerollDices();
            }
            
            _enemyList.EnemiesDead += EnemyListOnEnemiesDead;
            _player.Health.TookDamage += PlayerHealthOnTookDamage;
            
            _fmodMusicEventInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
            _fmodMusicEventInstance.start();
        }

        private Battle InitializeBattle()
        {
            return new Battle(_stateMachine, _battleStatus.GameSettings.Battle.MaxActions, _guardianList, _player, _enemyList, _attackProcessor);
        }

        private static EnemyList CreateEnemyList(BattleStatus battleStatus)
        {
            return new EnemyList(battleStatus.Enemies.Select((enemy, _) => new BattleEnemy(enemy, 5)).ToArray());
        }

        private static BattlePlayer CreatePlayer(BattleStatus battleStatus)
        {
            return new BattlePlayer(battleStatus.Player, battleStatus.Player.MaxHealth,
                battleStatus.PlayerHealth);
        }

        private static GuardianList CreateGuardianList(BattleStatus battleStatus)
        {
            return new GuardianList(new[]
            {
                GuardianCellFactory.CreateGuardianCell(battleStatus.Guardians[0]),
                GuardianCellFactory.CreateGuardianCell(battleStatus.Guardians[1]),
                GuardianCellFactory.CreateGuardianCell(battleStatus.Guardians[2]),
                GuardianCellFactory.CreateGuardianCell(battleStatus.Guardians[3]),
            });
        }

        private void InitializeStateMachineStates(GuardianList guardianList, EnemyAttack enemyAttack)
        {
            _stateMachine.AddState(new UserMoveState(guardianList, _player));
            _stateMachine.AddState(new EnemyAttackState(enemyAttack));
            _stateMachine.AddState(new EndState());
        }

        private void PlayerHealthOnTookDamage(TookDamageEventArgs args)
        {
            if (args.CurrentValue == 0)
            {
                _stateMachine.ChangeState<EndState>();
            }
        }

        private void EnemyListOnEnemiesDead()
        {
            _stateMachine.ChangeState<EndState>();
        }
    }
}