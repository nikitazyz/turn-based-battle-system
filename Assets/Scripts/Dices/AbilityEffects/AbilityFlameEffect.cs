using UnityEngine;

namespace Dices.AbilityEffects
{
    public class AbilityFlameEffect : AbilityEffect
    {
        [SerializeField] private int _flameAmount;

        public int FlameAmount => _flameAmount;
    }
}