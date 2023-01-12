using UnityEngine;

namespace Dices.AbilityEffects
{
    public class DamageEffect : AbilityEffect
    {
        [field: SerializeField]
        public int Damage { get; private set; }

        public override void Process(AttackStatus attackStatus)
        {
            attackStatus.Damage += Damage;
        }
    }
}