
using System.Collections.Generic;
using UnityEngine;




[RequireComponent(typeof(LineRenderer), typeof(EdgeCollider2D))]
public class TailPart : MonoBehaviour
{
    private bool isActive = true;

    private LineRenderer lineRenderer;
    private List<Vector2> pointsLineRenderer;

    private EdgeCollider2D edgeCollider;
    private List<Vector2> pointsEdgeCollider;
    private Queue<Vector2> pointsEdgeColliderTemp;
    private Vector2 lastPointEdgeCollider;


    private float minGapLineRenderer = 0.3f;
    private float minGapEdgeCollider = 0.5f;

    public Tail tail;

    private void Awake()
    {
        lineRenderer = transform.GetComponent<LineRenderer>();
        edgeCollider = transform.GetComponent<EdgeCollider2D>();

        lineRenderer.numCapVertices = 5;
        lineRenderer.numCornerVertices = 5;

        //edgeCollider.edgeRadius = 0.5f;  //TODO: zobaczyc co to

        pointsLineRenderer = new List<Vector2>();
        pointsEdgeCollider = new List<Vector2>();
        pointsEdgeColliderTemp = new Queue<Vector2>();
    }

    private void Update()
    {
        if (isActive)
        {
            UpdateLineRenderer();
            UpdateEdgeColliderTemp();
        }
        if (pointsEdgeColliderTemp.Count > 0)
            UpdateEdgeCollider();
    }

    private void UpdateLineRenderer()
    {
        Vector3 actPosition = tail.head.transform.position;

        if (pointsLineRenderer.Count < 2)
        {
            pointsLineRenderer.Add(actPosition);
            lineRenderer.positionCount = pointsLineRenderer.Count;
            lineRenderer.SetPosition(pointsLineRenderer.Count - 1, pointsLineRenderer[pointsLineRenderer.Count - 1]);
            return;
        }

        float distanceFromLastPoint = Vector3.Distance(pointsLineRenderer[pointsLineRenderer.Count - 1], actPosition);
        if (distanceFromLastPoint > minGapLineRenderer)
        {
            pointsLineRenderer.Add(actPosition);
            lineRenderer.positionCount = pointsLineRenderer.Count;
            lineRenderer.SetPosition(pointsLineRenderer.Count - 1, pointsLineRenderer[pointsLineRenderer.Count - 1]);
        }
    }

    private void UpdateEdgeColliderTemp()
    {
        Vector3 actPosition = tail.head.transform.position;

        // first add points to temp queue to prevent collisions with own tail
        if (pointsEdgeCollider.Count < 2)
        {
            pointsEdgeColliderTemp.Enqueue(actPosition);
            pointsEdgeCollider.Add(actPosition);
            return;
        }
        else
        {
            float distFromLastPoint = Vector3.Distance(actPosition, lastPointEdgeCollider);
            if (distFromLastPoint > minGapEdgeCollider)
            {
                pointsEdgeColliderTemp.Enqueue(actPosition);
                lastPointEdgeCollider = actPosition;
            }
        }
    }
    private void UpdateEdgeCollider()
    {
        Vector3 actPosition = tail.head.transform.position;

        while (pointsEdgeColliderTemp.Count > 0 && Vector3.Distance(pointsEdgeColliderTemp.Peek(), actPosition) > edgeCollider.edgeRadius + tail.head.Radius + 0.1f) 
        {
            //Debug.Log("Dodaje w " + gameObject.GetComponent<LineRenderer>().widthMultiplier + 
             //   " punkt " + actPosition + ", bo odl jest pomiędzy " + pointsEdgeColliderTemp.Peek() + ",a " + actPosition + " wynosi " 
              //  + Vector3.Distance(pointsEdgeColliderTemp.Peek(), actPosition) + " > " + edgeCollider.edgeRadius + (0.388f));

            pointsEdgeCollider.Add(pointsEdgeColliderTemp.Dequeue());
        }

        if (pointsEdgeCollider.Count > 2)
            edgeCollider.points = pointsEdgeCollider.ToArray();
    }

    public void End()
    {
        isActive = false;
        Vector3 actPosition = tail.head.transform.position;
        
        pointsLineRenderer.Add(actPosition);
        lineRenderer.positionCount = pointsLineRenderer.Count;
        lineRenderer.SetPosition(pointsLineRenderer.Count - 1, pointsLineRenderer[pointsLineRenderer.Count - 1]);

        pointsEdgeColliderTemp.Enqueue(actPosition);
    }

    public void SetThickness(float size)
    {
        //this.size = size;  //TODO: zrobic to lepiej
        lineRenderer.widthMultiplier = size;
        edgeCollider.edgeRadius = size / 2f;
    }

    //private void Restart()
    //{

    //    pointsLineRenderer = new List<Vector2>();
    //    pointsEdgeCollider = new List<Vector2>();
    //    pointsEdgeColliderTemp = new Queue<Vector2>();

    //    Vector2[] arr = new Vector2[2];
    //    arr[0] = new Vector2(-999f, -999f);
    //    arr[1] = new Vector2(-999f, -998f);
    //    edgeCollider.points = arr;

    //    lineRenderer.positionCount = 0;

    //}

}
