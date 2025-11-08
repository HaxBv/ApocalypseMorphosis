using UnityEngine;
public enum RangeForm
{
    
    S,
    A,
    B,
    C,

}
[CreateAssetMenu(fileName = "FormsDataSO", menuName = "ApocalypseMorphosis/Forms/FormsDataSO")]
public class FormsDataSO : ScriptableObject
{
    public string Name;
    public ulong ID;

    public RangeForm rangeForm;
    public int Health;
    public int Damage;
    public int Armor;
    public bool BuffIsActive;
    public float Speed;

    
}
