using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class MaximWolf : Player, IDamagable, IAtacar, IAllAbilities
{

    public GameObject cuadradoPrefab;
    public float RangeLeftClick = 2f;
    public float RangeQ = 10f;

    public int RageActual;
    public int RageMax = 100;
    public int RageGained = 2;

   // public Vector2 moveInput;
    public Animator animator;

    void Start()
    {
        
    }

    
    void Update()
    {
        MovementMechanic();
        Passive();
        Ability1();
        /*Ability2();
        Definitiva();
        OutOfControl();*/
        MouseDirection();
        Animation();
    }
    
    public void Passive()
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
            Destroy(cuadradoPrefab,1);

        }
    
    }
    
    public void Ability2()
    {
        Debug.Log("Aullido");
    }

    public void Definitiva()
    {
        Debug.Log("Bestia Liberada");
    }

    public void OutOfControl()
    {
        Debug.Log("Perdiste el control");
    }



    public void MouseDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Debug.Log("Posición del mouse en pantalla: " + mousePos);
    }


    public void TakeDamage(int damage)
    {
        HPactual -= damage;

        if (HPactual > 0)
        {
            Passive();
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
                Passive();
            }
        }


    }
    public void Animation()
    {
        float velocidadX = moveInput.x;
        float velocidadY = moveInput.y;
        float velocidadTotal = moveInput.magnitude;
        if (animator != null)
        {
            animator.SetFloat("Movement", velocidadTotal);
            print(moveInput);

        }


        if (velocidadX != 0)
            FlipSprite(Mathf.Sign(velocidadX));
    }

    private void FlipSprite(float direccionX)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direccionX;
        transform.localScale = scale;
    }

}
