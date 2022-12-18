using System;

namespace EventBusSystem
{
    public interface IEventBus
    {
        event Action<bool> EnableStateChanged;
        bool Enabled { get; set; }
        void Invoke();
    }
}