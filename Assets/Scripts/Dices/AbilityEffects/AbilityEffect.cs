using UnityEngine;

namespace Dices.AbilityEffects
{
    public abstract class AbilityEffect : ScriptableObject
    {
#if UNITY_EDITOR
        #pragma warning disable CS0414
        // ReSharper disable once NotAccessedField.Local
        [SerializeField, HideInInspector] private bool _isOpen = true;
#endif

        public abstract void Process(AttackStatus attackStatus);
    }
}