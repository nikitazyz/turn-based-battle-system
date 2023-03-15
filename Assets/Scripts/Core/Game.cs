using System;
using SceneLoadSystem;
using Object = UnityEngine.Object;

namespace Core
{
    public class Game : IGame
    {
        private readonly GameSettings _gameSettings;
        private readonly SceneLoader _sceneLoader;

        public Game(GameSettings gameSettings, SceneLoader sceneLoader)
        {
            _gameSettings = gameSettings;
            _sceneLoader = sceneLoader;
        }

        public BattleStatus BattleStatus { get; set; }

        public void StartBattle()
        {
            if (BattleStatus == null || BattleStatus.Player == null || BattleStatus.Guardians == null || BattleStatus.Guardians.Length == 0
                || BattleStatus.Enemies == null || BattleStatus.Enemies.Length == 0 || BattleStatus.GameSettings == null)
            {
                throw new NullReferenceException();
            }
            _sceneLoader.LoadScene(_gameSettings.Battle.BattleScene, BattleOnSceneLoaded);
        }

        private void BattleOnSceneLoaded()
        {
            var battle = Object.FindObjectOfType<BattleBootstrapper>();
            battle.Initialize(this);
        }

        public void OpenMap()
        {
            _sceneLoader.LoadScene(_gameSettings.MapScene);
        }

        public void OpenBattleEditor()
        {
            _sceneLoader.LoadScene(_gameSettings.EditorScene, EditorOnSceneLoaded);
            
        }

        private void EditorOnSceneLoaded()
        {
            Object.FindObjectOfType<DebugParametersBootstrapper>().Initialize(this);
        }
    }
}