using System;
using Random = UnityEngine.Random;

namespace Dices
{
    public class BattleDice
    {
        public event Action<int> Rerolled;
        public event Action Used;
        public event Action Reseted;
        public event Action<int> FlameSet;
        public event Action FlameReset;

        private Dice _dice;
        public int Side { get; private set; }
        public bool IsUsed { get; private set; }

        public int FlamedSide { get; private set; } = -1;

        public bool IsFlameOnActiveSide => Side == FlamedSide;

        public BattleDice(Dice dice)
        {
            _dice = dice;
        }
        
        public BattleDice(Dice dice, bool enabled)
        {
            _dice = dice;
            IsUsed = !enabled;
        }
    
        public void Reroll()
        {
            Side = Random.Range(0, 6);
            Rerolled?.Invoke(Side);
        }

        public DiceAbility DiceAbility => _dice.Abilities[Side];

        public void Use()
        {
            if (IsUsed)
            {
                return;
            }
            IsUsed = true;
            Used?.Invoke();
        }

        public void Reset(bool withoutReroll = false)
        {
            if (!IsUsed)
            {
                return;
            }

            if (!withoutReroll)
            {
                Reroll();
            }
            IsUsed = false;
            Reseted?.Invoke();
        }

        public void FlameActiveSide()
        {
            FlamedSide = Side;
            FlameSet?.Invoke(Side);
        }

        public void ResetFlame()
        {
            FlamedSide = -1;
            FlameReset?.Invoke();
        }
    }
}