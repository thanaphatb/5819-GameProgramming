using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;

    public void HealthUpdate(int playerHealth)
    {
        healthText.text = $"Lives: {playerHealth}";
    }
}