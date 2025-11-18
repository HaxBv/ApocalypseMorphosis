using UnityEngine;


public class EnemyBase : MonoBehaviour
{
    public EnemyDataSO data;
    public Rigidbody rb;
    
    [SerializeField] private Transform player;

    [SerializeField] private int currentHP;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float spawnRate;
    private SpriteRenderer sprite;

    public void Setup(EnemyDataSO data, Transform player)
    {
        this.data = data;
        this.player = player;

        currentHP = data.Health;
        speed = data.Speed;
        damage = data.Damage;
        spawnRate = data.SpawnRate;


        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = data.sprite;
    }


    void FixedUpdate()
    {
        // Dirección normalizada hacia el jugador
        Vector2 direction = (player.position - transform.position).normalized;

        // Aplicar velocidad lineal hacia el jugador
        rb.linearVelocity = direction * speed;
    }


    void Start()
    {
        
    }

}
