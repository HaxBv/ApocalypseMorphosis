using UnityEngine;

public class InkManAttack : MonoBehaviour
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
