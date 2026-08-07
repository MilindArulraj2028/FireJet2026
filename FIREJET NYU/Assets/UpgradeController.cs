using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpgradeDurability()
    {
       
        ShopStats.instance.playerHealth *= 1.3f;

    }
    public void MaxFuel()
    {
        
        ShopStats.instance.playerMaxFuel *= 1.3f;
    }
    public void FuelEfficiency()
    {
       
        ShopStats.instance.playerFuelEfficiency *= 1.3f;

    }
    public void Agility()
    {
        ShopStats.instance.playerAgility *= 1.3f;
  
    }
    public void LevelOne()
    {
        Debug.Log("ButtonCLicked");
        SceneManager.LoadScene("World");

    }
    public void LevelTwo()
    {
        SceneManager.LoadScene("World 2");
    }
    public void LevelThree()
    {
        SceneManager.LoadScene("World 3");
    }
    public void LevelFour()
    {
        SceneManager.LoadScene("World 2");
    }
}
