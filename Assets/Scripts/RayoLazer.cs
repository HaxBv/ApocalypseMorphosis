using UnityEngine;

public class RayoLazer : MonoBehaviour
{
    public float rotationSpeed = 30f; // Velocidad de rotaci�n
    

    private bool isRotating;          // Controla si el l�ser gira o no

    void Start()
    {

    }

    
    void Update()
    {
        
        RevisarInput();
        
        GirarHaciaMouse();
    }

    

   
    

    
    void RevisarInput()
    {
        if (Input.GetMouseButton(0))
            isRotating = true;
        else
            isRotating = false;
    }

    
    void GirarHaciaMouse()
    {
        if (isRotating)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            
            Vector3 direccion = mousePos - transform.position;
            float anguloObjetivo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

           
            float nuevoAngulo = Mathf.MoveTowardsAngle(transform.eulerAngles.z, anguloObjetivo, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, 0f, nuevoAngulo);
        }
            
    }
}
