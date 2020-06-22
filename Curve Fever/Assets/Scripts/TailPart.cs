using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(EdgeCollider2D))]
public class TailPart : MonoBehaviour
{

    public Tail tail;

    private bool isActive = true;

    private LineRenderer lineRenderer;
    private List<Vector2> pointsLineRenderer;

    private EdgeCollider2D edgeCollider;
    private List<Vector2> pointsEdgeCollider;
    private Queue<Vector2> pointsEdgeColliderTemp;
    private Vector2 lastPointEdgeCollider;

    private float minGapLineRenderer = 0.15f;
    private float minGapEdgeCollider = 0.5f;

    private void Awake()
    {
        lineRenderer = transform.GetComponent<LineRenderer>();
        edgeCollider = transform.GetComponent<EdgeCollider2D>();

        lineRenderer.numCapVertices = 5;
        lineRenderer.numCornerVertices = 5;

        pointsLineRenderer = new List<Vector2>();
        pointsEdgeCollider = new List<Vector2>();
        pointsEdgeColliderTemp = new Queue<Vector2>();
    }

    public void UpdateTailPart(Vector3 actPosition)
    {
        if (isActive)
        {
            UpdateLineRenderer(actPosition);
            UpdateEdgeColliderTemp(actPosition);
        }
        if (pointsEdgeColliderTemp.Count > 0)
            UpdateEdgeCollider(actPosition);
    }

    public void End(Vector3 actPosition)
    {
        isActive = false;

        pointsLineRenderer.Add(actPosition);
        lineRenderer.positionCount = pointsLineRenderer.Count;
        lineRenderer.SetPosition(pointsLineRenderer.Count - 1, pointsLineRenderer[pointsLineRenderer.Count - 1]);

        pointsEdgeColliderTemp.Enqueue(actPosition);
    }

    public void SetThickness(float size)
    {
        lineRenderer.widthMultiplier = size;
        edgeCollider.edgeRadius = size / 2f;
    }

    private void UpdateLineRenderer(Vector3 actPosition)
    {
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

    private void UpdateEdgeColliderTemp(Vector3 actPosition)
    {
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
    private void UpdateEdgeCollider(Vector3 actPosition)
    {
        while (pointsEdgeColliderTemp.Count > 0 && Vector3.Distance(pointsEdgeColliderTemp.Peek(), actPosition) > edgeCollider.edgeRadius + tail.head.Radius + 0.1f) 
        {
            pointsEdgeCollider.Add(pointsEdgeColliderTemp.Dequeue());
        }

        if (pointsEdgeCollider.Count > 2)
            edgeCollider.points = pointsEdgeCollider.ToArray();
    }

}
