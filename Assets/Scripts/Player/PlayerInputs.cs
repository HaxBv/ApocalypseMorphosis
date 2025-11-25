using System;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour, IAbilities, IRechargeAbility, IAtacar
{



    public FormsDataSO Formdata;

    public Rigidbody2D rb;
    public InputSystem_Actions input;

    
    protected PlayerStats stats;








    private int ControlMax;
    public int ControlActual;
    private int ControlMin;

    
    public Vector2 moveInput;


    public Action OnAbility1Trigger;
    public Action<PlayerInputs> OnAbility2Trigger;
    public Action<PlayerInputs> OnDefinitivaTrigger;

    public Action<PlayerInputs> OnAttackPerformed;

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

        input.Player.Attack.performed += OnAttack;

        input.Player.Skill1.performed += OnSkill1;
        input.Player.Skill2.performed += OnSkill2;
        input.Player.Ultimate.performed += OnUltimate;


    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.IsSelectingForm) return;
        Atacar();
    }

    private void OnSkill1(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.IsSelectingForm) return;
        Ability1();
        

    }
    private void OnSkill2(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.IsSelectingForm) return;
        Ability2();
    }

    private void OnUltimate(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.IsSelectingForm) return;
        Definitiva();
    }




    private void OnDisable()
    {
        input.Disable();

        input.Player.Move.performed -= OnMove;
        input.Player.Move.canceled -= OnMove;
        input.Player.Move.started -= OnMove;

        input.Player.Attack.performed -= OnAttack;

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
        
    }

    public virtual void Ability2()
    {
        
    }

    public virtual void Definitiva()
    {
        
    }

    public virtual void OutOfControl()
    {
        throw new NotImplementedException();
    }

    public virtual void Recharge()
    {
        throw new NotImplementedException();
    }

    public virtual void Atacar()
    {
        OnAttackPerformed?.Invoke(this);
    }

    public virtual void AttackCooldown()
    {
        throw new NotImplementedException();
    }
}
