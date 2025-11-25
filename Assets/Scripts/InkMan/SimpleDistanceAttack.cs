using UnityEngine;

public class SimpleDistanceAttack : MonoBehaviour
{
    public float speed;
    public float lifetime;

    private Vector2 direccion;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direccion * speed;
    }

    public void Lanzar(Vector2 dir)
    {
        direccion = dir.normalized;
    }
}
