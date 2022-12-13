using System;
using System.Collections.Generic;
using System.Linq;
using Guardians;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Core;
using UserInterface.Views.MoveViewElements;

namespace UserInterface.Views
{
    public class MoveView : MonoBehaviour, IView
    {
        public event Action EndMoveClicked;
        public event Action<int, int> DiceUsed; 

        [SerializeField] private Button _endMoveButton;
        [SerializeField] private GuardianCellElement[] _guardianCellElements;
        [SerializeField] private TextMeshProUGUI _actionsLeft;

        public GuardianCellElement[] GuardianCellElements => _guardianCellElements.ToArray();

        public bool EndMoveButtonActiveState
        {
            get => _endMoveButton.interactable;
            set => _endMoveButton.interactable = value;
        }

        public void SetActions(int actions, int maxActions)
        {
            _actionsLeft.text = $"{actions}/{maxActions}";
        }
        
        private void Awake()
        {
            _endMoveButton.onClick.AddListener(() => EndMoveClicked?.Invoke());
            for (int i = 0; i < _guardianCellElements.Length; i++)
            {
                var i1 = i;
                _guardianCellElements[i].DiceUsed += j => DiceUsed?.Invoke(i1, j);
            }
        }
    }
}