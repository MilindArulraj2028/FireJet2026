using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTransition : MonoBehaviour
{
    

    public Animator transition;
    
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
     
        transition.SetBool("Start", true);

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
       
        if (timer <= 0f)
        {
            transition.SetBool("Start", false);
        }
        

    }
    
}
