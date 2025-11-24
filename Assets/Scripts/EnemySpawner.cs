using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;

    [Header("Área de spawn (rectángulo alrededor del jugador)")]
    public float spawnWidth = 12f;     // Horizontal
    public float spawnHeight = 10f;    // Vertical

    [Header("Lista de enemigos (SO)")]
    public EnemyDataSO[] enemyTypes;

    private float[] timers;

    private void Awake()
    {
        timers = new float[enemyTypes.Length];
    }

    private void Update()
    {
        for (int i = 0; i < enemyTypes.Length; i++)
        {
            timers[i] += Time.deltaTime;

            if (timers[i] >= enemyTypes[i].SpawnRate)
            {
                timers[i] = 0f;
                SpawnEnemy(enemyTypes[i]);
                Debug.Log("Aparecio un enemigo");
            }
        }
    }

    // -----------------------------------------------------------------
    //     SPAWN EN BORDES DEL RECTÁNGULO (fuera del área segura)
    // -----------------------------------------------------------------
    void SpawnEnemy(EnemyDataSO data)
    {
        if (data.prefab == null)
            return;

        Vector2 spawnPos = GetSpawnPosition();

        GameObject enemyObj = Instantiate(data.prefab, spawnPos, Quaternion.identity);

        // Aplicar stats al EnemyBehaviour
        EnemyBase behaviour = enemyObj.GetComponent<EnemyBase>();
        if (behaviour != null)
        {
            behaviour.Setup(data);  // Puedes pasar el player si lo deseas
        }
    }
    public void UpdatePlayerReference(Transform newPlayerTransform)
    {
        player = newPlayerTransform;
    }

    // Genera una posición en uno de los 4 lados del rectángulo
    Vector2 GetSpawnPosition()
    {
        float x, y;
        int side = Random.Range(0, 4); // 0=arriba, 1=abajo, 2=izquierda, 3=derecha

        switch (side)
        {
            case 0: // Arriba
                x = Random.Range(-spawnWidth, spawnWidth);
                y = spawnHeight;
                break;

            case 1: // Abajo
                x = Random.Range(-spawnWidth, spawnWidth);
                y = -spawnHeight;
                break;

            case 2: // Izquierda
                x = -spawnWidth;
                y = Random.Range(-spawnHeight, spawnHeight);
                break;

            default: // Derecha
                x = spawnWidth;
                y = Random.Range(-spawnHeight, spawnHeight);
                break;
        }

        return (Vector2)player.position + new Vector2(x, y);
    }
}