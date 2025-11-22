using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    private PlayerStats currentPlayerStats;

    /*private void OnEnable()
    {
        if (FormManager.Instance != null)
            FormManager.Instance.OnPlayerChanged += OnChangeStats;
    }

    private void OnDisable()
    {
        if (FormManager.Instance != null)
            FormManager.Instance.OnPlayerChanged -= OnChangeStats;
    }

    private void OnChangeStats(PlayerStats stats)
    {
        if (currentPlayerStats != null)
            currentPlayerStats.OnHealthChanged -= UpdateHealth;

        currentPlayerStats = stats;

        if (currentPlayerStats != null)
            currentPlayerStats.OnHealthChanged += UpdateHealth;

        if (currentPlayerStats != null)
            UpdateHealth(currentPlayerStats.currentHealth, currentPlayerStats.maxHealth);
    }

    private void UpdateHealth(int current, int max)
    {
        if (healthText != null)
            healthText.text = $"{current} / {max}";
    }*/
}
