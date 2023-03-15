using Enemies;
using PlayerSystem;
using UnityEngine;
using UserInterface.Adapters;
using UserInterface.Views;

namespace Core
{
    public class DebugParametersBootstrapper : MonoBehaviour
    {
        [SerializeField] private DebugParametersView _debugParametersView;
        [SerializeField] private Enemy[] _enemies;
        [SerializeField] private Player _player;
        private DebugParametersAdapter _debugParametersAdapter;
        private DebugParameters _debugParameters;

        public void Initialize(IGame game)
        {
            _debugParameters = new DebugParameters(game);
            _debugParametersAdapter = new DebugParametersAdapter(_debugParametersView, _player, _enemies, game.BattleStatus, _debugParameters);
        }
    }
}