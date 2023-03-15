using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Views.MoveViewElements
{
    public class GuardianRerollButtonElement : MonoBehaviour
    {
        public event Action RerollClicked;
        
        [SerializeField] private Button _reroll;
        [SerializeField] private TextMeshProUGUI _rerollCountField;
        private int _rerollCount;

        public bool Interactable
        {
            get => _reroll.interactable;
            set => _reroll.interactable = value;
        }

        public int RerollCount
        {
            get => _rerollCount;
            set
            {
                _rerollCount = value;
                _rerollCountField.text = _rerollCount.ToString();
            }
        }

        private void Awake()
        {
            _reroll.onClick.AddListener(() => RerollClicked?.Invoke());
        }
    }
}