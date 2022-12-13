using HealthSystem;

namespace Player
{
    public class BattlePlayer
    {
        public Player Player { get; }
        
        public Health Health { get; }

        public BattlePlayer(Player player, int maxHealth, int healthValue)
        {
            Player = player;
            Health = new Health(maxHealth, healthValue);
        }

        public BattlePlayer(Player player, int healthValue) : this(player, healthValue, healthValue) { }
    }
}