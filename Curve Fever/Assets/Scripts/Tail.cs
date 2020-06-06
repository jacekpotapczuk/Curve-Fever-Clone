using System.Collections.Generic; 
using UnityEngine;


public class Tail : MonoBehaviour
{
    public Head head;

    [SerializeField]
    private GameObject tailPartPrefab;

    private List<TailPart> tailParts;
    private TailPart actTailPart;


    private void Awake()
    {
        tailParts = new List<TailPart>();
        NewTailPart();
    }

    private void NewTailPart()
    {
        if (actTailPart != null)
            actTailPart.End();
        actTailPart = Instantiate(tailPartPrefab, transform).GetComponent<TailPart>();
        actTailPart.tail = this;
        tailParts.Add(actTailPart);
    }

    public void ChangeThickness(float size)
    {
        NewTailPart();
        actTailPart.SetThickness(size);
    }
}
