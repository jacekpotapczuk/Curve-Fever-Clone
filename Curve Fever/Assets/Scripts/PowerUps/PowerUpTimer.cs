using System;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class PowerUpTimer : MonoBehaviour
{

    private LineRenderer lineRenderer;

    private const int numberOfSteps = 50;

    public float duration = 10f;
    private float durationLeft;

    public float radius = 1f;
    public Vector2 center = new Vector2(0f, 0f);

    public PowerUpTimerControler controler;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        durationLeft = duration;
    }

    private void Update() 
    {
        durationLeft -= Time.deltaTime;

        if (durationLeft <= 0)
            controler.RemoveTimer(this);

        float cutOffPercentage = durationLeft / duration;
        int currentNumberOfSteps = (int)((cutOffPercentage) * numberOfSteps);

        lineRenderer.positionCount = currentNumberOfSteps;

        float x, y, t, step;
        step = (2f * Mathf.PI) / (float)numberOfSteps;
        for (int i = 0; i < currentNumberOfSteps; i++)
        {

            t = i * step;
            x = center.x + radius * Mathf.Cos(t);
            y = center.y + radius * Mathf.Sin(t);

            lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
        }
    }
}
