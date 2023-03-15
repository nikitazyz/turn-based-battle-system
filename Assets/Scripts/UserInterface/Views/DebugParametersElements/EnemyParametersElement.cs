using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Views.Elements;

namespace UserInterface.Views.DebugParametersElements
{
    public class EnemyParametersElement : MonoBehaviour
    {
        public event Action<int> DropDownSelected;
        
        [SerializeField] private TMP_Dropdown _enemyDropdown;
        [SerializeField] private CheckboxTextFieldElement _enemyHealthParameter;
        [SerializeField] private bool _interactable;

        public bool Interactable
        {
            get => _interactable;
            set
            {
                _interactable = value;
                _enemyDropdown.interactable = value;
                _enemyHealthParameter.Interactable = value;
            }
        }

        public int DropDownValue
        {
            get => _enemyDropdown.value;
            set => _enemyDropdown.value = value;
        }

        public bool IsOverrideHealth
        {
            get => _enemyHealthParameter.Enabled;
            set => _enemyHealthParameter.Enabled = value;
        }

        public int OverrideHealthValue
        {
            get => int.Parse(_enemyHealthParameter.Value);
            set => _enemyHealthParameter.Value = value.ToString();
        }

        private void Awake()
        {
            _enemyDropdown.options.Clear();
            _enemyDropdown.onValueChanged.AddListener(i => DropDownSelected?.Invoke(i));
        }

        public void AddDropdownItems(List<string> elements)
        {
            _enemyDropdown.AddOptions(elements);
        }
    }
}