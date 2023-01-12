using SceneLoadSystem;

namespace Core
{
    public class Game
    {
        private readonly GameSettings _gameSettings;
        private readonly SceneLoader _sceneLoader;

        public Game(GameSettings gameSettings, SceneLoader sceneLoader)
        {
            _gameSettings = gameSettings;
            _sceneLoader = sceneLoader;
        }

        public void StartBattle()
        {
            _sceneLoader.LoadScene(_gameSettings.Battle.BattleScene);
        }
    }
}