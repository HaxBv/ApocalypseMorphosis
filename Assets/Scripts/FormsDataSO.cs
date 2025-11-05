using UnityEngine;
public enum ActiveForm
{
    InkMan,
    Shadow,
    Zero,
    MaximWolf,
    Konquest,
    Draco,
    Trueno,
}
[CreateAssetMenu(fileName = "FormsDataSO", menuName = "ApocalypseMorphosis/Forms/FormsDataSO")]
public class FormsDataSO : ScriptableObject
{
    
    public ulong ID;

    public ActiveForm ActualForm;
    public int Health;
    public int Damage;
    public int Armor;
    public bool BuffIsActive;
    public float Speed;

    
}
