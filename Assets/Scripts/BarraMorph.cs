using UnityEngine;
using UnityEngine.UI;

public class BarraMorph : MonoBehaviour
{
    public Image rellenoBarraMorph; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FormManager.Instance != null)
        {
            float porcentaje = FormManager.Instance.currentMorphCooldown / FormManager.Instance.maxMorphCooldown;
            rellenoBarraMorph.fillAmount = porcentaje;
        }
    }
}
