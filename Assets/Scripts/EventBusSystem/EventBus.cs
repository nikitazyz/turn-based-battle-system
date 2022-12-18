using System;

namespace EventBusSystem
{
    public abstract class EventBus : IEventBus
    {
        private bool _enabled;
        public event Action<bool> EnableStateChanged;

        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                EnableStateChanged?.Invoke(value);
            }
        }

        public void Invoke()
        {
            if (Enabled)
            {
                OnInvoke();
            }
        }
        protected abstract void OnInvoke();

        protected virtual void OnActiveChanged() { }
        
        public EventBus()
        {
            Enabled = true;
        }
        
        public EventBus(bool enabled)
        {
            Enabled = enabled;
        }
    }
}