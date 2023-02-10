using System;
using Dices;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Views.Elements;

namespace UserInterface.Views
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private HealthBarElement _healthBarElement;
        [SerializeField] private PoisonCounterElement _poisonCounterElement;
        [SerializeField] private Animator _animator;
        
        private static readonly int Attack = Animator.StringToHash("Attack");

        public RuntimeAnimatorController AnimatorController
        {
            get => _animator.runtimeAnimatorController;
            set => _animator.runtimeAnimatorController = value;
        }

        public void TriggerAnimation(DiceAnimationType animationType)
        {
            _animator.SetTrigger(GetAnimationFromType(animationType));
        }

        private int GetAnimationFromType(DiceAnimationType animationType)
        {
            return animationType switch
            {
                DiceAnimationType.None => 0,
                DiceAnimationType.Attack => Attack,
                _ => throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null)
            };
        }

        public int HealthValue
        {
            get => _healthBarElement.CurrentValue;
            set => _healthBarElement.CurrentValue = value;
        }

        public int MaxHealthValue
        {
            get => _healthBarElement.MaxValue;
            set => _healthBarElement.MaxValue = value;
        }

        public int PoisonCounter
        {
            get => _poisonCounterElement.Value;
            set => _poisonCounterElement.Value = value;
        }
    }
}
