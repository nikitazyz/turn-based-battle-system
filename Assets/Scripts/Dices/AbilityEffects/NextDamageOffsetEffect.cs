using UnityEngine;

namespace Dices.AbilityEffects
{
    public class NextDamageOffsetEffect : AbilityEffect
    {
        [SerializeField] private int _damageOffset;

        public int DamageOffset => _damageOffset;
    }
}