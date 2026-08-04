using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(EdgeCollider2D))]
[RequireComponent(typeof(LineRenderer))]
public class EdgeColliderVisualizer : MonoBehaviour
{
    void OnValidate()
    {
        SyncLineRenderer();
    }

    void SyncLineRenderer()
    {
        EdgeCollider2D edge = GetComponent<EdgeCollider2D>();
        LineRenderer line = GetComponent<LineRenderer>();

        Vector2[] points = edge.points;
        line.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            line.SetPosition(i, points[i]);
        }

        line.startWidth = 0.2f; //Adjust wall parameters
                line.endWidth = 0.2f;
    }
}
