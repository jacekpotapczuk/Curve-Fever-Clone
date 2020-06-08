using UnityEngine;

public class Player : MonoBehaviour
{
    private Head head;
    private Tail tail;
    private int thickness = 2;
    private int speed = 5;

    private const float thicknessRatioLineRenderer = 0.1f;
    private const float thicknessRatioHead = 0.8f;
    private const float speedRatio = 1.5f;

    private bool isImmortal = true;
    private bool isDrawingTail = false;
    private bool reverseControls = false;

    private bool autoDrawingBreaks = false;
    
    private const float minNoDrawingTime = 0.5f;
    private const float maxNoDrawingTime = 1.7f;
    private float noDrawingTimeLeft;
    private const float minDrawingTime = 1.7f;
    private const float maxDrawingTime = 2.7f;
    private float drawingTimeLeft;

    private string inputName;

    private void Awake()
    {
        head = GetComponentInChildren<Head>();
        tail = GetComponentInChildren<Tail>();
    }

    private void Start()
    {
        head.SetSpeed(speed * speedRatio);
        head.SetImmortality(isImmortal);

        if (isDrawingTail)
            tail.StartDrawing(head.transform.position);
         
        UpdateThickness();
    }

    private void Update()
    {
        float input = Input.GetAxisRaw(inputName);
        if (reverseControls)
            input = -input;
        head.UpdatePosition(input);
        tail.UpdateTail(head.transform.position);

        if (autoDrawingBreaks)
            HandleAutoDrawingBreaks();
    }

    private void HandleAutoDrawingBreaks()
    {
        if (isDrawingTail)
        {
            drawingTimeLeft -= Time.deltaTime;
            if (drawingTimeLeft < 0)
                StopDrawing();
        }
        else
        {
            noDrawingTimeLeft -= Time.deltaTime;
            if (noDrawingTimeLeft < 0)
                StartDrawing();
        }
    }

    public void SetUp(Vector3 startingPosition, Color headColor, Color tailColor, string inputName)
    {
        head.transform.position = startingPosition;
        head.ChangeColor(headColor);
        tail.ChangeColor(tailColor);
        this.inputName = inputName;
    }

    public void ThicknessUp()
    {
        if (thickness >= 6)
            return;
        thickness += 1;
        UpdateThickness();
    }

    public void ThicknessDown()
    {
        if (thickness <= 1)
            return;
        thickness -= 1;
        UpdateThickness();
    }

    public void SpeedUp()
    {
        if (speed >= 9)
            return;
        speed += 1;
        UpdateSpeed();
    }

    public void SpeedDown()
    {
        if (speed <= 2)
            return;
        speed -= 1;
        UpdateSpeed();
    }

    public void SetImmortality(bool isImmortal)
    {
        head.SetImmortality(isImmortal);
    }

    public void ReverseControls(bool reversed)
    {
        reverseControls = reversed;
    }

    private void UpdateSpeed()
    {
        head.SetSpeed(speed * speedRatio);
    }

    private void UpdateThickness()
    {
        tail.ChangeThickness(head.transform.position, thickness * thicknessRatioLineRenderer);
        head.ChangeThickness(thickness * thicknessRatioHead);
    }

    public void StartDrawing()
    {
        tail.StartDrawing(head.transform.position);
        isDrawingTail = true;

        drawingTimeLeft = Random.Range(minDrawingTime, maxDrawingTime);
    }

    public void StopDrawing()
    {
        tail.StopDrawing(head.transform.position);
        isDrawingTail = false;

        noDrawingTimeLeft = Random.Range(minNoDrawingTime, maxNoDrawingTime);
    }

    public void AutoDrawingBreaks(bool isActive)
    {
        if (isActive)
        {
            autoDrawingBreaks = true;
        }
        else
        {
            autoDrawingBreaks = false;
        }

    }
}
