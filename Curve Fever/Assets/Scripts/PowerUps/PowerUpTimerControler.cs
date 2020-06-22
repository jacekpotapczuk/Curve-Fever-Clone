using System.Collections.Generic;
using UnityEngine;

public class PowerUpTimerControler : MonoBehaviour
{
    [SerializeField]
    private PowerUpTimer powerUpTimerPrefab;

    [SerializeField]
    private Head head;

    private List<PowerUpTimer> powerUpTimers= new List<PowerUpTimer>();
    private const float radiusSpace = 0.15f; // distance between every next timer

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

    public void RemoveTimer(PowerUpTimer timer)
    {
        powerUpTimers.Remove(timer);
        Destroy(timer);
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
}
