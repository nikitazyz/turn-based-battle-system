using UnityEngine;

namespace Dices
{
    [CreateAssetMenu(fileName = "Dice", menuName = "Game/Dice")]
    public class Dice : ScriptableObject
    {
        [SerializeField] private DiceAbility _ability0;
        [SerializeField] private DiceAbility _ability1;
        [SerializeField] private DiceAbility _ability2;
        [SerializeField] private DiceAbility _ability3;
        [SerializeField] private DiceAbility _ability4;
        [SerializeField] private DiceAbility _ability5;

        public DiceAbility Ability0 => _ability0;

        public DiceAbility Ability1 => _ability1;

        public DiceAbility Ability2 => _ability2;

        public DiceAbility Ability3 => _ability3;

        public DiceAbility Ability4 => _ability4;

        public DiceAbility Ability5 => _ability5;

        public DiceAbility[] Abilities => new[]
        {
            _ability0,
            _ability1,
            _ability2,
            _ability3,
            _ability4,
            _ability5
        };
    }
}