using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Referencias a Textos")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI playerLevelText;
    /*public TextMeshProUGUI abilityQText;
    public TextMeshProUGUI abilityFText;
    public TextMeshProUGUI abilityUltimateText;*/


    private void Start()
    {
        playerLevelText.text = $"Nivel: {PlayerLevelSystem.Instance.Level}";
    }
    private void Update()
    {
        UpdateHealth();
        
    }
    private void OnEnable()
    {
        if (PlayerLevelSystem.Instance != null)
            PlayerLevelSystem.Instance.OnLevelUp += UpdatePlayerLevel;

        
    }

    private void OnDisable()
    {
        if (PlayerLevelSystem.Instance != null)
            PlayerLevelSystem.Instance.OnLevelUp -= UpdatePlayerLevel;
    }

    private void UpdateHealth()
    {
        if (FormManager.Instance != null && FormManager.Instance.currentPlayer != null)
        {
            PlayerStats stats = FormManager.Instance.currentPlayer.GetComponent<PlayerStats>();
            if (stats != null)
            {
                int current = stats.currentHealth; 
                int max = stats.maxHealth; 
                healthText.text = $"{current}/{max}";
            }
        }
    }

    private void UpdatePlayerLevel()
    {
        if (PlayerLevelSystem.Instance != null)
        {
            playerLevelText.text = $"Nivel: {PlayerLevelSystem.Instance.Level}";
        }

        //UpdateAbilityLevels();
    }



}
