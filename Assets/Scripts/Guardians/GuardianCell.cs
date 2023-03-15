using System;
using System.Linq;
using Dices;
using UnityEngine;

namespace Guardians
{
    public class GuardianCell
    {
        public event Action RerollAmountChanged;
        
        private readonly Guardian _guardian;
        private readonly BattleDice[] _dices;
        private int _rerollAmount;
        public Sprite Avatar => _guardian.Avatar;

        public BattleDice[] Dices => _dices.ToArray();

        public int RerollAmount
        {
            get => _rerollAmount;
            private set
            {
                _rerollAmount = value;
                RerollAmountChanged?.Invoke();
            }
        }

        public GuardianCell(Guardian guardian, BattleDice battleDice1, BattleDice battleDice2, BattleDice battleDice3)
        {
            _dices = new[]
            {
                battleDice1,
                battleDice2,
                battleDice3
            };

            _guardian = guardian;
            RerollAmount = guardian.RerollAmount;
        }

        private BattleDice[] GetDices()
        {
            BattleDice[] battleDiceArray = new BattleDice[3];
            _dices.CopyTo(battleDiceArray, 0);
            return battleDiceArray;
        }

        public void RerollDices(bool useRerollAmount = true)
        {
            if (RerollAmount <= 0)
            {
                return;
            }
            foreach (var dice in _dices)
            {
                if (!dice.IsUsed)
                {
                    dice.Reroll();
                }
            }

            if (useRerollAmount)
            {
                RerollAmount--;
            }
        }

        public void ResetDices()
        {
            foreach (var dice in _dices)
            {
                dice.Reset();
                dice.ResetFlame();
            }
        }

        public void ResetRerollAmount()
        {
            RerollAmount = _guardian.RerollAmount;
        }
    }
}