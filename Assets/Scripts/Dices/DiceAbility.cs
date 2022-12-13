using UnityEngine;

namespace Dices
{
    [CreateAssetMenu(fileName = "Dice Ability", menuName = "Game/Dice Ability")]
    public class DiceAbility : ScriptableObject
    {
        [SerializeField] private Sprite _image;

        public Sprite Image => _image;
    }
}