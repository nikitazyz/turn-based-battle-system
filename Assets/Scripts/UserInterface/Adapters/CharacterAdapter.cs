using AttackSystem;
using CharacterSystem;
using Dices;
using UserInterface.Views;

namespace UserInterface.Adapters
{
    public class CharacterAdapter
    {
        private CharacterView View { get; }
        private ICharacter Model { get; }

        public CharacterAdapter(CharacterView view, ICharacter character)
        {
            View = view;
            Model = character;
            
            InitializeHealth(character);
            InitializePoisonCounter();

            View.AnimatorController = character.AnimatorController;
        }

        private void InitializePoisonCounter()
        {
            Model.PoisonEffect.Changed += PoisonEffectOnChanged;
            View.PoisonCounter = Model.PoisonEffect.PoisonCounter;
        }

        private void InitializeHealth(ICharacter character)
        {
            Model.Health.ValueChanged += HealthOnValueChanged;
            View.MaxHealthValue = character.Health.MaxValue;
            View.HealthValue = character.Health.Value;
        }

        private void PoisonEffectOnChanged(int value)
        {
            View.PoisonCounter = value;
        }

        private void HealthOnValueChanged(int value)
        {
            View.HealthValue = value;
        }
    }
}