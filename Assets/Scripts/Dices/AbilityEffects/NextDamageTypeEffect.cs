using AttackSystem;
using UnityEngine;

namespace Dices.AbilityEffects
{
    public class NextDamageTypeEffect : AbilityEffect
    {
        [SerializeField] private EffectTargetType _effectTargetType;

        public EffectTargetType EffectTargetType => _effectTargetType;
    }
}