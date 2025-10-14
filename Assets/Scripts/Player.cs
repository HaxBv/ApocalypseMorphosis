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
public class Player : Entity
{
    public InputSystem_Actions input;

    public int Level = 1;
    public int LevelMax = 8;

    public int ExpActual;
    public int ExpMax;
    public int ExpToLevelUp;



    public int EnergyMax;
    public int EnergyMin = 0;
    public int Energyactual;

    public int RegenerationEnergy;
    public int RegenerationHP;
    public int RegenerationControlHP;

    public int ControlMax;
    public int ControlActual;
    public int ControlMin;

    public float Cooldown;
    public Vector2 moveInput;
   

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
    }
    private void OnDisable()
    {
        input.Enable();

        input.Player.Move.performed -= OnMove;
        input.Player.Move.canceled -= OnMove;
        input.Player.Move.started -= OnMove;
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
       MovementMechanic();
       
        
    }
    public void MovementMechanic()

    {
        transform.position += (Vector3)moveInput * MoveSpeed * Time.deltaTime;
    }
    public void Pasive()
    {
        int regenerationHp = 2;
        while (HPactual < HPMax) 
        {
            HPactual += regenerationHp;
            HPactual = Mathf.Min(HPMax, HPactual + regenerationHp);

        }
    }
    
}
