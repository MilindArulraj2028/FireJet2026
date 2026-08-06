using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float startDelay;
    public float speed;
    public Vector3 pos;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pos;
        startDelay -=Time.deltaTime;
        if (startDelay <= 0)
        {
            pos.x += speed * Time.deltaTime;
        }
        transform.position = pos;
    }
}
