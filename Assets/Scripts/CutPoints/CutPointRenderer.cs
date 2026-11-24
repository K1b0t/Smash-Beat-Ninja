using System.Collections.Generic;
using UnityEngine;

public class CutPointRenderer : MonoBehaviour
{
    public List<CutPoint> cutPoints;
    public LineRenderer lineRenderer;
    public CubeController cubeController;

    void Start()
    {
        for (int i = 0; i < cutPoints.Count; i++)
        {
            cutPoints[i].cutPointRenderer = this;
        }
    }

    void Update()
    {
        DrawCut();
    }

    private void DrawCut()
    {
        for (int i = 0; i < cutPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, cutPoints[i].transform.position);
        }
    }

    public void CheckIfAllPointsCut(CutPoint point)
    {
        if (cutPoints.Contains(point))
        {
            cutPoints.Remove(point);
            point.gameObject.SetActive(false);
        }

        lineRenderer.positionCount = cutPoints.Count;
        DrawCut();

        if (cutPoints.Count <= 0)
        {
            cubeController?.CortarCubo();
        }
    }
}
