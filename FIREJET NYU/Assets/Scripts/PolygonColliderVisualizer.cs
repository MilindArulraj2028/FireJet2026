using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(LineRenderer))]
public class PolygonColliderVisualizer : MonoBehaviour
{
    void OnValidate()
    {
        SyncLineRenderer();
    }

    void SyncLineRenderer()
    {
        PolygonCollider2D poly = GetComponent<PolygonCollider2D>();
        LineRenderer line = GetComponent<LineRenderer>();

        Vector2[] points = poly.points; // Defaults to the first path(path 0)

        line.useWorldSpace = false; // Local coordinates; keep aligned with the collider
                line.loop = true; // Closes and automatically connects the start and end points; no need to manually add duplicate points

        line.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            line.SetPosition(i, points[i]);
        }

        line.startWidth = 0.15f;
        line.endWidth = 0.15f;
    }
}
