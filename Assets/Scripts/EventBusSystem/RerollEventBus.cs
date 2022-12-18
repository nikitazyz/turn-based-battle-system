using Core;
using Guardians;

namespace EventBusSystem
{
    public class RerollEventBus : ActionEventBus
    {
        private readonly Battle _battle;

        public RerollEventBus(Battle battle) : base(battle)
        {
            _battle = battle;
        }

        public void Invoke(GuardianCell guardianCell)
        {
            if (!Enabled || _battle.Actions == 0)
            {
                return;
            }
            
            guardianCell.RerollDices();
            Invoke();
        }
    }
}