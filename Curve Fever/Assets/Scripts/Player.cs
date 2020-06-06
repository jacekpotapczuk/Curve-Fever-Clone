using UnityEngine;

public class Player : MonoBehaviour
{
    private Head head;
    private Tail tail;
    private int thickness = 4;
    private const float thicknessRatioLineRenderer = 0.1f;
    private const float thicknessRatioHead = 0.8f;

    private bool isImmortal = false;

    private void Awake()
    {
        head = GetComponentInChildren<Head>();
        tail = GetComponentInChildren<Tail>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeThickness();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            thickness -= 1;
            Debug.Log("Act thickness: " + thickness);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            thickness += 1;
            Debug.Log("Act thickness: " + thickness);
        }


    }

    public void ChangeThickness()
    {
        tail.ChangeThickness(thickness * thicknessRatioLineRenderer);
        head.ChangeThickness(thickness * thicknessRatioHead);
    }

    public void SetImmortality(bool isImmortal)
    {

    }
}
