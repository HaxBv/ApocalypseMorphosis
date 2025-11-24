using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamagable
{
    [SerializeField] public EnemyDataSO data;
    [SerializeField] public Rigidbody2D rb;

    protected Transform player;  // Referencia al jugador
    protected int currentHP;
    protected float speed;
    protected int damage;

    public virtual void Setup(EnemyDataSO data)
    {
        this.data = data;
        this.speed = data.Speed;
        this.damage = data.Damage;
        currentHP = data.Health;

        // Inicializar Rigidbody si no está asignado
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        // Buscar al jugador por tag si no se asigna manualmente
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

            if (player == null)
                Debug.LogError("No se encontró un jugador con el Tag 'Player'");
        }
    }

    private void FixedUpdate()
    {
        // Actualiza la referencia al jugador si se ha destruido o cambiado
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

            if (player == null)
                return;  // Si aún no se encuentra, no hacer nada.
        }

        // Si el jugador está presente, mover al enemigo hacia él
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    // ---------------------------------------
    // MÉTODO DE DAÑO VIRTUAL
    // ---------------------------------------
    public virtual void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        Debug.Log($"{gameObject.name} recibió {dmg} daño. HP: {currentHP}");

        if (currentHP <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
