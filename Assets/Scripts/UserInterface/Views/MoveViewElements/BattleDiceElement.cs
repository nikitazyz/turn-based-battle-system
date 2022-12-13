using System;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Views.MoveViewElements
{
    public class BattleDiceElement : MonoBehaviour
    {
        public event Action Used;
        
        [SerializeField] private Image _image;
        [SerializeField] private Button _battleDiceUseButton;

        public bool Enabled
        {
            get => _image.gameObject.activeSelf;
            set => _image.gameObject.SetActive(value);
        }

        public Sprite Image
        {
            get => _image.sprite;
            set => _image.sprite = value;
        }

        private void Awake()
        {
            _battleDiceUseButton.onClick.AddListener(() => Used?.Invoke());
        }

        private void OnDestroy()
        {
            _battleDiceUseButton.onClick.RemoveAllListeners();
        }
    }
}
