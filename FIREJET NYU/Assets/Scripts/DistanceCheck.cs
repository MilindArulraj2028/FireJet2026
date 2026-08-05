using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCheck : MonoBehaviour
{
    public Collider2D playerCollider;
    public EdgeCollider2D edgeCollider;

    public float score = 0;

    void Update()
    {
        ColliderDistance2D distanceInfo =
            playerCollider.Distance(edgeCollider);

        float distance = distanceInfo.distance;

        Debug.Log("distance£º" + distance);
        score += 10 - distance;
    }
}
