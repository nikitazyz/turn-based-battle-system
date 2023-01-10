using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Views.Elements
{
    public class HealthBarElement : MonoBehaviour
    {
        [SerializeField] private Image _bar;
        [SerializeField] private Image _secondaryBar;
        [SerializeField] private float _secondaryBarSpeed;
        [SerializeField] private TextMeshProUGUI _valueField;

        private int _maxValue;
        private int _currentValue;

        private Coroutine _secondaryUpdateCoroutine;
        
        public int MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                UpdateView();
            }
        }

        public int CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = value;
                UpdateView();
            }
        }

        private void UpdateView()
        {
            _valueField.text = $"{_currentValue}/{_maxValue}";
            
            if (_maxValue == 0)
            {
                _bar.fillAmount = 0;
            }
            else
            {
                _bar.fillAmount = (float)_currentValue / _maxValue;
            }

            if (_secondaryUpdateCoroutine != null)
            {
                StopCoroutine(_secondaryUpdateCoroutine);
            }

            _secondaryUpdateCoroutine = StartCoroutine(SecondaryUpdate());
        }

        IEnumerator SecondaryUpdate()
        {
            while (Mathf.Abs(_secondaryBar.fillAmount - _bar.fillAmount) > 0.05f)
            {
                _secondaryBar.fillAmount = Mathf.Lerp(_secondaryBar.fillAmount, _bar.fillAmount, Time.deltaTime * _secondaryBarSpeed);
                yield return null;
            }

            _secondaryBar.fillAmount = _bar.fillAmount;
        }
    }
}