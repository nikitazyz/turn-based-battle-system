using CharacterSystem;
using HealthSystem;

namespace PlayerSystem
{
    public class BattlePlayer: ICharacter
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