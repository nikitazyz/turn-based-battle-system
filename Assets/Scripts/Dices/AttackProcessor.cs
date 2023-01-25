using System;
using System.Collections.Generic;
using System.Linq;
using Dices.AbilityEffects;
using Enemies;
using PlayerSystem;

namespace Dices
{
    public class AttackProcessor
    {
        private readonly EnemyList _enemyList;
        private readonly BattlePlayer _battlePlayer;

        private EffectTargetType _effectTargetType;
        private int _damageOffset;
        private int _nextPhysicalDamageScale = 1;

        private List<Func<int, int>> _damageModifiers;
        private List<Func<int, int>> _physicalDamageModifiers;

        public AttackProcessor(EnemyList enemyList, BattlePlayer battlePlayer)
        {
            _enemyList = enemyList;
            _battlePlayer = battlePlayer;
        }

        public void UseDice(BattleDice battleDice)
        {
            battleDice.Use();
            var ability = battleDice.DiceAbility();
            Attack(ability);
        }

        private void Attack(DiceAbility ability)
        {
            BattleEnemy[] enemies = GetTargetEnemies();
            var damageOffset = _damageOffset;
            var nextPhysicalDamageScale = _nextPhysicalDamageScale;

            foreach (var abilityEffect in ability.GetEffects())
            {
                switch (abilityEffect)
                {
                    case DamageEffect damageEffect:
                        DamageTargetEnemies(damageEffect.Damage * nextPhysicalDamageScale + damageOffset, enemies);
                        _damageOffset = 0;
                        _nextPhysicalDamageScale = 1;
                        break;
                    case HealEffect healEffect:
                        _battlePlayer.Health.Heal(healEffect.Heal);
                        break;
                    case SelfDamageEffect selfDamageEffect:
                        _battlePlayer.Health.TakeDamage(selfDamageEffect.SelfDamage);
                        break;
                    case NextDamageTypeEffect nextDamageTypeEffect:
                        _effectTargetType = nextDamageTypeEffect.EffectTargetType;
                        break;
                    case NextDamageOffsetEffect nextDamageOffsetEffect:
                        _damageOffset += nextDamageOffsetEffect.DamageOffset;
                        break;
                    case ScaleNextDamageEffect scaleNextDamageEffect:
                        _nextPhysicalDamageScale = _nextPhysicalDamageScale == 1 ? scaleNextDamageEffect.Scale : _nextPhysicalDamageScale + nextPhysicalDamageScale;
                        break;
                }
            }
        }

        private void DamageTargetEnemies(int damage, BattleEnemy[] enemies)
        {
            foreach (var battleEnemy in enemies)
            {
                battleEnemy.Health.TakeDamage(damage);
            }
        }

        private BattleEnemy[] GetTargetEnemies()
        {
            var effectTarget = _effectTargetType;
            _effectTargetType = EffectTargetType.First;
            return effectTarget switch
            {
                EffectTargetType.First => new[] { _enemyList.FirstAlive },
                EffectTargetType.Last => new[] { _enemyList.LastAlive },
                EffectTargetType.All => _enemyList.AllAlive.ToArray(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}