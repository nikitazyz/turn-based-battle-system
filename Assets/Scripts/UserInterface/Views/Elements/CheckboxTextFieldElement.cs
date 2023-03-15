using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Views.Elements
{
    public class CheckboxTextFieldElement : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Toggle _toggle;

        public bool Interactable
        {
            get => _toggle.interactable;
            set => _toggle.interactable = value;
        }
        
        public bool Enabled
        {
            get => _toggle.isOn;
            set => _toggle.isOn = value;
        }

        public string Value
        {
            get => _inputField.text;
            set => _inputField.text = value;
        }

        private void Update()
        {
            _inputField.interactable = _toggle.isOn && _toggle.interactable;
        }
    }
}