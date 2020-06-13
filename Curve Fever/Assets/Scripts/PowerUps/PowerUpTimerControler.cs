using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PowerUpTimerControler : MonoBehaviour
{
    private List<PowerUpTimer> powerUpTimers= new List<PowerUpTimer>();

    //private const float radiusFirst = 0.2f; //  distance between the first timer and center
    private const float radiusSpace = 0.15f; // distance between every next timer

    [SerializeField]
    private PowerUpTimer powerUpTimerPrefab;

    [SerializeField]
    private Head head;

    public void AddTimer(float duration)
    {
        PowerUpTimer timer = Instantiate(powerUpTimerPrefab, transform).GetComponent<PowerUpTimer>();
        timer.duration = duration;
        timer.radius = head.Radius + powerUpTimers.Count * radiusSpace + 0.05f;
        timer.controler = this;
        powerUpTimers.Add(timer);
        UpdatePositions();
    }

    public void Update()
    {
        UpdatePositions();
        UpdateRadius();
    }

    private void UpdatePositions()
    {
        foreach(PowerUpTimer timer in powerUpTimers)
        {
            timer.center = new Vector2(head.gameObject.transform.position.x, head.gameObject.transform.position.y);
        }
    }

    private void UpdateRadius()
    {
        foreach (PowerUpTimer timer in powerUpTimers)
        {
            timer.center = new Vector2(head.gameObject.transform.position.x, head.gameObject.transform.position.y);
        }

        for (int i = 0; i < powerUpTimers.Count; i++)
        {
            powerUpTimers[i].radius = head.Radius + i * radiusSpace + 0.05f;
        }
    }


    public void RemoveTimer(PowerUpTimer timer)
    {
        powerUpTimers.Remove(timer);
        Destroy(timer);
        UpdateRadius();
    }
}
