using HealthSystem;

namespace Enemies
{
    public class BattleEnemy
    {
        public Enemy Enemy { get; }

        public Health Health { get; }

        public BattleEnemy(Enemy enemy, int maxHealth, int healthValue)
        {
            Enemy = enemy;
            Health = new Health(maxHealth, healthValue);
        }

        public BattleEnemy(Enemy enemy, int healthValue) : this(enemy, healthValue, healthValue) {}
    }
}