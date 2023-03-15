using System;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

namespace AudioSystem
{
    [CreateAssetMenu]
    public class EventReferencesAsset : ScriptableObject
    {
        public BattleEventReferences BattleEvents;
        public MoveEventReferences MoveEvents;
        public DiceEventReferences DiceEvents;
        public DamageEventReferences DamageEvents;
        public DeathEventReferences DeathEvents;
        public MusicEventReferences MusicEvents;
        public MagicEventReferences MagicEvents;
        public AttackEventReferences AttackEvents;
        
        [Serializable]
        public class BattleEventReferences
        {
            public EventReference Victory;
            public EventReference Lose;
        }

        [Serializable]
        public class MoveEventReferences
        {
            public EventReference BeginMove;
            public EventReference EndMove;
            public EventReference NoActions;
        }

        [Serializable]
        public class DiceEventReferences
        {
            public EventReference Reroll;
            public EventReference Use;
        }

        [Serializable]
        public class DamageEventReferences
        {
            public EventReference Player;
            public EventReference Sword;
        }

        [Serializable]
        public class DeathEventReferences
        {
            public EventReference Herald;
            public EventReference Punisher;
            public EventReference Paladin;
        }

        [Serializable]
        public class MusicEventReferences
        {
            public EventReference Battle;
        }

        [Serializable]
        public class MagicEventReferences
        {
            public EventReference Use;
            public EventReference Mana;
        }

        [Serializable]
        public class AttackEventReferences
        {
            public EventReference Poisoning;
            public EventReference Ignition;
            public EventReference Heal;
        }
    }
}