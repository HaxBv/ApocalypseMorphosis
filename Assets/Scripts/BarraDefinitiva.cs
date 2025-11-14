using UnityEngine;
using UnityEngine.UI;

public class BarraDefinitiva : MonoBehaviour
{
    public FormsDataSO CD;


    public Image rellenoBarraDefinitiva;
    void Start()
    {
        CD.RecargaActualDefinitiva = CD.TiempoMaximoDefinitiva;
    }

    void Update()
    {
        rellenoBarraDefinitiva.fillAmount = (CD.RecargaActualDefinitiva / CD.TiempoMaximoDefinitiva);
    }
}
