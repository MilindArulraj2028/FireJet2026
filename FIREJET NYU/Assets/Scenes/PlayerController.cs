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
    
    [Header("Boosting")]
    public float maxFuel;
    public float fuel;
    public float fuelDepletionSpeed;

    public float fuelRefillSpeed;
    public bool refilling;



    [Header("Damage + Health")]
    public Slider boost;
    public float MaxSpeed;
    public float CurrentSpeed;
    public float speedConstant;
    public float MaxHealth;
    public float CurrentHealth;

    
    void Start()
    {

        particles.Stop();
        instance = this;
        fuel = maxFuel;
        boost.value = 1f;
        MaxSpeed = Mathf.Sqrt((speed * boostAmount)* (speed * boostAmount)+ (speed * boostAmount) * (speed * boostAmount));
    }

    
    void Update()
    {
        CurrentSpeed = Mathf.Sqrt((rb.velocity.x * rb.velocity.x) + (rb.velocity.y * rb.velocity.y));
        speedConstant = Mathf.Clamp(0f, 1f, speedConstant);
        speedConstant = CurrentSpeed / MaxSpeed;
        TargetSpeedX = 0f;
        TargetSpeedY = 0f;
        
        boost.value = (fuel / maxFuel) - 0.05f;              
        spewing = false;

        if (Input.GetKey(KeyCode.Space))
        {

            if (fuel > 0f)
            {
                spewing = true;
                constant = boostAmount;
                acceleration = 55f;
                
            }
        }
        else
        {

            //fuel bigger
            Debug.Log("Refilling");
        }

        //Input
        if (Input.GetKey(KeyCode.UpArrow))
        {
           
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
            
            izPlaying = false;
        }
        if (spewing == true && !izPlaying)
        {
            
            particles.Play();
            izPlaying = true;
        }
        if (fuel <= 0f)
        {
            constant = 1f; ;
            acceleration = 30f;
        }
        if (Input.GetKey(KeyCode.Space) && fuel > 0f)
        {
            refilling = true;
            
            refilling = false;
        }
        if (!Input.GetKey(KeyCode.Space))
        {
            refilling = true;
        }
        if (refilling == false)
        {   
            fuel -= fuelDepletionSpeed * Time.deltaTime;
        }
        if (refilling == true)
        {
            fuel += fuelRefillSpeed * Time.deltaTime;
        }

        fuel = Mathf.Clamp(fuel, 0f, maxFuel);                     //Show this
        acceleration = 30f;

  
        

        //Process velocity
        lerpSpeedX = Mathf.MoveTowards(rb.velocity.x, TargetSpeedX, acceleration * Time.deltaTime);
        lerpSpeedY = Mathf.MoveTowards(rb.velocity.y, TargetSpeedY, acceleration * Time.deltaTime);
        
        rb.velocity = new Vector2(lerpSpeedX, lerpSpeedY);


        // Rotation Control
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 180f);
    }
}

