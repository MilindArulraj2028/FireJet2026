using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopStats : MonoBehaviour
{
    public static ShopStats instance;

    public PlayerController player;
    public float playerAgility;
    public float playerHealth;
    public float playerFuelEfficiency;
    public float playerMaxFuel;

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
      
    }

    // Update is called once per frame
    void Update()
    {
        

       
        
    }
}


  