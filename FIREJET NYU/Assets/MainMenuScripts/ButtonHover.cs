using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScale = 1.1f;   // Magnification
    public float speed = 10f;         // Animation speed

    private Vector3 normalScale;
    private Vector3 targetScale;


    void Start()
    {
        normalScale = transform.localScale;
        targetScale = normalScale;
    }


    void Update()
    {
        // Smoothly transition to the target size
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            speed * Time.deltaTime
        );
    }


    // Mouse enter
    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = normalScale * hoverScale;
    }


    // Mouse leaves
    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = normalScale;
    }

    // Mouse clicks
    public void ClickEffect()
    {
        targetScale = normalScale * 0.9f;
    }
}
