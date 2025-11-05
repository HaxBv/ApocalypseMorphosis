using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class MaximWolf : Player, IDamagable, IAtacar
{

    public GameObject cuadradoPrefab;
    public GameObject circuloPrefab;
    public float RangeLeftClick = 4f;
   

    public int RageActual;
    public int RageMax = 100;
    public int RageGained = 2;

   
   
    public Transform player;

    void Start()
    {
        
    }

    
    void Update()
    {


        MovementMechanic();


    }

    public override void Passive()
    {
        RageActual += RageGained;
        RageActual = Mathf.Min(RageMax, RageActual + RageGained);
    }
    public override void Ability1()
    {
       
           
            float direccion = Mathf.Sign(transform.localScale.x);
            Vector3 posicionFrente = transform.position + new Vector3(RangeLeftClick * direccion, 0, 0);

            Instantiate(cuadradoPrefab, posicionFrente, Quaternion.identity);
            

    
    }
    
    public override void Ability2()
    {
  
        if (player != null)
        {
            Instantiate(circuloPrefab, player.position, Quaternion.identity);
            Debug.Log("Aullido Aterrador");
        }
            
        else
        {
               
            Debug.LogWarning("No se asignó el jugador en el Inspector.");
            
        }
        

    }

    public override void Definitiva()
    {
        
            Debug.Log("Bestia Liberada");

        
        
    }

    public override void OutOfControl()
    {
        Debug.Log("Perdiste el control");
    }



    


    public void TakeDamage(int damage)
    {
        HpActual -= damage;

        if (HpActual > 0)
        {
            Passive();
        }

    }
    public void Atacar(GameObject target)
    {
        IDamagable receptor = target.GetComponent<IDamagable>();

        if (receptor != null)
        {
            receptor.TakeDamage(AtkActual);
            if (HpActual > 0)
            {
                Passive();
            }
        }


    }
    

    
}
