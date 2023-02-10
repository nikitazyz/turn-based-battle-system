using UnityEngine;

namespace Dices.AbilityEffects
{
    public class SelfPoisonEffect : AbilityEffect
    {
        [SerializeField] private int _damage;

        public int Damage => _damage;
    }
}