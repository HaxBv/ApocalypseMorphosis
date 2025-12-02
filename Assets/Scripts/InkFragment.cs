using UnityEngine;
using UnityEngine.InputSystem.XR;

public class InkFragment : MonoBehaviour
{
    public float xp;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            PlayerLevelSystem.Instance.GanarXP(xp);
            Destroy(gameObject);
            Debug.Log("Ganaste " + xp + " de XP");
            SoundManager.Instance.Play("InkManFragment");
        }
    }
}
