using Animation;
using CharacterSystem;
using Dices;
using HealthSystem;
using PoisonSystem;
using UnityEditor.Animations;
using UnityEngine;

namespace Enemies
{
    public class BattleEnemy : IDiceCharacter
    {
        public Enemy Enemy { get; }

        public Health Health { get; }
        public PoisonEffect PoisonEffect { get; }
        public AnimatorController AnimatorController { get; }

        public BattleEnemy(Enemy enemy, int maxHealth, int healthValue)
        {
            Enemy = enemy;
            Health = new Health(maxHealth, healthValue);
            Dice = new BattleDice(enemy.BattleDice, false);
            PoisonEffect = new PoisonEffect();
            AnimatorController = enemy.AnimatorController;
        }

        public BattleEnemy(Enemy enemy, int healthValue) : this(enemy, healthValue, healthValue) {}
        
        public BattleDice Dice { get; }
    }
}