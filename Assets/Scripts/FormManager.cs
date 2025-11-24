using System;
using UnityEngine;
[System.Serializable]
public class FormAbilitiesUI
{
    public GameObject[] Skill;
}
public class FormManager : MonoBehaviour
{
    public static FormManager Instance;

    [Header("UI de Habilidades por Forma")]
    public FormAbilitiesUI[] formsAbilitiesUI;

    [Header("Prefabs de Formas")]
    public GameObject[] formPrefabs;

    [Header("Morph Settings")]
    public float CurrentMorphCost;
    public float maxMorphCooldown;

    [HideInInspector]public float currentMorphCooldown;
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
        UpdateAbilityUI();
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

       

        Debug.Log("Transformado a forma: " + formPrefabs[index].name);

        
        UpdateAbilityUI();
    }
    private void UpdateAbilityUI()
    {
        for (int i = 0; i < formsAbilitiesUI.Length; i++)
        {
            bool isActiveForm = i == currentFormIndex;

            if (formsAbilitiesUI[i].Skill != null)
                SetActiveArray(formsAbilitiesUI[i].Skill, isActiveForm);

            
        }
    }

    private void SetActiveArray(GameObject[] objects, bool active)
    {
        foreach (var obj in objects)
        {
            if (obj != null)
                obj.SetActive(active);
        }
    }
}
