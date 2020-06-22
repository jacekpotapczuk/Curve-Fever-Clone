using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    private Player player;
    private Head head;
    private Tail tail;
    private PowerUpTimerControler powerUpTimerControler;
    private int thickness = 2;
    private int speed = 3;

    private const float thicknessRatioLineRenderer = 0.08f;
    private const float thicknessRatioHead = 0.64f;
    private const float speedRatio = 1.2f;

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

    public string nick;

    private bool isMoving = true;

    private bool isAlive = true;

    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
    }

    public void SetDead()
    {
        isAlive = false;
        PlayerManager.Instance.OnPlayerDead(player);
    }

    private void Awake()
    {
        head = GetComponentInChildren<Head>();
        tail = GetComponentInChildren<Tail>();
        powerUpTimerControler = GetComponentInChildren<PowerUpTimerControler>();
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
        if (!isAlive)
            return;
        float input = Input.GetAxisRaw(inputName);
        if (reverseControls)
            input = -input;
        if(isMoving)
            head.UpdatePosition(input, false);
        else
            head.UpdatePosition(input, true);

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

    public void SetUp(string nick, Color tailColor, Vector3 startingPosition, Color headColor, string inputName, float angle, Player player)
    {
        this.nick = nick;
        head.transform.position = startingPosition;
        head.ChangeColor(headColor);
        tail.ChangeColor(tailColor);
        this.inputName = inputName;
        head.SetAngle(angle);
        this.player = player;
    }

    public void AddPowerUpTimer(float duration)
    {
        powerUpTimerControler.AddTimer(duration);
    }

    public void SetMovement(bool isMoving)
    {
        this.isMoving = isMoving;
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
        if (speed >= 7)
            return;
        speed += 1;
        UpdateSpeed();
    }

    public void SpeedDown()
    {
        if (speed <= 1)
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
