using System.Collections.Generic;
using System.Linq;
using Core;
using Enemies;
using PlayerSystem;
using UnityEngine;
using UserInterface.Views;

namespace UserInterface.Adapters
{
    public class DebugParametersAdapter
    {
        private readonly Player _player;
        private readonly Enemy[] _enemies;
        private readonly BattleStatus _gameBattleStatus;
        private readonly DebugParameters _debugParameters;

        public DebugParametersAdapter(DebugParametersView debugParametersView, Player player, Enemy[] enemies,
            BattleStatus gameBattleStatus, DebugParameters debugParameters)
        {
            _player = player;
            _enemies = enemies;
            _gameBattleStatus = gameBattleStatus;
            _debugParameters = debugParameters;
            debugParametersView.SetEnemiesList(enemies.Select(enemy => enemy.name).ToArray());
            debugParametersView.SetEnemiesDefaultHealth(enemies.Select(enemy => enemy.MaxHealth).ToArray());
            debugParametersView.SetPlayerDefaultHealth(player.MaxHealth);
            
            debugParametersView.PlayClicked += DebugParametersViewOnPlayClicked;
        }

        private void DebugParametersViewOnPlayClicked(PlayClickedEventArgs obj)
        {
            _gameBattleStatus.PlayerHealth = obj.OverrideHealth ?? _player.MaxHealth;
            List<Enemy> instantiatedEnemies = new List<Enemy>();
            foreach (var enemyId in obj.Enemies)
            {
                if (enemyId == 0)
                {
                    continue;
                }
                int id = enemyId - 1;
                Enemy original = _enemies[id];
                int health = obj.EnemiesOverrideHealth[id].HasValue
                    ? obj.EnemiesOverrideHealth[id].Value
                    : original.MaxHealth;
                Enemy instance = Enemy.CreateInstance(original.BattleDice, health, original.AnimatorController);
                instantiatedEnemies.Add(instance);
            }

            _gameBattleStatus.Enemies = instantiatedEnemies.ToArray();
            _debugParameters.StartGame();
        }
    }
}