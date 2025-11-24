using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
public class InkMan : PlayerInputs, IDamagable
{
    public SkillsDataSO Skill1;
    public SkillsDataSO Skill2;
    public SkillsDataSO Ult;

    private Coroutine buffCoroutine;
    private float recargaEnergia = 10f;

    private float attackCooldown = 0;

    public float RangeAttack;

    private float CurrentSkill1Cost;
    private float CurrentSkill2Cost;
    private float CurrentDefinitivaCost;

    private float BuffMorphCD = 4.5f;
    public GameObject Ataque;

    void Start()
    {
        


        CurrentSkill1Cost = Formdata.Skill1Cost;
        CurrentSkill2Cost = Formdata.Skill2Cost;
        CurrentDefinitivaCost = Formdata.DefinitivaCost;





    }

    void Update()
    {
        Passive();
        MovementMechanic();
        AttackCooldown();
    }
    public override void Passive()
    {
        GameManager.Instance.EnergiaActual += recargaEnergia * Time.deltaTime;
    }
    
    public override void Ability1()
    {
        Debug.Log("Ability1");
    }


    public override void Ability2()
    {
        Debug.Log("Ability2");

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

            }
            else Debug.Log("Energia Insuficiente");
        }
        else Debug.Log("Habilidad en Enfriamiento");
    }

    private IEnumerator AplicarBuff(SkillsDataSO skill)
    {
        Debug.Log("Aplicando");

        // Si ya había un buff activo, se reinicia
        if (buffCoroutine != null)
            StopCoroutine(buffCoroutine);

        buffCoroutine = StartCoroutine(BuffRoutine(skill));
        yield break;
    }

    private IEnumerator BuffRoutine(SkillsDataSO skill)
    {
        // Sumar al acumulador
        
        FormManager.Instance.maxMorphCooldown -= BuffMorphCD;

        // Reaplicar stats actuales
        stats.AplicarMejorasPorNivel();

        Debug.Log($"BUFF aplicado por {skill.duration} segundos");

        yield return new WaitForSeconds(skill.duration);

        // Restar del acumulador
        
        FormManager.Instance.maxMorphCooldown += BuffMorphCD;

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
        Debug.Log("Ataco");


        attackCooldown = 1f / stats.currentSpeedAttack;
    }

    /*private void SetMoveAnimation(Vector2 vector)
    {
        print(vector);

        if (vector.x != 0)
            Controller.SetBool("isMoving", true);
        else
            Controller.SetBool("isMoving", false);

        if (vector.x < 0)
            Sprite.flipX = true;

        if (vector.x > 0)
            Sprite.flipX = false;
    }*/

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();

    }

   
}
