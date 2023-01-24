using System.Collections.Generic;
using Dices.AbilityEffects;
using UnityEngine;

namespace Dices
{
    [CreateAssetMenu(fileName = "Dice Ability", menuName = "Game/Dice Ability")]
    public class DiceAbility : ScriptableObject
    {
        [SerializeField] private Sprite _image;
        [SerializeField, HideInInspector] private List<AbilityEffect> _effects;

        public Sprite Image => _image;

        public AbilityEffect[] GetEffects()
        {
            AbilityEffect[] abilityEffects = new AbilityEffect[_effects.Count];
            _effects.CopyTo(abilityEffects);
            return abilityEffects;
        }
    }
}