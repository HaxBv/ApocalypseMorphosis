using System;
using UnityEngine;

public class FormManager : MonoBehaviour
{
    public static FormManager Instance;

    [Header("Prefabs de Formas")]
    public GameObject[] formPrefabs;

    [Header("Morph Settings")]
    public float CurrentMorphCost = 10f;
    public float maxMorphCooldown = 2f;

    private float currentMorphCooldown;
    private GameObject currentPlayer;
    private int currentFormIndex = 0;

    // Evento opcional para UI
    public event Action<GameObject> OnPlayerChanged;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        currentMorphCooldown = maxMorphCooldown;
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // Recargar cooldown
        if (currentMorphCooldown < maxMorphCooldown)
            currentMorphCooldown += Time.deltaTime;
    }

    public void ChangeForm(int index)
    {
        // Validaciones
        if (index < 0 || index >= formPrefabs.Length)
        {
            Debug.LogError("Índice de forma inválido");
            return;
        }

        if (index == currentFormIndex)
        {
            Debug.Log("Ya estás en esta forma");
            return;
        }

        if (GameManager.Instance == null)
        {
            Debug.LogError("No hay GameManager en la escena");
            return;
        }

        if (GameManager.Instance.EnergiaActual < CurrentMorphCost)
        {
            Debug.Log("Energía insuficiente");
            return;
        }

        if (currentMorphCooldown < maxMorphCooldown)
        {
            Debug.Log("Habilidad en cooldown");
            return;
        }

        // Usar energía
        GameManager.Instance.UsarEnergia(CurrentMorphCost);

        // Guardar posición y rotación
        Vector3 pos = currentPlayer != null ? currentPlayer.transform.position : Vector3.zero;
        Quaternion rot = currentPlayer != null ? currentPlayer.transform.rotation : Quaternion.identity;

        // Destruir player anterior
        if (currentPlayer != null)
            Destroy(currentPlayer);

        // Instanciar nuevo prefab
        currentPlayer = Instantiate(formPrefabs[index], pos, rot);
        currentPlayer.tag = "Player";

        // Reset cooldown y actualizar índice
        currentMorphCooldown = 0f;
        currentFormIndex = index;

        // Notificar UI
        OnPlayerChanged?.Invoke(currentPlayer);

        Debug.Log("Transformado a forma: " + formPrefabs[index].name);
    }
}
