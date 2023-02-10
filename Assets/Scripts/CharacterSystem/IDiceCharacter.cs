using Dices;

namespace CharacterSystem
{
    public interface IDiceCharacter : ICharacter
    {
        public BattleDice Dice { get; }
    }
}