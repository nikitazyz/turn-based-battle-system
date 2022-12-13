using Core;
using Dices;

namespace EventBusSystem
{
    public class UseEventBus : ActionEventBus
    {
        private readonly Battle _battle;

        public UseEventBus(Battle battle) : base(battle)
        {
            _battle = battle;
        }

        public void Invoke(BattleDice battleDice)
        {
            if (!Enabled || _battle.Actions == 0)
            {
                return;
            }

            battleDice.Use();
            Invoke();
        }
    }
}