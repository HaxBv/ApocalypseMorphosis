using UnityEngine;

public class ShadowAttack : MonoBehaviour
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
