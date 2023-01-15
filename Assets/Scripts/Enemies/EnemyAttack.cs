using System;
using System.Collections;
using CoroutineManagement;
using UnityEngine;

namespace Enemies
{
    public class EnemyAttack
    {
        public event Action AllEnemyAttacked;
        
        private readonly CoroutineService _coroutineService;
        private readonly EnemyList _enemyList;

        private Coroutine _attackCoroutine;

        public EnemyAttack(CoroutineService coroutineService, EnemyList enemyList)
        {
            _coroutineService = coroutineService;
            _enemyList = enemyList;
        }

        public void StartEnemyAttack()
        {
            _attackCoroutine = _coroutineService.StartCoroutine(AttackRoutine());
        }

        public void StopEnemyAttack()
        {
            _coroutineService.EndInvoke(_attackCoroutine);
        }

        IEnumerator AttackRoutine()
        {
            foreach (var enemy in _enemyList.AllAlive)
            {
                yield return new WaitForSeconds(1);
                Debug.Log("Enemy attack");
            }
            yield return new WaitForSeconds(1);
            AllEnemyAttacked?.Invoke();
        }
    }
}