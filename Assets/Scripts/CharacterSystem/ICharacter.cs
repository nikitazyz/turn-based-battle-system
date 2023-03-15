using HealthSystem;
using PoisonSystem;
using UnityEngine;

namespace CharacterSystem
{
    public interface ICharacter
    {
        public Health Health { get; }
        public PoisonEffect PoisonEffect { get; }
        public RuntimeAnimatorController AnimatorController { get; }
    }
}