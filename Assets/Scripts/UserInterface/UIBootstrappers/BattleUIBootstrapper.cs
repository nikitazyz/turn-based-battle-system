using AttackSystem;
using Core;
using Enemies;
using Initialization;
using UnityEngine;
using UserInterface.Adapters;
using UserInterface.Views;

namespace UserInterface.UIBootstrappers
{
    public class BattleUIBootstrapper : MonoBehaviour, IInitializable<Battle, AttackProcessor, EnemyAttack>
    {
        [SerializeField] private MoveView _moveView;
        [SerializeField] private StatusView _statusView;

        [SerializeField] private CharacterView _playerView;
        [SerializeField] private EnemyView[] _enemyViews;

        private MoveAdapter _moveAdapter;
        private StatusAdapter _statusAdapter;

        private PlayerAdapter _playerAdapter;
        private EnemyAdapter[] _characterAdapters;

        public void Initialize(Battle battle, AttackProcessor attackProcessor, EnemyAttack enemyAttack)
        {
            _moveAdapter = new MoveAdapter(_moveView, battle);

            _statusAdapter = new StatusAdapter(_statusView, battle.Player);
            _playerAdapter = new PlayerAdapter(_playerView, battle.Player, attackProcessor);

            _characterAdapters = new EnemyAdapter[battle.EnemyList.Count];

            for (var i = 0; i < battle.EnemyList.Count; i++)
            {
                var enemy = battle.EnemyList[i];
                _characterAdapters[i] = new EnemyAdapter(_enemyViews[i], enemy, enemyAttack);
            }
        }
    }
}