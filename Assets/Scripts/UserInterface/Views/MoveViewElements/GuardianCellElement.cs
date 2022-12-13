using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Views.MoveViewElements
{
    public class GuardianCellElement : MonoBehaviour
    {
        public event Action<int> DiceUsed; 

        [SerializeField] private BattleDiceElement[] _battleDiceElements;
        [SerializeField] private Image _avatarImage;

        public Sprite Avatar
        {
            get => _avatarImage.sprite;
            set => _avatarImage.sprite = value;
        }
        public BattleDiceElement[] BattleDiceElements => _battleDiceElements.ToArray();

        private void Awake()
        {
            for (var i = 0; i < _battleDiceElements.Length; i++)
            {
                var battleDiceElement = _battleDiceElements[i];
                var i1 = i;
                battleDiceElement.Used += () => DiceUsed?.Invoke(i1);
            }
        }
    }
}
