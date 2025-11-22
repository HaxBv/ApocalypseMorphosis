using UnityEngine;
using UnityEngine.UI;

public class BarraControl : MonoBehaviour
{
    public Image rellenoBarraControl;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (GameManager.Instance != null)
        {
            float porcentaje = GameManager.Instance.ControlActual / GameManager.Instance.ControlMax;
            rellenoBarraControl.fillAmount = porcentaje;
        }

    }
}
