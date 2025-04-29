using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

    public GameObject zombiePrefab;
    public Transform[] spawnPoints; // Array of possible spawn points
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI waveWarningText;

    private int waveNumber = 0;
    private int zombiesPerWave = 5;
    private int maxZombies = 45;
    private List<GameObject> activeZombies = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (true)
        {
            if (activeZombies.Count == 0) // Only start a new wave when all zombies are gone
            {
                waveNumber++;
                waveText.text = "Wave: " + waveNumber;

                waveWarningText.text = "Wave Incoming!";
                yield return new WaitForSeconds(3f); // Display warning for 3 seconds
                waveWarningText.text = "";

                int zombiesToSpawn = Mathf.Min(zombiesPerWave + (waveNumber * 2), maxZombies);

                for (int i = 0; i < zombiesToSpawn; i++)
                {
                    if (activeZombies.Count < maxZombies)
                    {
                        SpawnZombie();
                    }
                }

                yield return new WaitForSeconds(10f); // 10-second cooldown before next wave
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void SpawnZombie()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject zombie = Instantiate(zombiePrefab, spawnPoint.position, Quaternion.identity);
        activeZombies.Add(zombie);
        zombie.GetComponent<EnemyAi>().OnDeath += () => activeZombies.Remove(zombie); // Remove from list when killed
    }

    void Die()
    { 

        GameOver gameOverManager = Object.FindFirstObjectByType<GameOver>();

        if (gameOverManager != null)
        {
            int currentWave = Object.FindFirstObjectByType<WaveSpawner>().waveNumber; // Assuming you have a WaveManager with currentWave
            gameOverManager.SetWavesSurvived(currentWave);
            gameOverManager.ShowGameOver();
        }
    }
}
    

