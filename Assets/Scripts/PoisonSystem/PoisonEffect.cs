using System;
using UnityEngine;

namespace PoisonSystem
{
    public class PoisonEffect
    {
        private int _poisonCounter;
        public event Action<int> Changed;

        public int PoisonCounter
        {
            get => _poisonCounter;
            set
            {
                _poisonCounter = Mathf.Max(value, 0);
                Changed?.Invoke(_poisonCounter);
            }
        }
    }
}