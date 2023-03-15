using System;

namespace HealthSystem
{
    public class Health
    {
        public event Action<int> ValueChanged;
        public event Action<TookDamageEventArgs> TookDamage;
        public event Action<HealedEventArgs> OnHeal;
        
        private int _value;

        public int Value
        {
            get => _value;
            private set
            {
                _value = value;
                ValueChanged?.Invoke(value);
            }
        }

        public int MaxValue { get; }

        public Health(int maxValue, int value)
        {
            MaxValue = maxValue;
            Value = value;
        }

        public Health(int value) : this(value, value)
        {
        }

        public void TakeDamage(int damage)
        {
            if (damage <=0)
            {
                return;
            }
            if (Value - damage < 0)
            {
                var value = Value;
                Value = 0;
                TookDamage?.Invoke(new TookDamageEventArgs(value, Value));
                return;
            }

            Value -= damage;
            TookDamage?.Invoke(new TookDamageEventArgs(damage, Value));
        }

        public void Heal(int healValue)
        {
            if (healValue <= 0)
            {
                return;
            }
            if (Value + healValue > MaxValue)
            {
                var value = Value;
                Value = MaxValue;
                OnHeal?.Invoke(new HealedEventArgs(value, Value));
                return;
            }

            Value += healValue;
            OnHeal?.Invoke(new HealedEventArgs(healValue, Value));
        }
    }
}