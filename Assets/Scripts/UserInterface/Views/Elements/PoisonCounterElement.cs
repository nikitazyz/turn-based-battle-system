using System;
using TMPro;
using UnityEngine;

namespace UserInterface.Views.Elements
{
    public class PoisonCounterElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterField;

        private int _value;
        
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                UpdateView();
            }
        }

        private void Awake()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            _counterField.text = _value.ToString();
            gameObject.SetActive(_value > 0);
        }
    }
}
