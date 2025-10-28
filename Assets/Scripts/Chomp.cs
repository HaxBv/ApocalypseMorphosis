using UnityEngine;

public class Chomp : MonoBehaviour
{
    public GameObject ChompPrefab;
    void Start()
    {
        Destroy(gameObject,0.7f);
    }

    void Update()
    {
        
    }
}
