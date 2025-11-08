using UnityEngine;

public class Aullido : MonoBehaviour
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
