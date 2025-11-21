using UnityEngine;

public enum HabilidadTipo
{
    Projectile,
    Buff
}

[CreateAssetMenu(fileName = "SkillsDataSO", menuName = "ApocalypseMorphosis/SkillsDataSO")]
public class SkillsDataSO : ScriptableObject
{
    [Header("General")]
    public string owner;
    public HabilidadTipo tipo;


    [Header("Projectile")]
    public float baseDamage;
    public float baseSpeed;
    public float baseLifetime;
    public float damagePerLevel;
    public float speedPerLevel;
    public float lifetimePerLevel;
    public GameObject prefab;

    [Header("Buffos")]
    public float bonusDamage;
    public float bonusArmor;
    public float bonusSpeed;
    public float bonusAttackSpeed;
    public float duration;
}
