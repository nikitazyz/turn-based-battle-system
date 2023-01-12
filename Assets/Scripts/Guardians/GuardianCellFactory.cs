using Dices;

namespace Guardians
{
    public class GuardianCellFactory
    {
        public static BattleDice CreateBattleDice(Dice dice) => new BattleDice(dice);

        public static GuardianCell CreateGuardianCell(Guardian guardian) =>
            new GuardianCell(guardian, CreateBattleDice(guardian.Dice), CreateBattleDice(guardian.Dice), CreateBattleDice(guardian.Dice));
        
    }
}