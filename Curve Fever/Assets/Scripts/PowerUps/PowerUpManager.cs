
using System.Linq;
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


    private void Awake()
    {
        Instance = this;
    }


    public void SpawnRandomizedPowerUp()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        SpawnPowerUp(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], x, y);
    }

    private void SpawnPowerUp(PowerUp powerUp, float x, float y)
    {
        PowerUp p = Instantiate(powerUp, transform);
        p.transform.position = new Vector3(x, y, 1f);
    }

}
