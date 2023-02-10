using System;
using Dices;
using Enemies;
using PlayerSystem;

namespace AttackSystem
{
    public abstract class AttackProcessor
    {
        public event Action<BattleDice, DiceAbility> DiceUsed; 

        protected readonly EnemyList EnemyList;
        protected readonly BattlePlayer BattlePlayer;

        public AttackProcessor(EnemyList enemyList, BattlePlayer battlePlayer)
        {
            EnemyList = enemyList;
            BattlePlayer = battlePlayer;
        }
        
        public void UseDice(BattleDice battleDice)
        {
            battleDice.Use();
            var ability = battleDice.DiceAbility;
            Attack(ability, battleDice);
            DiceUsed?.Invoke(battleDice, ability);
        }

        protected abstract void Attack(DiceAbility ability, BattleDice battleDice);
    }
}