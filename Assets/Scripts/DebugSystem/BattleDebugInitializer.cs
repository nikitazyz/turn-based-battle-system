using Core;
using Dices;
using Guardians;
using UnityEngine;

namespace DebugSystem
{
    public class BattleDebugInitializer : MonoBehaviour
    {
        [SerializeField] private BattleBootstrapper _battleBootstrapper;

        [SerializeField] private GameSettings _debugGameSettings;
        [SerializeField] private Dice _debugGuardianDice;
        [SerializeField] private Guardian[] _debugGuardians;

        private void Awake()
        {
            _battleBootstrapper.Initialize(new BattleStatus(_debugGameSettings, _debugGuardians, _debugGuardianDice));
        }
    }
}