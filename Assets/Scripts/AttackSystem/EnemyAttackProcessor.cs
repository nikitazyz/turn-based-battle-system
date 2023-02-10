using System;
using System.Collections.Generic;
using System.Linq;
using Dices;
using Dices.AbilityEffects;
using Enemies;
using Guardians;
using PlayerSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AttackSystem
{
    public class EnemyAttackProcessor : AttackProcessor
    {
        private readonly GuardianList _guardianList;

        public EnemyAttackProcessor(EnemyList enemyList, BattlePlayer battlePlayer, GuardianList guardianList) : base(enemyList, battlePlayer)
        {
            _guardianList = guardianList;
        }

        protected override void Attack(DiceAbility ability, BattleDice battleDice)
        {
            foreach (var abilityEffect in ability.GetEffects())
            {
                switch (abilityEffect)
                {
                    case DamageEffect damageEffect:
                        BattlePlayer.Health.TakeDamage(damageEffect.Damage);
                        break;
                    case HealEffect healEffect:
                        var battleEnemy = Min(EnemyList.Where(enemy => enemy.Dice != battleDice), enemy => enemy.Health.Value);
                        battleEnemy.Health.Heal(healEffect.Heal);
                        break;
                    case PoisonEffect poisonEffect:
                        BattlePlayer.PoisonEffect.PoisonCounter += poisonEffect.Damage;
                        break;
                    case AbilityFlameEffect abilityFlameEffect:
                        FlameDices(abilityFlameEffect);
                        break;
                }
            }
        }

        private void FlameDices(AbilityFlameEffect abilityFlameEffect)
        {
            var dices = _guardianList.SelectMany(guardian => guardian.Dices).Where(dice=>dice.FlamedSide == -1).OrderBy(_ => Random.value).ToArray();
            var dicesToFlame = dices.Take(Mathf.Min(dices.Count(), abilityFlameEffect.FlameAmount));
            foreach (var dice in dicesToFlame)
            {
                dice.FlameActiveSide();
            }
        }


        public TSource Min<TSource, TCompare>(IEnumerable<TSource> elements, Func<TSource, TCompare> compareValue) where TCompare : IComparable
        {
            IEnumerator<TSource> elementsEnumerator = elements.GetEnumerator();
            if (!elementsEnumerator.MoveNext())
            {
                throw new ArgumentException();
            }

            TSource minSource = elementsEnumerator.Current;
            TCompare minValue = compareValue(minSource);

            while (elementsEnumerator.MoveNext())
            {
                var value = compareValue(elementsEnumerator.Current);
                if (value.CompareTo(minValue) >= 0)
                {
                    continue;
                }

                minSource = elementsEnumerator.Current;
                minValue = value;
            }

            return minSource;
        }
    }
}