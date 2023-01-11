using Core;
using PlayerSystem;
using UserInterface.Core;
using UserInterface.Views;

namespace UserInterface.Adapters
{
    public class StatusAdapter : IAdapter<StatusView, BattlePlayer>
    {
        private BattlePlayer _model;
        public StatusView View { get; }

        public BattlePlayer Model
        {
            get => _model;
            set
            {
                if (_model != null)
                {
                    _model.Health.ValueChanged -= HealthOnValueChanged;
                }
                _model = value;
                if (_model != null)
                {
                    _model.Health.ValueChanged += HealthOnValueChanged;
                }
                OnPlayerUpdated(value);
            }
        }

        private void OnPlayerUpdated(BattlePlayer player)
        {
            View.SetHealth(player.Health.MaxValue, player.Health.Value);
        }

        public StatusAdapter(StatusView view, BattlePlayer model)
        {
            View = view;
            Model = model;
        }

        private void HealthOnValueChanged(int value)
        {
            View.SetHealth(Model.Health.MaxValue, value);
        }
    }
}