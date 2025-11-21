using System;
using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour
{
    public static PlayerLevelSystem Instance;

    [Header("Level")]
    public int Level = 1;
    public int LevelMax = 100;

    [Header("Experience (Linear Scaling)")]
    public float XpActual = 0;
    public float XpBase = 100f;
    public float XpIncrease = 50f;
    public float XpToLevelUp;

    public event Action OnLevelUp;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        XpToLevelUp = CalcularXpNecesaria(Level);
    }

    public void GanarXP(float cantidad)
    {
        if (Level >= LevelMax)
        {
            XpActual = XpToLevelUp;
            return;
        }

        XpActual += cantidad;

        while (XpActual >= XpToLevelUp && Level < LevelMax)
            SubirNivel();

        if (Level >= LevelMax)
            XpActual = XpToLevelUp;
    }


    private void SubirNivel()
    {
        if (Level >= LevelMax) return;

        XpActual -= XpToLevelUp;
        Level++;

        XpToLevelUp = CalcularXpNecesaria(Level);

        OnLevelUp?.Invoke();
        
    }

    private float CalcularXpNecesaria(int nivel)
    {
        return XpBase + (nivel - 1) * XpIncrease;
    }
}
