using TMPro;
using UnityEngine;
using UserInterface.Core;

namespace UserInterface.Views
{
    public class StatusView : MonoBehaviour, IView
    {
        [SerializeField] private TextMeshProUGUI _health;

        public void SetHealth(int maxHealth, int health)
        {
            _health.text = $"{health}/{maxHealth}";
        }
    }
}