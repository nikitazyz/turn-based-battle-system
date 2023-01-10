using System;
using UnityEngine;

namespace Dices.AbilityEffects
{
    public class HealEffect : AbilityEffect
    {
        [field: SerializeField]
        public int Heal { get; private set; }

        public override void Process(AttackStatus attackStatus)
        {
            attackStatus.Heal += Heal;
        }
    }
}