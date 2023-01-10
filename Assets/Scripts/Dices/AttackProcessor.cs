using System;
using System.Linq;
using Enemies;
using PlayerSystem;

namespace Dices
{
    public class AttackProcessor
    {
        private readonly EnemyList _enemyList;
        private readonly BattlePlayer _battlePlayer;

        public AttackProcessor(EnemyList enemyList, BattlePlayer battlePlayer)
        {
            _enemyList = enemyList;
            _battlePlayer = battlePlayer;
        }

        public void UseDice(BattleDice battleDice)
        {
            battleDice.Use();
            var ability = battleDice.DiceAbility();
            var attackStatus = new AttackStatus();
            ability.Process(attackStatus);
            Attack(attackStatus);
        }

        private void Attack(AttackStatus attackStatus)
        {
            BattleEnemy[] enemies = attackStatus.EffectTargetType switch
            {
                EffectTargetType.First => new[] { _enemyList[0] },
                EffectTargetType.Last => new[] { _enemyList[^1] },
                EffectTargetType.All => _enemyList.ToArray(),
                _ => throw new ArgumentOutOfRangeException()
            };

            foreach (var battleEnemy in enemies)
            {
                battleEnemy.Health.TakeDamage(attackStatus.Damage);
            }
            
            _battlePlayer.Health.Heal(attackStatus.Heal);
            _battlePlayer.Health.TakeDamage(attackStatus.Damage);
        }
    }
}