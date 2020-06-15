using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    public static PowerUpManager Instance;

    [SerializeField]
    private PowerUp[] powerUpPrefabs;

    private const float minX = -13.35f;
    private const float maxX =  1f;
    private const float minY = -4.85f;
    private const float maxY = 4.9f;

    private List<PowerUp> spawnedPowerUps;
    private bool autoSpawn = false;

    private float minNoPowerUpTime = 0.7f;
    private float maxNoPowerUpTime = 2f;
    private float timeLeftToSpawn = -1f;

    private void Awake()
    {
        Instance = this;
        spawnedPowerUps = new List<PowerUp>();
    }

    public void AutoSpawn(bool isActive)
    {
        autoSpawn = isActive;
    }

    private void Update()
    {
        if (autoSpawn)
        {
            timeLeftToSpawn -= Time.deltaTime;
            if(timeLeftToSpawn < 0)
            {
                SpawnRandomizedPowerUp();
                timeLeftToSpawn = Random.Range(minNoPowerUpTime, maxNoPowerUpTime);
            }
        }
    }

    public void SpawnRandomizedPowerUp()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        SpawnPowerUp(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], x, y);
    }

    public void ClearPowerUps()
    {
        foreach(PowerUp pu in spawnedPowerUps)
        {
            Debug.Log("Niszcze PU");
            Destroy(pu.gameObject);
        }
        spawnedPowerUps.Clear();
    }

    private void SpawnPowerUp(PowerUp powerUp, float x, float y)
    {
        PowerUp p = Instantiate(powerUp, transform);
        p.transform.position = new Vector3(x, y, 1f);
        spawnedPowerUps.Add(p); //TODO: odkomentowac
    }



}
