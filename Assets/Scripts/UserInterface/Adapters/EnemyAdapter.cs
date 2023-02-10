using AttackSystem;
using CharacterSystem;
using Enemies;
using UserInterface.Views;

namespace UserInterface.Adapters
{
    public class EnemyAdapter : CharacterAdapter
    {
        private readonly EnemyView _view;
        private readonly IDiceCharacter _diceCharacter;
        
        public EnemyAdapter(EnemyView view, IDiceCharacter character, EnemyAttack enemyAttack) : base(view, character)
        {
            view.DiceEnabled = !character.Dice.IsUsed;
            
            character.Dice.Reseted += DiceOnReseted;
            character.Dice.Used += DiceOnUsed;
            character.Dice.Rerolled += DiceOnRerolled;

            _view = view;
            _diceCharacter = character;
            enemyAttack.EnemyAttacked += EnemyAttackOnEnemyAttacked;
        }

        private void EnemyAttackOnEnemyAttacked(BattleEnemy enemy)
        {
            if (_diceCharacter == enemy)
            {
                _view.TriggerAnimation(enemy.Dice.DiceAbility.DiceAnimationType);
            }
        }

        private void DiceOnReseted()
        {
            _view.DiceEnabled = true;
        }

        private void DiceOnUsed()
        {
            _view.DiceEnabled = false;
        }

        private void DiceOnRerolled(int side)
        {
            _view.Dice = _diceCharacter.Dice.DiceAbility.Image;
        }
    }
}