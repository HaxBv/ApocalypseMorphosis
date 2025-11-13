using System.Collections;
using UnityEngine;

public class Shadow : PlayerInputs, IDamagable, IAtacar
{
    public GameObject DagasPrefab;
    public GameObject SlashPrefab;
    public GameObject RafagaPrefab;


    public float RangeDagas;
    public float SplitDagas;

    public float SpeedRafaga;
    public float StartToRafaga;

    public Animator Controller;
    public Transform player;
    public SpriteRenderer Sprite;

    void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        MovementMechanic();


    }
    public override void Passive()
    {
        throw new System.NotImplementedException();
    }
    public override void Ability1()
    {
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

    public override void Definitiva()
    {

        /*Color color = Color.white;
        color.a = ;
        Sprite.color = color;*/
        if (player != null)

        {

            Debug.Log("1000 Sombras");
            Debug.Log(StartToRafaga);
            StartCoroutine(SpawnRafaga());
        }

        else

        {
            Debug.LogWarning("No se asignó el jugador en el Inspector.");
        }


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

    private void SetMoveAnimation(Vector2 vector)
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
    }

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();

    }

    public void Atacar(GameObject Target)
    {
        throw new System.NotImplementedException();
    }
}


