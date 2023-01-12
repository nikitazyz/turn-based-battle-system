using Dices;
using UnityEngine;

namespace Guardians
{
    [CreateAssetMenu(fileName = "Guardian", menuName = "Game/Guardian")]
    public class Guardian : ScriptableObject
    {
        [SerializeField] private Sprite _avatar;
        public Sprite Avatar => _avatar;

        [SerializeField] private Dice _dice;
        public Dice Dice => _dice;
    }
}