using UnityEngine;

public class Slash : MonoBehaviour
{
    public float lifetime;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    
    void Update()
    {
        
    }
}
