using UnityEngine;
public enum RangeForm
{
    
    S,
    A,
    B,
    C,

}
[CreateAssetMenu(fileName = "FormsDataSO", menuName = "ApocalypseMorphosis/FormsDataSO")]
public class FormsDataSO : ScriptableObject
{
    public string Name;
    public ulong ID;

    public RangeForm rangeForm;

    public int Health;
    public int Damage;
    public int Armor;
    public float SpeedMovement;
    public float SpeedAttack;

    public float Skill1Cost;
    public float Skill2Cost;
    public float DefinitivaCost;


    public float RecargaActualSkill1;
    public float TiempoMaximoRecarga1;

    public float RecargaActualSkill2;
    public float TiempoMaximoRecarga2;

    public float RecargaActualDefinitiva;
    public float TiempoMaximoDefinitiva;



}
