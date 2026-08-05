using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelNearFar : MonoBehaviour
{
    [Header("Move up and down + Zoom")]
    public float scaleNear = 1.3f;   // Size at the bottom-most (nearest) position
    public float scaleFar = 0.7f;    // Size at the highest (most distant) point
    public float moveDistanceY = 1.5f; // Vertical movement range
    public float speedY = 1f;        // Vertical movement speed

        [Header("Move left and right")]
    public float moveDistanceX = 5f; // Range of lateral movement
    public float speedX = 0.7f;      // Horizontal movement speed (differs from vertical speed; the trajectory is not overly regular)

    public ParticleSystem particles;
    private Vector3 originalPos;

    void Start()
    {
        
        originalPos = transform.position;
    }

    void Update()
    {
        particles.Play();
        
        // sinY: -1(UP/FAR) ~ 1(DOWN/NEAR)
        float sinY = Mathf.Sin(Time.time * speedY);

        // 0(UP/FAR) ~ 1(DOWN/NEAR), used directly to map scaling and position
        float t = (sinY + 1f) / 2f;

        // Position: At the highest point at t = 0(originalPos.y + moveDistanceY), at the very bottom when t = 1(originalPos.y - moveDistanceY)
        float yOffset = Mathf.Lerp(moveDistanceY, -moveDistanceY, t);

        // Scaling: Uses `scaleFar` when t=0 (top/farthest) and `scaleNear` when t=1 (bottom/nearest)
        // Since the same t-value is used for both position and scaling, it naturally ensures the object gets smaller as it moves up and larger as it moves down
        float currentScale = Mathf.Lerp(scaleFar, scaleNear, t);


        // Lateral movement: Use a separate sine wave with a different speed to create a natural swaying effect
        float xOffset = Mathf.Sin(Time.time * speedX) * moveDistanceX;

        transform.position = new Vector3(
            originalPos.x + xOffset,
            originalPos.y + yOffset,
            originalPos.z
        );

        transform.localScale = new Vector3(currentScale, currentScale, currentScale);
    }
}