using UnityEngine;

namespace Dices.AbilityEffects
{
    public class HealEffect : AbilityEffect
    {
        [field: SerializeField]
        public int Heal { get; private set; }
    }
}