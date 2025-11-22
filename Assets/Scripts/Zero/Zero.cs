using System;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Zero : PlayerInputs, IDamagable
{


    public SkillsDataSO Skill1;
    public SkillsDataSO Skill2;
    public SkillsDataSO Ult;


    public BalasDePlasma Plasma;



    public float RangeDisparo;



    public float RangeLazer = 2f;
    public Transform player;

    private float CurrentSkill1Cost;
    private float CurrentSkill2Cost;
    private float CurrentDefinitivaCost;


    private Coroutine buffCoroutine;


    private float attackCooldown = 0f;

    void Start()
    {
        
        CurrentSkill1Cost = Formdata.Skill1Cost;
        CurrentSkill2Cost = Formdata.Skill2Cost;
        CurrentDefinitivaCost = Formdata.DefinitivaCost;
    }

    void Update()
    {
        MovementMechanic();
        Recharge();
        AttackCooldown();
    }
    public override void Ability1()
    {
        if (Formdata.RecargaActualSkill1 >= Formdata.TiempoMaximoRecarga1)
        {
            if (GameManager.Instance.EnergiaActual >= CurrentSkill1Cost)
            {
                Formdata.RecargaActualSkill1 = 0;
                GameManager.Instance.UsarEnergia(CurrentSkill1Cost);
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;



                Instantiate(Skill1.prefab, mousePos, Quaternion.identity);

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
        if (Formdata.RecargaActualSkill2 >= Formdata.TiempoMaximoRecarga2)
        {
            if (GameManager.Instance.EnergiaActual >= CurrentSkill2Cost)
            {
                Formdata.RecargaActualSkill2 = 0;
                GameManager.Instance.UsarEnergia(CurrentSkill2Cost);
                StartCoroutine(AplicarBuff(Skill2));
               
            }
            else Debug.Log("Energia Insuficiente");
        }
        else Debug.Log("Habilidad en Enfriamiento");
    }

    private IEnumerator AplicarBuff(SkillsDataSO skill)
    {
        Debug.Log("Aplicando BUFF desde Skill2");

        // Si ya había un buff activo, se reinicia
        if (buffCoroutine != null)
            StopCoroutine(buffCoroutine);

        buffCoroutine = StartCoroutine(BuffRoutine(skill));
        yield break;
    }

    private IEnumerator BuffRoutine(SkillsDataSO skill)
    {
        // Sumar al acumulador
        stats.buffDamage += (int)skill.bonusDamage;
        stats.buffArmor += (int)skill.bonusArmor;
        stats.buffSpeedMovement += skill.bonusSpeed;
        stats.buffSpeedAttack += skill.bonusAttackSpeed;

        // Reaplicar stats actuales
        stats.AplicarMejorasPorNivel();

        Debug.Log($"BUFF aplicado por {skill.duration} segundos");

        yield return new WaitForSeconds(skill.duration);

        // Restar del acumulador
        stats.buffDamage -= (int)skill.bonusDamage;
        stats.buffArmor -= (int)skill.bonusArmor;
        stats.buffSpeedMovement -= skill.bonusSpeed;
        stats.buffSpeedAttack -= skill.bonusAttackSpeed;

        // Reaplicar stats actuales
        stats.AplicarMejorasPorNivel();

        Debug.Log("BUFF terminado");
    }




    public override void Definitiva()
    {
        if (Formdata.RecargaActualDefinitiva >= Formdata.TiempoMaximoDefinitiva)

        {
            if (GameManager.Instance.EnergiaActual >= CurrentDefinitivaCost)
            {
                

                if (player != null)
                {
                    Formdata.RecargaActualDefinitiva = 0;
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
                    GameObject rayo = Instantiate(Ult.prefab, posicionFrente, rotacion);
                    


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
    public override void AttackCooldown()
    {
        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;
    }
    public override void Atacar()
    {
        if (attackCooldown > 0f)
            return;
        Debug.Log("¡Ataco!");
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); mousePos.z = 0f;
        Vector2 direccion = (mousePos - transform.position).normalized;
        Vector3 origen = transform.position + (Vector3)direccion * RangeDisparo;

        BalasDePlasma bala = Instantiate(Plasma, origen, Quaternion.identity);
        bala.Lanzar(direccion);

        attackCooldown = 1f / stats.currentSpeedAttack;
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
        if (Formdata.RecargaActualSkill1 < Formdata.TiempoMaximoRecarga1)
        {
            Formdata.RecargaActualSkill1 += Time.deltaTime;
        }
        if (Formdata.RecargaActualSkill2 < Formdata.TiempoMaximoRecarga2)
        {
            Formdata.RecargaActualSkill2 += Time.deltaTime;
        }
        if (Formdata.RecargaActualDefinitiva < Formdata.TiempoMaximoDefinitiva)
        {
            Formdata.RecargaActualDefinitiva += Time.deltaTime;
        }
    }
}
