using Core;
using Initialization;
using UnityEngine;
using UserInterface.Adapters;
using UserInterface.Views;

namespace UserInterface.UIBootstrappers
{
    public class BattleUIBootstrapper : MonoBehaviour, IInitializable<Battle>
    {
        [SerializeField] private MoveView _moveView;
        [SerializeField] private StatusView _statusView;

        [SerializeField] private CharacterView _playerView;
        [SerializeField] private CharacterView[] _enemyViews;

        private MoveAdapter _moveAdapter;
        private StatusAdapter _statusAdapter;

        private CharacterAdapter _playerAdapter;
        private CharacterAdapter[] _characterAdapters;

        public void Initialize(Battle battle)
        {
            _moveAdapter = new MoveAdapter(_moveView, battle);

            _statusAdapter = new StatusAdapter(_statusView, battle.Player);
            _playerAdapter = new CharacterAdapter(_playerView, battle.Player);

            _characterAdapters = new CharacterAdapter[battle.EnemyList.Count];

            for (var i = 0; i < battle.EnemyList.Count; i++)
            {
                var enemy = battle.EnemyList[i];
                _characterAdapters[i] = new CharacterAdapter(_enemyViews[i], enemy);
            }
        }
    }
}