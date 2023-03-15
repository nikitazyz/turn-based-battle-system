using SceneLoadSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private SceneLoader _sceneLoader;

        private Game _game;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _game = new Game(_gameSettings, _sceneLoader);
            _game.BattleStatus = new BattleStatus(_gameSettings, _gameSettings.Guardians, null, _gameSettings.Player,
                _gameSettings.Player.MaxHealth);
            _game.OpenBattleEditor();
        }
    }
}