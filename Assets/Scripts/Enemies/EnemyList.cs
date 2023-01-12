using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Enemies
{
    public class EnemyList : IReadOnlyList<BattleEnemy>
    {
        private readonly IList<BattleEnemy> _battleEnemies;

        public EnemyList(params BattleEnemy[] battleEnemies)
        {
            _battleEnemies = battleEnemies;
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
    }
}