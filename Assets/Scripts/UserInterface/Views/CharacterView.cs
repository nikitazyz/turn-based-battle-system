using UnityEngine;
using UserInterface.Views.Elements;

namespace UserInterface.Views
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _characterRenderer;
        [SerializeField] private HealthBarElement _healthBarElement;

        public Sprite CharacterSprite
        {
            get => _characterRenderer.sprite;
            set => _characterRenderer.sprite = value;
        }

        public int HealthValue
        {
            get => _healthBarElement.CurrentValue;
            set => _healthBarElement.CurrentValue = value;
        }

        public int MaxHealthValue
        {
            get => _healthBarElement.MaxValue;
            set => _healthBarElement.MaxValue = value;
        }
    }
}
