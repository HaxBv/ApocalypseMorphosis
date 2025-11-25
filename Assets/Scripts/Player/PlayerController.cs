using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public static PlayerController Instance;
    public PlayerInputs InputManager;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        if (InputManager == null) InputManager = GetComponent<PlayerInputs>();
    }
    void Start()
    {
        

    }


    void Update()
    {
        
    }
    
}
