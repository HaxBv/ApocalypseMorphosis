using UnityEngine;
public enum DisasterLevelEnemy
{
    None,
    Tiger,
    Orgue,
    Demon,
    Dragon,
    God
}
[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "ApocalypseMorphosis/EnemyDataSO")]

public class EnemyDataSO : ScriptableObject
{
    public string EnemyName;
    public ulong ID;

    public DisasterLevelEnemy disaster;
    public int Health;
    public int Damage;
    public float Speed;

    public float SpawnRate;

    public GameObject prefab;
}
