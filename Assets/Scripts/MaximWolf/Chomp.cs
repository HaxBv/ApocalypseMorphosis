using UnityEngine;

public class Chomp : MonoBehaviour
{
    public GameObject ChompPrefab;

    public float LifeTime;
    void Start()
    {
        Destroy(gameObject,LifeTime);
    }

    void Update()
    {
        
    }
}
