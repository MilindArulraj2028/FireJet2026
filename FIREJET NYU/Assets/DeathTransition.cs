using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTransition : MonoBehaviour
{

    public PlayerController player;
    public Animator transition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isDead == true)
        {
            transition.SetTrigger("Die");
        }
    }
}
