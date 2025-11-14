using System;
using UnityEngine;

public class Zero : PlayerInputs, IDamagable, IAtacar
{
    public GameObject circuloPrefab;
    public GameObject RayoLazerPrefab;
    public float RangeLazer = 2f;
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
    public override void Ability1()
    {
        if (data.RecargaActualSkill1 >= data.TiempoMaximoRecarga1)
        {
            if (GameManager.Instance.EnergiaActual >= CurrentSkill1Cost)
            {
                data.RecargaActualSkill1 = 0;
                GameManager.Instance.UsarEnergia(CurrentSkill1Cost);
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;



                Instantiate(circuloPrefab, mousePos, Quaternion.identity);

                Debug.Log("Objetivo Localizado: " + mousePos + " DisparandoMisiles");
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
                data.RecargaActualSkill2 = 0;
                GameManager.Instance.UsarEnergia(CurrentSkill2Cost);
                Debug.Log("Zero 2.0 ACTIVADO");
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
                

                if (player != null)
                {
                    data.RecargaActualDefinitiva = 0;
                    GameManager.Instance.UsarEnergia(CurrentDefinitivaCost);
                    // Obtener la posición del mouse en coordenadas del mundo
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0f;

                    // Calcular la dirección del mouse desde el jugador
                    float dirFrontal = Mathf.Sign(transform.localScale.x);
                    Vector3 posicionFrente = transform.position + new Vector3(RangeLazer * dirFrontal, 0, 0);
                    Vector3 direccion = mousePos - posicionFrente;

                    // Calcular el ángulo en grados
                    float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

                    // Crear el rectángulo rotado hacia el mouse
                    Quaternion rotacion = Quaternion.Euler(0f, 0f, angulo);
                    GameObject rayo = Instantiate(RayoLazerPrefab, posicionFrente, rotacion);

                    //Hacer que el rayo siga al jugador
                    rayo.transform.SetParent(player);

                    Debug.Log("KAME HAME HA");
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

    public void Atacar(GameObject Target)
    {
        throw new System.NotImplementedException();
    }

    public override void OutOfControl()
    {
        throw new System.NotImplementedException();
    }

    public override void Passive()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
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
