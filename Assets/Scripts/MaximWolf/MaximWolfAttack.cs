using UnityEngine;

public class MaximWolfAttack : MonoBehaviour
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
