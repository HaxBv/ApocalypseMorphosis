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
        if (FormManager.Instance.currentMorphCooldown < FormManager.Instance.maxMorphCooldown)
        {
            Debug.Log("Habilidad en cooldown");
            return;
        }
        if (GameManager.Instance.EnergiaActual < FormManager.Instance.MorphCost)
        {
            Debug.Log("Energía insuficiente");
            return;
        }
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
        currentPlayer = GameObject.FindGameObjectWithTag("Player");

        if (currentPlayer != null)
        {
            currentInkman = currentPlayer.GetComponent<InkMan>();

            if (currentInkman != null)
            {
                
                ControlActual += controlRecoveryRate * Time.deltaTime;
            }
            else
            {
                
                ControlActual -= controlDrainRate * Time.deltaTime;
            }
        }
        else
        {
           
            currentInkman = null;
            ControlActual -= controlDrainRate * Time.deltaTime;
        }

        
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
