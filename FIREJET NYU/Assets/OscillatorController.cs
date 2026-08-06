using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillatorController : MonoBehaviour
{
    public float amplitude = 1f;   // How far it moves
    public float frequency = 2f;   // How fast it moves

    public GameObject target;



    void Start()
    {
       
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = target.transform.position.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = pos;
    }
}