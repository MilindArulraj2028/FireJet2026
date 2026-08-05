using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoLevel : MonoBehaviour
{
    public float timerLength;
    public string sceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerLength -= Time.deltaTime;
        if (timerLength <= 0)
        {
            LoadLevel();
        }
    }
    void LoadLevel()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
