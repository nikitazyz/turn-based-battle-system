using HealthSystem;

namespace Player
{
    public class BattlePlayer
    {
        private Player _player;
        
        public Health Health { get; }

        public BattlePlayer(Player player, int maxHealth, int healthValue)
        {
            _player = player;
            Health = new Health(maxHealth, healthValue);
        }

        public BattlePlayer(Player player, int healthValue) : this(player, healthValue, healthValue) { }
    }
}