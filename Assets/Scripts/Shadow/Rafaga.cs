using UnityEngine;

public class Rafaga : MonoBehaviour
{
    public GameObject DagasPrefab;
    public float lifetime;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
