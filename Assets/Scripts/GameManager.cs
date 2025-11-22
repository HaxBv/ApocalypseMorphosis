using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float EnergiaActual;
    public float EnergiaMax = 100f;
    private float EnergiaMin = 0f;

    public GameObject currentPlayer;
    public InkMan currentInkman;

    public float ControlActual;
    public float ControlMax = 100f;
    public float ControlMin = 0f;

    public float controlRecoveryRate;
    public float controlDrainRate;

    public float currentMorphCooldown = 0f; 
    public float maxMorphCooldown;


    public GameObject panelSeleccion;


    public InputSystem_Actions input;

    public bool IsSelectingForm = false;

    private void Awake()
    {
        input = new();
        if (Instance == null)
            Instance = this;
        panelSeleccion.SetActive(false);
    }

    void Start()
    {
        EnergiaActual = EnergiaMax;
        ControlActual = ControlMax;
    }
    private void OnEnable()
    {
        input.Enable();

        input.UI.ChangeForm.started += OnSpaceDown;

        
        input.UI.ChangeForm.canceled += OnSpaceUp;

    }
    private void OnSpaceDown(InputAction.CallbackContext context)
    {
        panelSeleccion.SetActive(true);
        IsSelectingForm = true;
    }

    private void OnSpaceUp(InputAction.CallbackContext context)
    {
        panelSeleccion.SetActive(false);
        IsSelectingForm = false;
    }



    private void OnDisable()
    {
        input.Disable();


        
        input.UI.ChangeForm.started -= OnSpaceDown;
        input.UI.ChangeForm.canceled -= OnSpaceUp;

    }

   

    void Update()
    {
        OverLoad();
        ActualizarControl();

    }


    

    public void ActualizarControl()
    {
        // Buscar GameObject con tag Player
        currentPlayer = GameObject.FindGameObjectWithTag("Player");

        if (currentPlayer != null)
        {
            // Ver si el script Inkman está en ese objeto
            currentInkman = currentPlayer.GetComponent<InkMan>();

            if (currentInkman != null)
            {
                // Player actual = Inkman  recuperar control
                ControlActual += controlRecoveryRate * Time.deltaTime;
            }
            else
            {
                // Player actual NO es Inkman drenar control
                ControlActual -= controlDrainRate * Time.deltaTime;
            }
        }
        else
        {
            // No hay player  drenar también
            currentInkman = null;
            ControlActual -= controlDrainRate * Time.deltaTime;
        }

        // Clamp final
        ControlActual = Mathf.Clamp(ControlActual, ControlMin, ControlMax);
    }



    public void OverLoad()
    {

        if(EnergiaActual <= EnergiaMin)
            EnergiaActual = EnergiaMin;

        if(EnergiaActual >= EnergiaMax)
            EnergiaActual = EnergiaMax;
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
