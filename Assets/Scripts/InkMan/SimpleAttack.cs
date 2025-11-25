using UnityEngine;

public class SimpleAttack : MonoBehaviour
{
    private float lifetime = 0.3f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }


    void Update()
    {

    }
}
