using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    // Waves no. Text
    public TextMeshProUGUI Waves;
    [SerializeField] private TextMeshProUGUI waveCounter;


    public int currentWave = 0;
    public int enemiesToSpawn = 0;
    public int enemiesLeft = 0;
    public bool startWave = false;
    public bool stopSpawn = false;

    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        currentWave = 0;
        enemiesToSpawn = 0;
        waveCounter.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndWave()
    {
        
        currentWave += 1;
        enemiesToSpawn += 20;
        new WaitForSeconds(3.0f);
        StartWave();
    }

    private void StartWave()
    {
        
        new WaitForSeconds(3.0f);
        stopSpawn = false;
        enemiesLeft = enemiesToSpawn;
        StartSpawning();
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        startWave = false;
        enemiesLeft = enemiesToSpawn;
    }

    IEnumerator SpawnEnemyRoutine()
    {
        int enemiesSpawned = 0;

        yield return new WaitForSeconds(3.0f);

        while (stopSpawn == false)
        {
            if (enemiesSpawned != enemiesToSpawn)
            {
                GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                enemiesSpawned++;
            }
            else if (enemiesSpawned == enemiesToSpawn)
            {
                stopSpawn = true;
                startWave = true;
                enemiesSpawned = 0;
            }

            yield return new WaitForSeconds(1.0f);

        }
    }

    private void UpdateWaves(int currentWave)
    {
        waveCounter.gameObject.SetActive(true);
        waveCounter.text = "Wave: " + currentWave.ToString();
        StartCoroutine(WaveDown());


    }

    private IEnumerator WaveDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(6.0f);
            waveCounter.gameObject.SetActive(false);

        }
    }
}
