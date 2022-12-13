using System;
using SceneLoadSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    [Serializable]
    public class GameBattleSettings
    {
        [field: SerializeField, ScenePath] public string BattleScene { get; private set; }
        [field: SerializeField] public int MaxActions { get; private set; }
    }
}