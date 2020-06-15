using System.Collections.Generic; 
using UnityEngine;


public class Tail : MonoBehaviour
{
    public Head head;

    [SerializeField]
    private GameObject tailPartPrefab;

    private List<TailPart> tailParts;
    private TailPart actTailPart;

    private float size = 0.2f;

    private Color color;

    private void Awake()
    {
        tailParts = new List<TailPart>();
    }

    public void UpdateTail(Vector3 actPosition)
    {
        if (actTailPart != null)
            actTailPart.UpdateTailPart(actPosition);
    }

    public void ChangeColor(Color color)
    {
        this.color = color;
        if (actTailPart != null)
        {
            LineRenderer lr = actTailPart.GetComponent<LineRenderer>();
            lr.startColor = color;
            lr.endColor = color;
        }
    }

    public void ChangeThickness(Vector3 actPositon, float size)
    {
        this.size = size;
        if (actTailPart != null)
            NewTailPart(actPositon);
    }

    public void StartDrawing(Vector3 actPositon)
    {
        NewTailPart(actPositon);
    }

    public void StopDrawing(Vector3 actPosition)
    {
        if (actTailPart != null)
            actTailPart.End(actPosition);
        actTailPart = null;
    }

    private void NewTailPart(Vector3 actPosition)
    {
        if (actTailPart != null)
            actTailPart.End(actPosition);
        actTailPart = Instantiate(tailPartPrefab, transform).GetComponent<TailPart>();
        actTailPart.tail = this;

        LineRenderer lr = actTailPart.GetComponent<LineRenderer>();
        lr.startColor = color;
        lr.endColor = color;

        actTailPart.SetThickness(size);
        tailParts.Add(actTailPart);
    }
}
