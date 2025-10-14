using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    public InputSystem_Actions input;
    public GameObject circlePrefab;
    private Camera mainCamera;

    private void Awake()
    {
        input = new InputSystem_Actions();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Click.performed += OnClick;
    }

    private void OnDisable()
    {
        input.Player.Click.performed -= OnClick;
        input.Disable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
        worldPos.z = 0f;

        Instantiate(circlePrefab, worldPos, Quaternion.identity); //Quaternion es para que al generar el obejto se genere sin rotación alguna,
                                                                  //sin el Quaternion se generara dependiendo de la rotación del objeto

        Debug.Log("Círculo creado");
    }
}