using System;
using SceneLoadSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private SceneLoader _sceneLoader;

        [SerializeField] private Button _debugButton;
        private Game _game;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _game = new Game(_gameSettings, _sceneLoader);
            _debugButton.onClick.AddListener(() => _game.StartBattle());
        }
    }
}