using UnityEngine;
using UnityEngine.UI;

public class BarraSkill1 : MonoBehaviour
{
    public FormsDataSO CD;

    
    public Image rellenoBarraSkill1;

   

    
    void Start()
    {
        CD.RecargaActualSkill1 = CD.TiempoMaximoRecarga1;

       

    }

    void Update()
    {
        rellenoBarraSkill1.fillAmount = (CD.RecargaActualSkill1 / CD.TiempoMaximoRecarga1);
    }
}
