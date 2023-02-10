using HealthSystem;
using PoisonSystem;
using UnityEditor.Animations;

namespace CharacterSystem
{
    public interface ICharacter
    {
        public Health Health { get; }
        public PoisonEffect PoisonEffect { get; }
        public AnimatorController AnimatorController { get; }
    }
}