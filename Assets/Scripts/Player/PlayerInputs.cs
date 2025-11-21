using System;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour, IAbilities, IRechargeAbility
{



    public FormsDataSO Formdata;

    public Rigidbody2D rb;
    public InputSystem_Actions input;

    
    protected PlayerStats stats;








    private int ControlMax;
    public int ControlActual;
    private int ControlMin;

    
    public Vector2 moveInput;


    public Action<PlayerInputs> OnAbility1Trigger;
    public Action<PlayerInputs> OnAbility2Trigger;
    public Action<PlayerInputs> OnDefinitivaTrigger;

    private void Awake()
    {
        input = new();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();


    }
    private void OnEnable()
    {
        input.Enable();

        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += OnMove;
        input.Player.Move.started += OnMove;

        input.Player.Skill1.performed += OnSkill1;

        input.Player.Skill2.performed += OnSkill2;
        input.Player.Ultimate.performed += OnUltimate;

    }
    private void OnSkill1(InputAction.CallbackContext context)
    {
        Ability1();

    }
    private void OnSkill2(InputAction.CallbackContext context)
    {
        Ability2();
    }

    private void OnUltimate(InputAction.CallbackContext context)
    {
        Definitiva();
    }




    private void OnDisable()
    {
        input.Enable();

        input.Player.Move.performed -= OnMove;
        input.Player.Move.canceled -= OnMove;
        input.Player.Move.started -= OnMove;

        input.Player.Skill1.performed -= OnSkill1;
        input.Player.Skill2.performed -= OnSkill2;
        input.Player.Ultimate.performed -= OnUltimate;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    void Start()
    {

    }


    void Update()
    {



    }
    public void MovementMechanic()
    {
        rb.linearVelocity = moveInput * stats.currentSpeedMovement;
    }

    /*public virtual void Pasive()
    {
        int regenerationHp = 2;
        while (HPactual < HPMax)
        {
            HPactual += regenerationHp;
            HPactual = Mathf.Min(HPMax, HPactual + regenerationHp);

        }
    }*/

    public virtual void Passive()
    {
        throw new NotImplementedException();
    }

    public virtual void Ability1()
    {
        OnAbility1Trigger?.Invoke(this);
    }

    public virtual void Ability2()
    {
        throw new NotImplementedException();
    }

    public virtual void Definitiva()
    {
        throw new NotImplementedException();
    }

    public virtual void OutOfControl()
    {
        throw new NotImplementedException();
    }

    public virtual void Recharge()
    {
        throw new NotImplementedException();
    }
}
