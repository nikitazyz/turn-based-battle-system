using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HealthSystem;

namespace Enemies
{
    public class EnemyList : IReadOnlyList<BattleEnemy>
    {
        public event Action EnemiesDead;
        
        private readonly IList<BattleEnemy> _battleEnemies;

        public EnemyList(params BattleEnemy[] battleEnemies)
        {
            _battleEnemies = battleEnemies;
            foreach (var battleEnemy in battleEnemies)
            {
                battleEnemy.Health.TookDamage += EnemyHealthOnTookDamage;
            }
        }

        private void EnemyHealthOnTookDamage(TookDamageEventArgs args)
        {
            if (args.CurrentValue == 0 && _battleEnemies.All(enemy => enemy.Health.Value == 0))
            {
                EnemiesDead?.Invoke();
            }
        }

        public IEnumerator<BattleEnemy> GetEnumerator()
        {
            return _battleEnemies.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _battleEnemies.Count;

        public BattleEnemy this[int index] => _battleEnemies[index];

        public BattleEnemy FirstAlive => _battleEnemies.FirstOrDefault(en => en.Health.Value > 0);
        public BattleEnemy LastAlive => _battleEnemies.LastOrDefault(en => en.Health.Value > 0);
        public IList<BattleEnemy> AllAlive => _battleEnemies.Where(en => en.Health.Value > 0).ToList();
    }
}