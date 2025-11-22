using UnityEngine;
using UnityEngine.UI;

public class BarraEnergia : MonoBehaviour
{
    

    public Image rellenoBarraEnergia;
    


    void Start()
    {
        
    }


    void Update()
    {
        if (GameManager.Instance != null)
        {
            float porcentaje = GameManager.Instance.EnergiaActual / GameManager.Instance.EnergiaMax;
            rellenoBarraEnergia.fillAmount = porcentaje;
        }
    }
}
