using UnityEngine;

public class Dagas : MonoBehaviour
{
    
    public float speed = 10f;
    public float lifetime = 2f;

    private Vector2 direccion;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

   

    void Update()
    {
        Proyectil();
    }
    public void Lanzar(Vector2 dir)
    {
        direccion = dir;
    }
    public void Proyectil()
    {
        transform.Translate(direccion * speed * Time.deltaTime);

    }

}
