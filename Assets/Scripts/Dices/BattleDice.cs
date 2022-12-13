using System;
using Random = UnityEngine.Random;

namespace Dices
{
    public class BattleDice
    {
        public event Action<int> Rerolled;
        public event Action Used;
        public event Action Reseted; 

        private Dice _dice;
        public int Side { get; private set; }
        public bool IsUsed { get; private set; }

        public BattleDice(Dice dice)
        {
            _dice = dice;
        }
    
        public void Reroll()
        {
            Side = Random.Range(0, 6);
            Rerolled?.Invoke(Side);
        }

        public DiceAbility DiceAbility()
        {
            return _dice.Abilities[Side];
        }

        public void Use()
        {
            if (IsUsed)
            {
                return;
            }
            IsUsed = true;
            Used?.Invoke();
        }

        public void Reset()
        {
            if (!IsUsed)
            {
                return;
            }
            IsUsed = false;
            Reseted?.Invoke();
        }
    }
}