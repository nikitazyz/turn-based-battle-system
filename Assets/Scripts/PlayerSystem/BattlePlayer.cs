using CharacterSystem;
using HealthSystem;
using PoisonSystem;
using UnityEditor.Animations;

namespace PlayerSystem
{
    public class BattlePlayer: ICharacter
    {
        private Player _player;
        
        public Health Health { get; }
        public PoisonEffect PoisonEffect { get; }
        public AnimatorController AnimatorController { get; }

        public BattlePlayer(Player player, int maxHealth, int healthValue)
        {
            _player = player;
            Health = new Health(maxHealth, healthValue);
            PoisonEffect = new PoisonEffect();
            AnimatorController = player.Animator;
        }

        public BattlePlayer(Player player, int healthValue) : this(player, healthValue, healthValue) { }
    }
}