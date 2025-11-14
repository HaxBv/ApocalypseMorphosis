using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float EnergiaActual;
    public float EnergiaMaxima = 100f;
    private float EnergiaMinima = 0f;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
    }

    void Start()
    {
        EnergiaActual = EnergiaMaxima;
    }


    void Update()
    {
        OverLoad();
    }

    public void OverLoad()
    {

        if(EnergiaActual <= EnergiaMinima)
            EnergiaActual = EnergiaMinima;

        if(EnergiaActual >= EnergiaMaxima)
            EnergiaActual = EnergiaMaxima;
       // EnergiaActual = Mathf.Max(0, EnergiaActual - UsarEnergia());
    }
    public bool UsarEnergia(float cantidad)
    {
        if (EnergiaActual >= cantidad)
        {
            EnergiaActual -= cantidad;
            return true;
        }
        return false;
    }




}
