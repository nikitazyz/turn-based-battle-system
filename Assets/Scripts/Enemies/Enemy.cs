using Animation;
using Dices;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Game/Enemy")]
    public class Enemy : ScriptableObject
    {
        [SerializeField] private Dice _battleDice;
        [SerializeField] private int _maxHealth;
        
        [SerializeField] private RuntimeAnimatorController _animatorController;

        public RuntimeAnimatorController AnimatorController => _animatorController;

        public Dice BattleDice => _battleDice;

        public int MaxHealth => _maxHealth;

        public static Enemy CreateInstance(Dice dice, int health, RuntimeAnimatorController runtimeAnimatorController)
        {
            Enemy enemy = CreateInstance<Enemy>();
            enemy._battleDice = dice;
            enemy._maxHealth = health;
            enemy._animatorController = runtimeAnimatorController;
            return enemy;
        }
    }
}