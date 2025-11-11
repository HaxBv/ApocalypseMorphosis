using System;
using UnityEngine;

public class Zero : Player, IDamagable, IAtacar
{
    public GameObject circuloPrefab;
    public GameObject RayoLazerPrefab;
    public float RangeLazer = 2f;
    public Transform player;

    
    void Start()
    {
        
    }

    void Update()
    {
        MovementMechanic();


    }
    public override void Ability1()
    {



        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        

        Instantiate(circuloPrefab, mousePos, Quaternion.identity);

            Debug.Log("Objetivo Localizado: " + mousePos+" DisparandoMisiles");
        
    }

    public override void Ability2()
    {
        
        Debug.Log("Zero 2.0 ACTIVADO");

        
    }
    public override void Definitiva()
    {
        if (player != null)
            {
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
}
