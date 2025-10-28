using UnityEngine;

public class RayoLazer : MonoBehaviour
{
    public float rotationSpeed = 30f; // Velocidad de rotación
    

    private bool isRotating;          // Controla si el láser gira o no

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        RevisarInput();
        
        GirarHaciaMouse();
    }

    

   
    

    // Revisa si se mantiene presionado el click izquierdo
    void RevisarInput()
    {
        if (Input.GetMouseButton(0))
            isRotating = true;
        else
            isRotating = false;
    }

    // Gira lentamente el láser hacia el cursor
    void GirarHaciaMouse()
    {
        if (isRotating)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            // Calcular la dirección del mouse respecto al jugador
            Vector3 direccion = mousePos - transform.position;
            float anguloObjetivo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

            // Rotar de forma suave hacia el mouse
            float nuevoAngulo = Mathf.MoveTowardsAngle(transform.eulerAngles.z, anguloObjetivo, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, 0f, nuevoAngulo);
        }
            
    }
}
