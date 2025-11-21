using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public FormsDataSO baseFormData;

    public int currentHealth;
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


    private void OnEnable()
    {
        AplicarStatsBase();
        PlayerLevelSystem.Instance.OnLevelUp += AplicarMejorasPorNivel;
    }

    private void OnDisable()
    {
        PlayerLevelSystem.Instance.OnLevelUp -= AplicarMejorasPorNivel;
    }

    private void AplicarStatsBase()
    {

        currentHealth = baseFormData.Health;
        currentDamage = baseFormData.Damage;
        currentArmor = baseFormData.Armor;
        currentSpeedMovement = baseFormData.SpeedMovement;
        currentSpeedAttack = baseFormData.SpeedAttack;
    }

    public void AplicarMejorasPorNivel()
    {
        int nivel = PlayerLevelSystem.Instance.Level - 1;

        currentHealth = baseFormData.Health + (nivel * HealthPerLevel);
        currentDamage = baseFormData.Damage + (nivel * DamagePerLevel) + buffDamage;
        currentArmor = baseFormData.Armor + (nivel * ArmorPerLevel) + buffArmor;
        currentSpeedMovement = baseFormData.SpeedMovement + (nivel * SpeedMovementPerLevel) + buffSpeedMovement;
        currentSpeedAttack = baseFormData.SpeedAttack + (nivel * SpeedAttackPerLevel) + buffSpeedAttack;
    }
}
