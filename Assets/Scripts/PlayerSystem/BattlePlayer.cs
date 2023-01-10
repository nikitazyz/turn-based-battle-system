using HealthSystem;

namespace PlayerSystem
{
    public class BattlePlayer
    {
        private PlayerSystem.Player _player;
        
        public Health Health { get; }

        public BattlePlayer(PlayerSystem.Player player, int maxHealth, int healthValue)
        {
            _player = player;
            Health = new Health(maxHealth, healthValue);
        }

        public BattlePlayer(PlayerSystem.Player player, int healthValue) : this(player, healthValue, healthValue) { }
    }
}