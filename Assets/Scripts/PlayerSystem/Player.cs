using UnityEngine;

namespace PlayerSystem
{
    [CreateAssetMenu(menuName = "Game/Player", fileName = "Player")]
    public class Player : ScriptableObject
    {
        [field: SerializeField]
        public int Lvl { get; private set; }
        [field: SerializeField]
        public int MaxHealth { get; private set; }
    }
}