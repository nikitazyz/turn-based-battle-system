using UnityEngine;

namespace Dices.AbilityEffects
{
    public class PoisonEffect : AbilityEffect
    {
        [SerializeField] private int _damage;

        public int Damage => _damage;
    }
}