using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Views.DebugParametersElements;
using UserInterface.Views.Elements;

namespace UserInterface.Views
{
    public class DebugParametersView : MonoBehaviour
    {
        public event Action<PlayClickedEventArgs> PlayClicked;

        [SerializeField] private CheckboxTextFieldElement _playerHealthParameter;
        [SerializeField] private EnemyParametersElement[] _enemyParametersElements;
        [SerializeField] private Button _play;

        private int[] _healths;
        private void Awake()
        {
            bool lastValid = true;
            foreach (var enemyParametersElement in _enemyParametersElements)
            {
                enemyParametersElement.DropDownSelected += (i) => EnemyParametersElementOnDropDownSelected(enemyParametersElement, i);
                enemyParametersElement.Interactable = lastValid;
                lastValid = enemyParametersElement.DropDownValue != 0;
            }
            _play.onClick.AddListener(OnPlayClicked);
        }

        private void OnPlayClicked()
        {
            int[] enemies = _enemyParametersElements.Select(enemyParameter => enemyParameter.DropDownValue).ToArray();
            int?[] enemiesOverride = _enemyParametersElements.Select<EnemyParametersElement, int?>(enemyParameter => enemyParameter.IsOverrideHealth ? enemyParameter.OverrideHealthValue : null).ToArray();

            PlayClickedEventArgs eventArgs = new PlayClickedEventArgs()
            {
                Enemies = enemies,
                EnemiesOverrideHealth = enemiesOverride,
                OverrideHealth = _playerHealthParameter.Enabled ? int.Parse(_playerHealthParameter.Value) : null
            };
            
            PlayClicked?.Invoke(eventArgs);
        }

        public void SetEnemiesList(string[] enemies)
        {
            List<string> enemiesList = new List<string> { "None" };
            enemiesList.AddRange(enemies);
            foreach (var enemyParametersElement in _enemyParametersElements)
            {
                enemyParametersElement.AddDropdownItems(enemiesList);
            }
        }

        public void SetEnemiesDefaultHealth(int[] healths)
        {
            _healths = new int[healths.Length];
            healths.CopyTo(_healths, 0);
        }

        public void SetPlayerDefaultHealth(int health)
        {
            _playerHealthParameter.Value = health.ToString();
        }

        private void EnemyParametersElementOnDropDownSelected(EnemyParametersElement element, int value)
        {
            if (_healths.Length > value)
            {
                element.OverrideHealthValue = _healths[value];
            }
            bool lastValid = true;
            foreach (var enemyParametersElement in _enemyParametersElements)
            {
                enemyParametersElement.Interactable = lastValid;
                lastValid = enemyParametersElement.DropDownValue != 0;
            }
        }
    }
}