using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public enum ActiveForm
{
    InkMan,
    Shadow,
    Zero,
    MaximWolf,
    Konquest,
    Draco,
    Trueno,
}
public class Player : Entity, IAbilities
{
    public InputSystem_Actions input;

    public int Level = 1;
    private int LevelMax;

    public int ExpActual;
    private int ExpMax;
    private int ExpToLevelUp;



    private int EnergyMax;
    private int EnergyMin;
    public int Energyactual;

    private int RegenerationEnergy;
    private int RegenerationHP;
    private int RegenerationControlHP;

    private int ControlMax;
    public int ControlActual;
    private int ControlMin;

    public float Cooldown;
    public Vector2 moveInput;


    public Action<Player> OnAbility1Trigger;
    public Action<Player> OnAbility2Trigger;
    public Action<Player> OnDefinitivaTrigger;

    private void Awake()
    {
        input = new();
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
        transform.position += (Vector3)moveInput * MoveSpeed * Time.deltaTime;
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
}
