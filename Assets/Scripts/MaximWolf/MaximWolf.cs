using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class MaximWolf : PlayerInputs, IDamagable, IAtacar
{

    public GameObject cuadradoPrefab;
    public GameObject circuloPrefab;
    public float RangeLeftClick = 4f;
   

    public int RageActual;
    public int RageMax = 100;
    public int RageGained = 2;

   
   
    public Transform player;

    private float CurrentSkill1Cost;
    private float CurrentSkill2Cost;
    private float CurrentDefinitivaCost;

    void Start()
    {
        CurrentSkill1Cost = data.Skill1Cost;
        CurrentSkill2Cost = data.Skill2Cost;
        CurrentDefinitivaCost = data.DefinitivaCost;
    }

    
    void Update()
    {


        MovementMechanic();
        Recharge();

    }

    public override void Passive()
    {
        RageActual += RageGained;
        RageActual = Mathf.Min(RageMax, RageActual + RageGained);
    }
    public override void Ability1()
    {

        if (data.RecargaActualSkill1 >= data.TiempoMaximoRecarga1)
        {

            if (GameManager.Instance.EnergiaActual >= CurrentSkill1Cost)
            {
                data.RecargaActualSkill1 = 0;
                GameManager.Instance.UsarEnergia(CurrentSkill1Cost);
                float direccion = Mathf.Sign(transform.localScale.x);
                Vector3 posicionFrente = transform.position + new Vector3(RangeLeftClick * direccion, 0, 0);

                Instantiate(cuadradoPrefab, posicionFrente, Quaternion.identity);
            }
            else
                Debug.Log("Energia Insuficiente");
        }
        else
            Debug.Log("Habilidad en Enfriamiento");



    }
    
    public override void Ability2()
    {
        if (data.RecargaActualSkill2 >= data.TiempoMaximoRecarga2)
        {
            if (GameManager.Instance.EnergiaActual >= CurrentSkill2Cost)
            {
                if (player != null)
                {
                    data.RecargaActualSkill2 = 0;
                    GameManager.Instance.UsarEnergia(CurrentSkill2Cost);
                    
                    Instantiate(circuloPrefab, player.position, Quaternion.identity);
                    Debug.Log("Aullido Aterrador");
                }

                else
                {

                    Debug.LogWarning("No se asignó el jugador en el Inspector.");

                }
            }
            else
                Debug.Log("Energia Insuficiente");
        }
        else
            Debug.Log("Habilidad en Enfriamiento");


    }

    public override void Definitiva()
    {
        if (data.RecargaActualDefinitiva >= data.TiempoMaximoDefinitiva)
        {
            
            if (GameManager.Instance.EnergiaActual >= CurrentDefinitivaCost)
            {
                data.RecargaActualDefinitiva = 0;
                GameManager.Instance.UsarEnergia(CurrentDefinitivaCost);
                Debug.Log("Bestia Liberada");
            }
            else
                Debug.Log("Energia Insuficiente");
        }
        else
            Debug.Log("Habilidad en Enfriamiento");


    }

    public override void OutOfControl()
    {
        Debug.Log("Perdiste el control");
    }



    


   public void TakeDamage(int damage)
    {
       /* HpActual -= damage;

        if (HpActual > 0)
        {
            Passive();
        }*/

    }
    public void Atacar(GameObject target)
    {
        /*IDamagable receptor = target.GetComponent<IDamagable>();

        if (receptor != null)
        {
            receptor.TakeDamage(AtkActual);
            if (HpActual > 0)
            {
                Passive();
            }
        }*/


    }
    public override void Recharge()
    {
        if (data.RecargaActualSkill1 < data.TiempoMaximoRecarga1)
        {
            data.RecargaActualSkill1 += Time.deltaTime;
        }
        if (data.RecargaActualSkill2 < data.TiempoMaximoRecarga2)
        {
            data.RecargaActualSkill2 += Time.deltaTime;
        }
        if (data.RecargaActualDefinitiva < data.TiempoMaximoDefinitiva)
        {
            data.RecargaActualDefinitiva += Time.deltaTime;
        }
    }



}
