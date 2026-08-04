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

    [ContextMenu("同步显示")]
    void SyncLineRenderer()
    {
        PolygonCollider2D poly = GetComponent<PolygonCollider2D>();
        LineRenderer line = GetComponent<LineRenderer>();

        Vector2[] points = poly.points; // 默认取第一条路径(path 0)

        line.useWorldSpace = false; // 局部坐标,和Collider保持一致
        line.loop = true; // 闭合,首尾自动连接,不用手动加重复点

        line.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            line.SetPosition(i, points[i]);
        }

        line.startWidth = 0.15f;
        line.endWidth = 0.15f;
    }
}
