namespace HealthSystem
{
    public class TookDamageEventArgs
    {
        public readonly int CurrentValue;
        public readonly int Damage;

        public TookDamageEventArgs(int damage, int currentValue)
        {
            Damage = damage;
            CurrentValue = currentValue;
        }
    }
}