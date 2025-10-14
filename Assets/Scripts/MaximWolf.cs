using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class MaximWolf : Player, IDamagable, IAtacar
{

    public GameObject cuadradoPrefab;
    public float RangeLeftClick = 2f;
    public float RangeQ = 10f;

    public int RageActual;
    public int RageMax = 100;
    public int RageGained = 2;


    /*private void Awake()
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
    }*/
    void Start()
    {
        
    }

    
    void Update()
    {
        MovementMechanic();
        Pasive();
        Ability1();
        Ability2();
        Definitiva();
        OutOfControl();
    }
    /*public void MovementMechanic()

    {
        transform.position += (Vector3)moveInput * MoveSpeed * Time.deltaTime;
    }
    */
    public void Pasive()
    {
        RageActual += RageGained;
        RageActual = Mathf.Min(RageMax, RageActual + RageGained);

    }
    public void Ability1()
    {
        if (Input.GetMouseButtonDown(1))
        {
           
            float direccion = Mathf.Sign(transform.localScale.x);
            Vector3 posicionFrente = transform.position + new Vector3(RangeLeftClick * direccion, 0, 0);

            Instantiate(cuadradoPrefab, posicionFrente, Quaternion.identity);


        }
    
    }
    public void Ability2()
    {

    }
    public void Definitiva()
    {

    }
    public void OutOfControl()
    {

    }
    public void TakeDamage(int damage)
    {
        HPactual -= damage;

        if (HPactual > 0) 
        {
            Pasive();
        }

    }
    public void Atacar(GameObject target)
    {
        IDamagable receptor = target.GetComponent<IDamagable>();

        if (receptor != null)
        {
            receptor.TakeDamage(Atkactual);
            if (HPactual > 0)
            {
                Pasive();
            }
        }

       
    }
}
