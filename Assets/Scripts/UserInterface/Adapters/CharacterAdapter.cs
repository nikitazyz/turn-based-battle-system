using CharacterSystem;
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
            
            Model.Health.ValueChanged += HealthOnValueChanged;
            View.MaxHealthValue = character.Health.MaxValue;
            View.HealthValue = character.Health.Value;
        }

        private void HealthOnValueChanged(int value)
        {
            View.HealthValue = value;
        }
    }
}