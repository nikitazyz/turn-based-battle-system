using Core;
using Dices;
using Enemies;
using Guardians;
using PlayerSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace DebugSystem
{
    public class BattleDebugInitializer : MonoBehaviour
    {
        [SerializeField] private BattleBootstrapper _battleBootstrapper;

        [SerializeField] private GameSettings _debugGameSettings;
        [SerializeField] private Dice _debugGuardianDice;
        [SerializeField] private Guardian[] _debugGuardians;
        [SerializeField] private Enemy[] _debugEnemies;
        [SerializeField] private Player _player;

        private void Awake()
        {
            _battleBootstrapper.Initialize(new BattleStatus(_debugGameSettings, _debugGuardians, _debugGuardianDice, _debugEnemies, _player, _player.MaxHealth));
        }
    }
}