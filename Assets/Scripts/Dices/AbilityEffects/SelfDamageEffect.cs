using System;
using UnityEngine;

namespace Dices.AbilityEffects
{
    [Serializable]
    public class SelfDamageEffect : AbilityEffect
    {
        [field: SerializeField]
        public int SelfDamage { get; private set; }
    }
}