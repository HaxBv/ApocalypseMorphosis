using UnityEngine;
using UnityEngine.UI;

public class BarraXP : MonoBehaviour
{
    

    public Image rellenoBarraXP;



    void Start()
    {

    }


    void Update()
    {
        if (PlayerLevelSystem.Instance != null)
        {
            float porcentaje = PlayerLevelSystem.Instance.XpActual/ PlayerLevelSystem.Instance.XpToLevelUp;
            rellenoBarraXP.fillAmount = porcentaje;
        }
    }
}