using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAnimation : MonoBehaviour
{
    public Animator anim;
    public float timer = 12f;
    // Start is called before the first
    // frame update
    void Start()
    {
        anim.SetBool("Playing", true);
        

    }

    // Update is called once per frame
    void Update()
    {
        timer = -Time.deltaTime;
        if (timer <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
