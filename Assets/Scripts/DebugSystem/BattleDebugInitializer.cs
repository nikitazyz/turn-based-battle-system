using Core;
using Enemies;
using Guardians;
using PlayerSystem;
using UnityEngine;

namespace DebugSystem
{
    public class BattleDebugInitializer : MonoBehaviour
    {
        [SerializeField] private BattleBootstrapper _battleBootstrapper;

        [SerializeField] private GameSettings _debugGameSettings;
        [SerializeField] private Guardian[] _debugGuardians;
        [SerializeField] private Enemy[] _debugEnemies;
        [SerializeField] private Player _player;

        private void Awake()
        {
            _battleBootstrapper.Initialize(new BattleStatus(_debugGameSettings, _debugGuardians, _debugEnemies, _player, _player.MaxHealth));
        }
    }
}