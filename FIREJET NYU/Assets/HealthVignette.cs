using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HealthVignette : MonoBehaviour
{
    public Volume volume;
    public PlayerController player;
    private Vignette vignette;
    public float targetHaze;


    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGet(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
        targetHaze = (player.CurrentHealth / player.MaxHealth);
        vignette.intensity.value = 0.5f - targetHaze;
    }
}
