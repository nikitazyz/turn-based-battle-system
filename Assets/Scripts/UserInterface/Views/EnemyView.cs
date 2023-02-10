using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Views
{
    public class EnemyView : CharacterView
    {
        [SerializeField] private Image _dice;

        public bool DiceEnabled
        {
            get => _dice.gameObject.activeSelf;
            set => _dice.gameObject.SetActive(value);
        }
        public Sprite Dice
        {
            get => _dice.sprite;
            set => _dice.sprite = value;
        }
    }
}