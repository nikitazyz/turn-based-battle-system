using UnityEngine;

namespace Dices.AbilityEffects
{
    public class NextDamageTypeEffect : AbilityEffect
    {
        [SerializeField] private EffectTargetType _effectTargetType;
        public override void Process(AttackStatus attackStatus)
        {
            attackStatus.EffectTargetType = _effectTargetType;
        }
    }
}