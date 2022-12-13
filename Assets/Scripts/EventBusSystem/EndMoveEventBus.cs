using Core;

namespace EventBusSystem
{
    public class EndMoveEventBus : EventBus
    {
        private readonly Battle _battle;

        public EndMoveEventBus(Battle battle)
        {
            _battle = battle;
        }

        protected override void OnInvoke()
        {
            _battle.EndMove();
        }
    }
}