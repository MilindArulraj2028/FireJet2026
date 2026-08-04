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

    [ContextMenu("同步显示")]
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

        line.startWidth = 0.2f; // 墙的粗细,自己调
        line.endWidth = 0.2f;
    }
}
