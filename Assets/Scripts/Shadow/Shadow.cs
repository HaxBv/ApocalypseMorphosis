using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Shadow : PlayerInputs, IDamagable, IAtacar
{
    public GameObject DagasPrefab;
    public GameObject SlashPrefab;
    public GameObject RafagaPrefab;


    

    public float RangeDagas;
    public float SplitDagas;

    public float SpeedRafaga;
    public float StartToRafaga;

    //public Animator Controller;
    public Transform player;
    public SpriteRenderer Sprite;

    private float CurrentSkill1Cost;
    private float CurrentSkill2Cost;
    private float CurrentDefinitivaCost;


    

    void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();


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
        throw new System.NotImplementedException();

    }
    public override void Ability1()
    {
        if(data.RecargaActualSkill1 >= data.TiempoMaximoRecarga1)
        {
            
            if (GameManager.Instance.EnergiaActual >= CurrentSkill1Cost)
            {
                data.RecargaActualSkill1 = 0;
                GameManager.Instance.UsarEnergia(CurrentSkill1Cost);
                print("Energia Actual: " + GameManager.Instance.EnergiaActual);
                // Posición del mouse en mundo
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0f;

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



    public override void Ability2()
    {

        if (data.RecargaActualSkill2 >= data.TiempoMaximoRecarga2)
        {
            if (GameManager.Instance.EnergiaActual >= CurrentSkill2Cost)
            {
                data.RecargaActualSkill2 = 0;
                GameManager.Instance.UsarEnergia(CurrentSkill2Cost);
                print("Energia Actual: " + GameManager.Instance.EnergiaActual);
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;

                transform.position = mousePos;

                if (player != null)
                {
                    Debug.Log("Teletransportación");
                    Instantiate(SlashPrefab, player.position, Quaternion.identity);
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
                if (player != null)

                {
                    GameManager.Instance.UsarEnergia(CurrentDefinitivaCost);
                    print("Energia Actual: " + GameManager.Instance.EnergiaActual);
                    Debug.Log("1000 Sombras");

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
            Instantiate(RafagaPrefab, player.position, rotacion);
            yield return new WaitForSeconds(SpeedRafaga);
        }
    }



    public override void OutOfControl()
    {
        Debug.Log("Perdiste el control");
    }


    void CrearDaga(Vector3 posicion, Vector2 direccion)
    {
        GameObject daga = Instantiate(DagasPrefab, posicion, Quaternion.identity);
        Dagas proyectil = daga.GetComponent<Dagas>();

        if (proyectil != null)
        {
            proyectil.Lanzar(direccion);
        }
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

    public void Atacar(GameObject Target)
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


