using Core;
using PlayerSystem;
using UserInterface.Core;
using UserInterface.Views;

namespace UserInterface.Adapters
{
    public class StatusAdapter : IAdapter<StatusView, BattlePlayer>
    {
        public StatusView View { get; set; }
        public BattlePlayer Model { get; set; }

        public StatusAdapter(StatusView view, BattlePlayer model)
        {
            View = view;
            Model = model;
            
            model.Health.ValueChanged += HealthOnValueChanged;
            View.SetHealth(model.Health.Value, model.Health.MaxValue);
        }

        private void HealthOnValueChanged(int value)
        {
            View.SetHealth(value, Model.Health.MaxValue);
        }
    }
}