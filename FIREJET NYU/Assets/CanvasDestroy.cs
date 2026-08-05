using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDestroy : MonoBehaviour
{
    public PlayerController player;
    public Canvas can;
    // Start is called before the first frame update
    void Start()
    {
        can.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isDead == true)
        {
            can.enabled = false;
        }
    }
}
