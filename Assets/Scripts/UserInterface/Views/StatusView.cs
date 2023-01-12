using TMPro;
using UnityEngine;

namespace UserInterface.Views
{
    public class StatusView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _health;

        public void SetHealth(int maxHealth, int health)
        {
            _health.text = $"{health}/{maxHealth}";
        }
    }
}