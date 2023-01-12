using PlayerSystem;
using UserInterface.Views;

namespace UserInterface.Adapters
{
    public class StatusAdapter
    {
        private StatusView View { get; }
        private BattlePlayer BattlePlayer { get; }

        public StatusAdapter(StatusView view, BattlePlayer model)
        {
            View = view;
            BattlePlayer = model;
            
            View.SetHealth(BattlePlayer.Health.MaxValue, BattlePlayer.Health.Value);
            BattlePlayer.Health.ValueChanged += HealthOnValueChanged;
        }

        private void HealthOnValueChanged(int value)
        {
            View.SetHealth(BattlePlayer.Health.MaxValue, value);
        }
    }
}