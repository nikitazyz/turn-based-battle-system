using Dices;

namespace Guardians
{
    public class GuardianCellFactory
    {
        public static BattleDice CreateBattleDice(Dice dice) => new BattleDice(dice);

        public static GuardianCell CreateGuardianCell(Guardian guardian, Dice dice1, Dice dice2, Dice dice3) =>
            new GuardianCell(guardian, CreateBattleDice(dice1), CreateBattleDice(dice2), CreateBattleDice(dice3));
        
    }
}