using Dices;
using Guardians;

namespace Core
{
    public class BattleStatus
    {
        public GameSettings GameSettings { get; }

        public Guardian[] Guardians { get; }
        public Dice Dice { get; }

        public BattleStatus(GameSettings gameSettings, Guardian[] guardians, Dice dice)
        {
            Guardians = guardians;
            Dice = dice;
            GameSettings = gameSettings;
        }
    }
}