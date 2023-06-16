using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : Singleton<LineDrawer>
{
    private List<LineRenderer> lineRenderers;
    private GameObject lineObj;

    private void Awake()
    {
        lineRenderers = new List<LineRenderer>();
    }

    public void DrawLine(Vector3 startPoint, Vector3 endPoint)
    {
        LineRenderer lineRenderer = CreateLineRenderer();
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }

    public void ClearLines()
    {
        foreach (LineRenderer lineRenderer in lineRenderers)
        {
            Destroy(lineRenderer.gameObject);
        }
        lineRenderers.Clear();
    }

    private LineRenderer CreateLineRenderer()
    {
        lineObj = new GameObject("Line");
        lineObj.transform.SetParent(transform);

        LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        lineRenderers.Add(lineRenderer);

        return lineRenderer;
    }
}
