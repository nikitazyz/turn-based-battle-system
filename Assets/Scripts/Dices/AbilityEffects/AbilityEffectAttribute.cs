using System;

namespace Dices.AbilityEffects
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AbilityEffectAttribute : Attribute
    {
        public string MenuName;
    }
}