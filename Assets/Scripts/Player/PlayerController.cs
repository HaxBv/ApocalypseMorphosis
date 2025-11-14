using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FormsDataSO data;
    public static PlayerController Instance;
    public PlayerInputs InputManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        InputManager = GetComponent<PlayerInputs>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void IniciarRecarga()
    {
        data.RecargaActualSkill1 = data.TiempoMaximoRecarga1;
    }

    public void ActualizarRecarga()
    {
        print("Recargando");
        if (data.RecargaActualSkill1 > 0f)
            data.RecargaActualSkill1 -= Time.deltaTime;
        if (data.RecargaActualSkill1 < 0f)
            data.RecargaActualSkill1 = 0f;

    }
    public float Progreso()
    {
        return 1f - (data.RecargaActualSkill1 / data.TiempoMaximoRecarga1);

    }
}
