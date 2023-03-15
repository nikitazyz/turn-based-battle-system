using System;
using System.Collections;
using AttackSystem;
using CoroutineManagement;
using Dices;
using Dices.AbilityEffects;
using PlayerSystem;
using UnityEngine;

namespace Enemies
{
    public class EnemyAttack
    {
        public event Action AllEnemyAttacked;
        public event Action<BattleEnemy> EnemyAttacked;
        
        private readonly CoroutineService _coroutineService;
        private readonly EnemyList _enemyList;
        private readonly AttackProcessor _attackProcessor;

        private Coroutine _attackCoroutine;

        public EnemyAttack(CoroutineService coroutineService, EnemyList enemyList, EnemyAttackProcessor attackProcessor)
        {
            _coroutineService = coroutineService;
            _enemyList = enemyList;
            _attackProcessor = attackProcessor;
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
                enemy.Dice.Reset();
                enemy.Dice.Reroll();
            }

            foreach (var enemy in _enemyList.AllAlive)
            {
                yield return new WaitForSeconds(1.5f);
                enemy.Health.TakeDamage(enemy.PoisonEffect.PoisonCounter--);
                _attackProcessor.UseDice(enemy.Dice);
                EnemyAttacked?.Invoke(enemy);
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(1);
            AllEnemyAttacked?.Invoke();
        }
    }
}