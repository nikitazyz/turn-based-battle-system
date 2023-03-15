using System;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Views
{
    public class EndGameView : MonoBehaviour
    {
        public event Action RestartClicked;
        public event Action MainMenuClicked;
        
        [SerializeField] private GameObject _root;
        [SerializeField] private Button _restart;
        [SerializeField] private Button _mainMenu;

        public bool Enabled
        {
            get => _root.activeSelf;
            set => _root.SetActive(value);
        }
        
        private void Awake()
        {
            _restart.onClick.AddListener(() => RestartClicked?.Invoke());
            _mainMenu.onClick.AddListener(() => MainMenuClicked?.Invoke());
        }
    }
}