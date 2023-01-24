using UnityEngine;

namespace Dices.AbilityEffects
{
    public class ScaleNextDamageEffect : AbilityEffect
    {
        [SerializeField] private int _scale;

        public int Scale => _scale;
    }
}