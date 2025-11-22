using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public FormsDataSO baseFormData;

    public int currentHealth;
    public int maxHealth;
    public int currentDamage;
    public int currentArmor;
    public float currentSpeedMovement;
    public float currentSpeedAttack;

    public int HealthPerLevel;
    public int DamagePerLevel;
    public int ArmorPerLevel;
    public float SpeedMovementPerLevel;
    public float SpeedAttackPerLevel;

    [HideInInspector] public int buffDamage;
    [HideInInspector] public int buffArmor;
    [HideInInspector] public float buffSpeedMovement;
    [HideInInspector] public float buffSpeedAttack;

    public int FormIndex;

    private void OnEnable()
    {
        maxHealth = baseFormData.Health;
        AplicarMejorasPorNivel();
        AplicarBaseStats();
        

        PlayerLevelSystem.Instance.OnLevelUp += AplicarMejorasPorNivel;
    }

    private void OnDisable()
    {
        PlayerLevelSystem.Instance.OnLevelUp -= AplicarMejorasPorNivel;
    }

    public void AplicarBaseStats()
    {
        if (baseFormData == null) return;

        maxHealth = baseFormData.Health;
        currentHealth = maxHealth;

        currentDamage = baseFormData.Damage;
        currentArmor = baseFormData.Armor;
        currentSpeedMovement = baseFormData.SpeedMovement;
        currentSpeedAttack = baseFormData.SpeedAttack;
    }

    public void AplicarMejorasPorNivel()
    {
        int nivel = PlayerLevelSystem.Instance.Level - 1;

        int oldMax = maxHealth;
        maxHealth = baseFormData.Health + (nivel * HealthPerLevel);

        // Mantener la vida proporcional si quieres
        float porcentajeVida = (float)currentHealth / oldMax;
        currentHealth = Mathf.RoundToInt(porcentajeVida * maxHealth);

        currentDamage = baseFormData.Damage + (nivel * DamagePerLevel) + buffDamage;
        currentArmor = baseFormData.Armor + (nivel * ArmorPerLevel) + buffArmor;
        currentSpeedMovement = baseFormData.SpeedMovement + (nivel * SpeedMovementPerLevel) + buffSpeedMovement;
        currentSpeedAttack = baseFormData.SpeedAttack + (nivel * SpeedAttackPerLevel) + buffSpeedAttack;
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
