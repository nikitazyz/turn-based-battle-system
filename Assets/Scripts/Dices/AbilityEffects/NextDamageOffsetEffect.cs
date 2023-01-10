using UnityEngine;
using UnityEngine.Serialization;

namespace Dices.AbilityEffects
{
    public class NextDamageOffsetEffect : AbilityEffect
    {
        [SerializeField] private int _damageOffset;
        public override void Process(AttackStatus attackStatus)
        {
            
        }
    }
}