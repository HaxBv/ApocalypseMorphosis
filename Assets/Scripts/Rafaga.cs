using UnityEngine;

public class Rafaga : MonoBehaviour
{
    public GameObject DagasPrefab;
    void Start()
    {
        Destroy(gameObject, 1.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
