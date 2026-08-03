using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;


    public Rigidbody2D rb;
    
    [Header("Movement Settings")]
    public float TargetSpeedX;
    public float TargetSpeedY;
    public float lerpSpeedX;
    public float lerpSpeedY;
    public float acceleration; // 25
    public float speed; // 5
    public float constant; // trail boost
    public float boostAmount;

    [Header("Particles")]
    public ParticleSystem particles;
    public bool spewing;
    public bool izPlaying;
    // public float particleCount;
    [Header("Boosting")]
    public float maxFuel;
    public float fuel;
    public float fuelDepletionSpeed;

    public float fuelRefillSpeed;
    public bool refilling;



    [Header("Damage + Health")]
    public Slider boost;
    // Start is called before the first frame update
    void Start()
    {
        particles.Stop();
        instance = this;
        fuel = maxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        TargetSpeedX = 0f;
        TargetSpeedY = 0f;
        // particleCount = particles.particleCount;
        boost.value = (fuel / maxFuel) - 0.1f;               // ADD BOLEAN FLAG REFUELING/FUELING SO THEY DONT CLASH OR JITTER
        //Input
        spewing = false;
        if (Input.GetKey(KeyCode.Space))
        {

            fuelRefillSpeed += -3f;

            if (fuel > 0f)
            {
                spewing = true;
                constant = boostAmount;
                acceleration = 55f;


            }
            else
            {

                fuel -= fuelRefillSpeed;
            }

        }


        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("Up");
            TargetSpeedY = speed * constant;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            TargetSpeedY = -speed * constant;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            TargetSpeedX = -speed * constant;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            TargetSpeedX = speed * constant;
        }
        acceleration = 30f;
        constant = 1f;



        if (spewing == false && izPlaying)
        {
            particles.Stop();
            Debug.Log("No Particles rn");
            izPlaying = false;
        }
        if (spewing == true && !izPlaying)
        {
            Debug.Log("Particles should be spewing");
            particles.Play();
            izPlaying = true;
        }
        if (fuel <= 0f)
        {
            constant = 1f; ;
            acceleration = 30f;
        }

        fuel = Mathf.Clamp(fuel, 0f, maxFuel);
        acceleration = 30f;

        //  particles.Stop();
        Debug.Log("NotSpewing");

        //Process velocity
        lerpSpeedX = Mathf.MoveTowards(rb.velocity.x, TargetSpeedX, acceleration * Time.deltaTime);
        lerpSpeedY = Mathf.MoveTowards(rb.velocity.y, TargetSpeedY, acceleration * Time.deltaTime);
        rb.velocity = new Vector2(lerpSpeedX, lerpSpeedY);


        // Rotation Control
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 180f);
    }
}

