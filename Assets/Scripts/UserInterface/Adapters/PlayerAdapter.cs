using AttackSystem;
using CharacterSystem;
using Dices;
using UserInterface.Views;

namespace UserInterface.Adapters
{
    public class PlayerAdapter : CharacterAdapter
    {
        private CharacterView _view;
        
        public PlayerAdapter(CharacterView view, ICharacter character, AttackProcessor attackProcessor) : base(view, character)
        {
            attackProcessor.DiceUsed += AttackProcessorOnDiceUsed;
            _view = view;
        }

        private void AttackProcessorOnDiceUsed(BattleDice dice, DiceAbility ability)
        {
            _view.TriggerAnimation(ability.DiceAnimationType);
        }
    }
}