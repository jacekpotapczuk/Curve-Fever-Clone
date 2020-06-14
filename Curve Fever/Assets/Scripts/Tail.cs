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
        Debug.Log("Jestem w changeColor w tail");
        Debug.Log("Color " + color.ToString());
        this.color = color;
        if (actTailPart != null)
        {
            actTailPart.GetComponent<LineRenderer>().startColor = color;
            actTailPart.GetComponent<LineRenderer>().endColor = color;
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

        actTailPart.GetComponent<LineRenderer>().startColor = color;
        Debug.Log("Start color ustawiony na " + color.ToString());
        actTailPart.GetComponent<LineRenderer>().endColor = color;

        actTailPart.SetThickness(size);
        tailParts.Add(actTailPart);
    }
}
