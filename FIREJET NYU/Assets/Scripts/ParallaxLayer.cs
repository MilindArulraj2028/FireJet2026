using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float speed = 1f;
    private Material mat;

    void Start()
    {
        
        mat = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        float offset = Time.time * speed;
        mat.mainTextureOffset = new Vector2(offset, 0);
    }
}