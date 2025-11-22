using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;

    /*private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Array que guarda la vida de cada forma por índice
    private int[] formHealth;

    public void Init(int totalForms)
    {
        formHealth = new int[totalForms];
        for (int i = 0; i < totalForms; i++)
            formHealth[i] = 0; // -1 indica que nunca se ha instanciado
    }

    public void SaveHealth(int formIndex, int currentHealth)
    {
        formHealth[formIndex] = currentHealth;
    }

    public int GetHealth(int formIndex, int baseHealth)
    {
        if (formHealth[formIndex] < 0)
            return baseHealth;
        return formHealth[formIndex];
    }*/
}
