using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Game/Settings")]
    public class GameSettings : ScriptableObject
    {
        [field: SerializeField]
        public GameBattleSettings Battle { get; private set; }
    }
}