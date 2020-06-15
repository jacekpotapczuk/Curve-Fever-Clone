using UnityEngine;
using UnityEngine.Playables;

public class Head : MonoBehaviour
{
    private float angleChangeSpeed = 0.5f;

    private float angle;
    private float speed;

    private bool isImmortal;
    private Player player;
    public float Radius
    {
        get
        {
            return transform.localScale.x * 0.09f;
        }
    }

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public void UpdatePosition(float input, bool onlyChangeAngle = false)
    {
        angle -= input * angleChangeSpeed * speed * Time.deltaTime;
        if (onlyChangeAngle)
            return;

        float x = speed * Mathf.Cos(angle) * Time.deltaTime;
        float y = speed * Mathf.Sin(angle) * Time.deltaTime;

        transform.localPosition += new Vector3(x, y, 0f);

    }

    public void SetAngle(float angle)
    {
        this.angle = angle;
    }

    public void ChangeColor(Color color)
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.color = color;
    }

    public void ChangeThickness(float thickness)
    {
        transform.localScale = new Vector3(thickness, thickness, thickness);
    }

    public void SetImmortality(bool isImmortal)
    {
        this.isImmortal = isImmortal;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "event")
            return;
        if (isImmortal)
            return;

        player.SetDead();
    }

}
