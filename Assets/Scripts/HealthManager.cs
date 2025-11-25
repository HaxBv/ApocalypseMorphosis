using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;

    public int currentHealth;
    public int maxHealth;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void InitHealth(int baseHealth)
    {
        maxHealth = baseHealth;
        currentHealth = maxHealth;
    }

    public void UpdateMaxHealth(int newMax)
    {
        float percent = (float)currentHealth / maxHealth;
        maxHealth = newMax;
        currentHealth = Mathf.RoundToInt(maxHealth * percent);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }
}
