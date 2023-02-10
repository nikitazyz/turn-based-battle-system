using System.Linq;
using Dices;
using UnityEngine;

namespace Guardians
{
    public class GuardianCell
    {
        private readonly Guardian _guardian;
        private readonly BattleDice[] _dices;
        public Sprite Avatar => _guardian.Avatar;

        public BattleDice[] Dices => _dices.ToArray();

        public GuardianCell(Guardian guardian, BattleDice battleDice1, BattleDice battleDice2, BattleDice battleDice3)
        {
            _dices = new[]
            {
                battleDice1,
                battleDice2,
                battleDice3
            };

            _guardian = guardian;
        }

        private BattleDice[] GetDices()
        {
            BattleDice[] battleDiceArray = new BattleDice[3];
            _dices.CopyTo(battleDiceArray, 0);
            return battleDiceArray;
        }

        public void RerollDices()
        {
            foreach (var dice in _dices)
            {
                if (!dice.IsUsed)
                {
                    dice.Reroll();
                }
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
    }
}