using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopStats : MonoBehaviour
{
    public static ShopStats instance;


    [Header("Stats")]
    public PlayerController player;
    [Space(10)]
    public float playerAgility;
    public float playerHealth;
    public float playerFuelEfficiency;
    public float playerMaxFuel;
    [Space(10)]
    public float LevelTime;
    public TMP_Text myText;
    public bool broadcasting;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        
    }
    // Start is called before the first frame update
    void Start()
    {
      broadcasting = true;
    }

    // Update is called once per frame
    void Update()
    {

        myText.text = LevelTime.ToString("F2");



    }
}


  