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

        [SerializeField] private int _rerollAmount;
        public int RerollAmount => _rerollAmount;
    }
}