using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float startDelay;
    public float speed;
    public Vector3 pos;
    public PlayerController player;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        startDelay -=Time.deltaTime;
        if (startDelay <= 0 && player.broadcasting == true)
        {
            pos.x += speed * Time.deltaTime;
        }
        transform.position = pos;

    }
}
