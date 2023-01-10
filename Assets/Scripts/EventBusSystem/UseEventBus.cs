using Core;
using Dices;

namespace EventBusSystem
{
    public class UseEventBus : ActionEventBus
    {
        private readonly Battle _battle;
        private readonly AttackProcessor _attackProcessor;

        public UseEventBus(Battle battle, AttackProcessor attackProcessor) : base(battle)
        {
            _battle = battle;
            _attackProcessor = attackProcessor;
        }

        public void Invoke(BattleDice battleDice)
        {
            if (!Enabled || _battle.Actions == 0)
            {
                return;
            }

            _attackProcessor.UseDice(battleDice);
            Invoke();
        }
    }
}