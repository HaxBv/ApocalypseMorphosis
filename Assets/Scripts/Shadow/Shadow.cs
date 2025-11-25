using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Shadow : PlayerInputs, IDamagable
{
    

    public SkillsDataSO Skill1;
    public SkillsDataSO Skill2;
    public SkillsDataSO Ult;


    public float RangeDagas;
    public float SplitDagas;

    public float SpeedRafaga;
    public float StartToRafaga;

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
    public override void MovementMechanic()
    {
        rb.linearVelocity = moveInput * stats.currentSpeedMovement;
    }
    public override void Passive()
    {
        throw new System.NotImplementedException();

    }
    public override void Ability1()
    {
        if(Formdata.RecargaActualSkill1 >= Formdata.TiempoMaximoRecarga1)
        {
            
            if (GameManager.Instance.EnergiaActual >= CurrentSkill1Cost)
            {
                Formdata.RecargaActualSkill1 = 0;
                GameManager.Instance.UsarEnergia(CurrentSkill1Cost);
                OnAbility1Trigger?.Invoke(this);

                print("Energia Actual: " + GameManager.Instance.EnergiaActual);
                // Posición del mouse en mundo
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Camera.main.nearClipPlane + 1f;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                // Dirección hacia el mouse
                Vector2 dirBase = (mousePos - transform.position).normalized;

                // NUEVO: origen desplazado
                Vector3 origen = transform.position + (Vector3)dirBase * RangeDagas;

                // Ángulo de separación (usa SplitDagas como grados)
                Vector2 dirUp = Rotate(dirBase, SplitDagas);
                Vector2 dirDown = Rotate(dirBase, -SplitDagas);

                // Crear 3 dagas desde adelante del personaje
                CrearDaga(origen, dirBase);
                CrearDaga(origen, dirUp);
                CrearDaga(origen, dirDown);
            }
            else
                Debug.Log("Energia Insuficiente");
        }
        else
            Debug.Log("Habilidad en Enfriamiento");

    }




    // Función para rotar un vector 2D
    Vector2 Rotate(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(rad);
        float cos = Mathf.Cos(rad);

        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        );
    }
    void CrearDaga(Vector3 posicion, Vector2 direccion)
    {
        GameObject daga = Instantiate(Skill1.prefab, posicion, Quaternion.identity);
        Dagas proyectil = daga.GetComponent<Dagas>();

        if (proyectil != null)
        {
            proyectil.Lanzar(direccion);

            // Simplemente rotar usando Atan2
            daga.transform.up = direccion; // La cara “arriba” del sprite apunta al movimiento
        }
    }





    public override void Ability2()
    {

        if (Formdata.RecargaActualSkill2 >= Formdata.TiempoMaximoRecarga2)
        {
            if (GameManager.Instance.EnergiaActual >= CurrentSkill2Cost)
            {
                Formdata.RecargaActualSkill2 = 0;
                GameManager.Instance.UsarEnergia(CurrentSkill2Cost);
                OnAbility2Trigger?.Invoke(this);

                print("Energia Actual: " + GameManager.Instance.EnergiaActual);
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Camera.main.nearClipPlane + 1f;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                transform.position = mousePos;

                if (player != null)
                {
                    Debug.Log("Teletransportación");
                    Instantiate(Skill2.prefab, player.position, Quaternion.identity);
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
                if (player != null)

                {
                    GameManager.Instance.UsarEnergia(CurrentDefinitivaCost);
                    print("Energia Actual: " + GameManager.Instance.EnergiaActual);
                    Debug.Log("1000 Sombras");
                    OnDefinitivaTrigger?.Invoke(this);
                    StartCoroutine(SpawnRafaga());
                }

                else

                {
                    Debug.LogWarning("No se asignó el jugador en el Inspector.");
                }

            }
            else
                print("Energia Insuficiente");
        }
        else
            Debug.Log("Habilidad en Enfriamiento");


        /*Color color = Color.white;
        color.a = ;
        Sprite.color = color;*/



    }

    private IEnumerator SpawnRafaga()
    {
        yield return new WaitForSeconds(StartToRafaga);
        float[] rotaciones = { 0f, 35f, 90f, 145f };
        foreach (float rot in rotaciones)
        {

            Quaternion rotacion = Quaternion.Euler(0f, 0f, rot);
            Instantiate(Ult.prefab, player.position, rotacion);
            yield return new WaitForSeconds(SpeedRafaga);
        }
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


