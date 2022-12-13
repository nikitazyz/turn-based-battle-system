using Core;

namespace EventBusSystem
{
    public class ActionEventBus : EventBus
    {
        private readonly Battle _battle;

        public ActionEventBus(Battle battle)
        {
            _battle = battle;
        }

        protected override void OnInvoke()
        {
            _battle.Act();
        }
    }
}