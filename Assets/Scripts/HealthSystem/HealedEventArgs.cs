namespace HealthSystem
{
    public class HealedEventArgs
    {
        public readonly int CurrentValue;
        public readonly int HealValue;

        public HealedEventArgs(int healValue, int currentValue)
        {
            HealValue = healValue;
            CurrentValue = currentValue;
        }
    }
}