using UnityEngine;

public class Cinemachine : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdatePlayerReference(Transform newPlayerTransform)
    {
        player = newPlayerTransform;
    }
}
