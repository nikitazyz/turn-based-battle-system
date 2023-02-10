using System;
using System.Collections.Generic;
using System.Linq;
using Dices;
using Dices.AbilityEffects;
using Enemies;
using PlayerSystem;
using PoisonEffect = Dices.AbilityEffects.PoisonEffect;

namespace AttackSystem
{
    public sealed class PlayerAttackProcessor : AttackProcessor
    {

        private EffectTargetType _effectTargetType;
        private int _damageOffset;
        private int _nextPhysicalDamageScale = 1;

        public PlayerAttackProcessor(EnemyList enemyList, BattlePlayer battlePlayer) : base(enemyList, battlePlayer)
        {
        }

        protected override void Attack(DiceAbility ability, BattleDice battleDice)
        {
            if (battleDice.IsFlameOnActiveSide)
            {
                BattlePlayer.Health.TakeDamage(1);
                battleDice.ResetFlame();
            }
            
            BattleEnemy[] enemies = GetTargetEnemies();
            var damageOffset = _damageOffset;
            var nextPhysicalDamageScale = _nextPhysicalDamageScale;

            foreach (var abilityEffect in ability.GetEffects())
            {
                switch (abilityEffect)
                {
                    case DamageEffect damageEffect:
                        DamageTargetEnemies((damageEffect.Damage + damageOffset) * nextPhysicalDamageScale, enemies);
                        _damageOffset = 0;
                        _nextPhysicalDamageScale = 1;
                        break;
                    case HealEffect healEffect:
                        BattlePlayer.Health.Heal(healEffect.Heal);
                        break;
                    case SelfDamageEffect selfDamageEffect:
                        BattlePlayer.Health.TakeDamage(selfDamageEffect.SelfDamage);
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
                    case PoisonEffect poisonEffect:
                        PoisonTargetEnemy(poisonEffect, enemies);
                        break;
                    case SelfPoisonEffect selfPoisonEffect:
                        BattlePlayer.PoisonEffect.PoisonCounter += selfPoisonEffect.Damage;
                        break;
                }
            }
        }

        private void PoisonTargetEnemy(PoisonEffect poisonEffect, BattleEnemy[] battleEnemies)
        {
            foreach (var enemy in battleEnemies)
            {
                enemy.PoisonEffect.PoisonCounter += poisonEffect.Damage;
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
                EffectTargetType.First => new[] { EnemyList.FirstAlive },
                EffectTargetType.Last => new[] { EnemyList.LastAlive },
                EffectTargetType.All => EnemyList.AllAlive.ToArray(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}