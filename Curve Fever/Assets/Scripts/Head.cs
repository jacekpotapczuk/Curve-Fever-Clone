using UnityEngine;

public class Head : MonoBehaviour
{

    private float angle;

    [SerializeField]
    private float speedDefault = 20f;

    public float speedActual = 20f;

    [SerializeField]
    private float angleChangeSpeed = 0.05f;

    [SerializeField]
    private Player player;

    public bool isImmortal = true;

    private Vector3 lastPosition;

    private bool detectCollision = true;

    public float Radius
    {
        get
        {
            return transform.localScale.x * 0.09f;
        }
    }

    public Vector3 ActualPosition
    {
        get
        {
            return transform.localPosition;
        }
    }

    public Vector3 LastPosition
    {
        get
        {
            if (lastPosition == null)
            {
                Debug.Log("Jestem w if last pos"); // TODO: wywalic jak useless
                return ActualPosition;
            }
               
            return lastPosition;
        }
    }
    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        angle -= horizontalInput * angleChangeSpeed;

        float x = speedActual * Mathf.Cos(angle) * Time.deltaTime;
        float y = speedActual * Mathf.Sin(angle) * Time.deltaTime;

        transform.localPosition += new Vector3(x, y, 0f);
        lastPosition = ActualPosition;
    }

    public void ChangeThickness(float thickness)
    {
        transform.localScale = new Vector3(thickness, thickness, thickness);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Głowa kolizja");
        Debug.Break();
        
    }
}
