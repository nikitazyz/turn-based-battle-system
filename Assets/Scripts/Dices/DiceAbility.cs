using System;
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

        public void Process(AttackStatus attackStatus)
        {
            foreach (var abilityEffect in _effects)
            {
                abilityEffect.Process(attackStatus);
            }
        }
    }
}