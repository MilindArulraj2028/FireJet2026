using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopUI : MonoBehaviour
{
    
    [Header("Sliders")]
    public Slider sliderAgility;
    public Slider sliderHealth;
    public Slider sliderFuelEfficiency;
    public Slider sliderMaxFuel;

    // Start is called before the first frame update
    void Start()
    {
        sliderAgility.value = ShopStats.instance.playerAgility;
        sliderHealth.value = ShopStats.instance.playerHealth;
        sliderFuelEfficiency.value = 1/ShopStats.instance.playerFuelEfficiency;
        sliderMaxFuel.value = ShopStats.instance.playerMaxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
