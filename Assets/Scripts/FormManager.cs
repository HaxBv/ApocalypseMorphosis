using System;
using UnityEngine;
using System.Collections;
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
    public float MorphCost;
    public float maxMorphCooldown;

    [HideInInspector] public float currentMorphCooldown;
    [HideInInspector] public GameObject currentPlayer;
    private int currentFormIndex = 0;

    // Evento opcional para UI
    //public event Action<GameObject> OnPlayerChanged;

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
    public void AplicarBuffUlt(float cooldownReduccion, float costReduccion, float duracion)
    {
        StartCoroutine(BuffUltRoutine(cooldownReduccion, costReduccion, duracion));
    }

    private IEnumerator BuffUltRoutine(float cooldownReduccion, float costReduccion, float duracion)
    {
        float originalCooldown = maxMorphCooldown;
        float originalCost = MorphCost;

        maxMorphCooldown -= cooldownReduccion;
        MorphCost -= costReduccion;

        Debug.Log($"BUFF Ult activado: cooldown {maxMorphCooldown}, cost {MorphCost} por {duracion} segundos");

        yield return new WaitForSeconds(duracion);

        maxMorphCooldown = originalCooldown;
        MorphCost = originalCost;

        Debug.Log("BUFF Ult finalizado, cooldown y cost restaurados");
    }

    public void ChangeForm(int index)
    {

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

        

        

        GameManager.Instance.UsarEnergia(MorphCost);

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
