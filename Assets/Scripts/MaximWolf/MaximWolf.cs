using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class MaximWolf : PlayerInputs, IDamagable
{
    public SkillsDataSO Skill1;
    public SkillsDataSO Skill2;
    public SkillsDataSO Ult;

  
    public float RangeLeftClick = 4f;

    private Coroutine buffCoroutine;

    public int RageActual;
    public int RageMax = 100;
    public int RageGained = 2;

   
   
    public Transform player;

    private float CurrentSkill1Cost;
    private float CurrentSkill2Cost;
    private float CurrentDefinitivaCost;

    private float attackCooldown = 0f;

    public float RangeAttack;
    public GameObject Ataque;

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

    public override void Passive()
    {
        RageActual += RageGained;
        RageActual = Mathf.Min(RageMax, RageActual + RageGained);
    }
    public override void Ability1()
    {
        
        if (Formdata.RecargaActualSkill1 >= Formdata.TiempoMaximoRecarga1)
        {

            if (GameManager.Instance.EnergiaActual >= CurrentSkill1Cost)
            {
                Formdata.RecargaActualSkill1 = 0;
                GameManager.Instance.UsarEnergia(CurrentSkill1Cost);
                OnAbility1Trigger?.Invoke();
                float direccion = Mathf.Sign(transform.localScale.x);
                Vector3 posicionFrente = transform.position + new Vector3(RangeLeftClick * direccion, 0, 0);

                Instantiate(Skill1.prefab, posicionFrente, Quaternion.identity);
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
                if (player != null)
                {
                    Formdata.RecargaActualSkill2 = 0;
                    GameManager.Instance.UsarEnergia(CurrentSkill2Cost);
                    OnAbility2Trigger?.Invoke(this);
                    Instantiate(Skill2.prefab, player.position, Quaternion.identity);
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
        if (Formdata.RecargaActualDefinitiva >= Formdata.TiempoMaximoDefinitiva)
        {
            
            if (GameManager.Instance.EnergiaActual >= CurrentDefinitivaCost)
            {
                Formdata.RecargaActualDefinitiva = 0;
                GameManager.Instance.UsarEnergia(CurrentDefinitivaCost);
                StartCoroutine(AplicarBuff(Ult));
                OnDefinitivaTrigger?.Invoke(this);
            }
            else Debug.Log("Energia Insuficiente");
        }
        else Debug.Log("Habilidad en Enfriamiento");
    }

    private IEnumerator AplicarBuff(SkillsDataSO skill)
    {
        Debug.Log("Aplicando BUFF");

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

    public override void AttackCooldown()
    {
        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;
    }
    public override void Atacar()
    {
        if (attackCooldown > 0f)
            return;

        float direccion = Mathf.Sign(transform.localScale.x);
        Vector3 posicionFrente = transform.position + new Vector3(RangeAttack * direccion, 0, 0);

        Instantiate(Ataque, posicionFrente, Quaternion.identity);



        attackCooldown = 1f / stats.currentSpeedAttack;
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
