using Animation;
using Dices;
using UnityEditor.Animations;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Game/Enemy")]
    public class Enemy : ScriptableObject
    {
        [SerializeField] private Dice _battleDice;
        
        [SerializeField] private CharacterAnimatorParameter _animatorController;

        public AnimatorController AnimatorController => _animatorController.GetAnimatorController();

        public Dice BattleDice => _battleDice;
    }
}