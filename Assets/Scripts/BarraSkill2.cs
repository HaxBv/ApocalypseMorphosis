using UnityEngine;
using UnityEngine.UI;

public class BarraSkill2 : MonoBehaviour
{
    public FormsDataSO CD;


    public Image rellenoBarraSkill2;
    void Start()
    {
        CD.RecargaActualSkill2 = CD.TiempoMaximoRecarga2;

       

    }

    void Update()
    {
        rellenoBarraSkill2.fillAmount = (CD.RecargaActualSkill2 / CD.TiempoMaximoRecarga2);
    }
}