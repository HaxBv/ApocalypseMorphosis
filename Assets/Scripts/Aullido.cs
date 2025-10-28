using UnityEngine;

public class Aullido : MonoBehaviour
{
    public float lifetime = 1f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
