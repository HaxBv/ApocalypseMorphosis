using UnityEngine;
using System.Collections;

public class Shadow : Player, IDamagable, IAtacar, IAllAbilities
{
    public GameObject DagasPrefab;
    public GameObject RafagaPrefab;

    public float RangeDagas = 2f;
    public Animator animator;
    public Transform player;
    void Start()
    {
        
    }

    
    void Update()
    {
        MovementMechanic();
        Ability1();
        Ability2();
        Definitiva();
    }
    public void Passive()
    {
        throw new System.NotImplementedException();
    }
    public void Ability1()
    {
        if (Input.GetMouseButtonDown(1))
        {
            float direccion = Mathf.Sign(transform.localScale.x);
            Vector3 posicionFrente = transform.position + new Vector3(RangeDagas * direccion, 0, 0);

            // Dirección base (frente)
            Vector2 dirFrente = new Vector2(direccion, 0);
            Vector2 dirArriba = new Vector2(direccion, 0.5f).normalized;
            Vector2 dirAbajo = new Vector2(direccion, -0.5f).normalized;

            // Instanciar las 3 dagas
            CrearDaga(posicionFrente, dirFrente);
            CrearDaga(posicionFrente, dirArriba);
            CrearDaga(posicionFrente, dirAbajo);
        }

    }

    public void Ability2()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

            Vector3 mousePos = Input.mousePosition;


            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);


            worldPos.z = 0f;


            transform.position = worldPos;

            Debug.Log("Teletrasnfortasion");
        }

    }

    public void Definitiva()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (player != null)
            {
                Debug.Log("1000 Sombras");
                StartCoroutine(SpawnRafaga());
            }
            else
            {
                Debug.LogWarning("No se asignó el jugador en el Inspector.");
            }
        }

    }
    private IEnumerator SpawnRafaga()
    {
        // Rotaciones para formar el asterisco
        float[] rotaciones = { 0f, 45f, 90f, 135f };

        foreach (float rot in rotaciones)
        {
            Quaternion rotacion = Quaternion.Euler(0f, 0f, rot);
            Instantiate(RafagaPrefab, player.position, rotacion);
            yield return new WaitForSeconds(0.5f); // Tiempo de espera
        }
    }

    public void OutOfControl()
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



    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();

    }

    public void Atacar(GameObject target)
    {
        IDamagable receptor = target.GetComponent<IDamagable>();

        if (receptor != null)
        {
            receptor.TakeDamage(Atkactual);
            if (HPactual > 0)
            {
                Passive();
            }
        }


    }
    



}


