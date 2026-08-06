using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Rigidbody2D rb;

    [Header("Movement Settings")]
    public float TargetSpeedX;
    public float TargetSpeedY;
    [Space(10)]
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
    public Slider boost;
  //  private float boostShake;
  //  private float impactShake;


    [Header("Damage + Health")]
    public Slider healthSlider;
    public float MaxSpeed;
    public float CurrentSpeed;
    public float speedConstant;
    [Space(10)]
    public float MaxHealth;
    public float CurrentHealth;
    [Space(6)]
    public float DamageConstant;
    public float potentialDamage;

    [Header("Upgrade Points")]
    public float UpgradePoints;
    public float distance;
    public float radius;
    [Header("Camera Stuff")]
    public CinemachineVirtualCamera vcam;
    public float intensity;

    private CinemachineBasicMultiChannelPerlin noise;

    [Header("Miscellaneus")]
    public bool isDead;
    public ParticleSystem deathParticle;
    public Animator transition;
    public SpriteRenderer renderer;
    private bool died;

    void Start()
    {
        renderer.enabled = true;
        transition.SetBool("Die", false);
        isDead = false;
        CurrentHealth = MaxHealth;
        particles.Stop();
        instance = this;
        fuel = maxFuel;
        boost.value = 1f;
        MaxSpeed = Mathf.Sqrt((speed * boostAmount) * (speed * boostAmount) + (speed * boostAmount) * (speed * boostAmount));
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }


    void Update()
    {
        
        healthSlider.value = CurrentHealth / MaxHealth;
        

        acceleration = 30f;
        CurrentSpeed = Mathf.Sqrt((rb.velocity.x * rb.velocity.x) + (rb.velocity.y * rb.velocity.y));
        speedConstant = CurrentSpeed / MaxSpeed;

        potentialDamage = speedConstant * DamageConstant;
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
                intensity = 1.5f;

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



        if (CurrentHealth < 0f && !isDead)
        {
            isDead = true;
            ShopStats.instance.playerAgility = acceleration;
            ShopStats.instance.playerHealth = MaxHealth;
            ShopStats.instance.playerFuelEfficiency = fuelDepletionSpeed;
            ShopStats.instance.playerMaxFuel = maxFuel;
            StartCoroutine(StartDeath("DeathTransition"));


        }


        //Process velocity
        lerpSpeedX = Mathf.MoveTowards(rb.velocity.x, TargetSpeedX, acceleration * Time.deltaTime);
        lerpSpeedY = Mathf.MoveTowards(rb.velocity.y, TargetSpeedY, acceleration * Time.deltaTime);
        if (isDead == false)
        {

            rb.velocity = new Vector2(lerpSpeedX, lerpSpeedY);

        }
        if (isDead == true)
        {
            Instantiate(deathParticle, transform.position + new Vector3(0f, 0f, -3f), Quaternion.identity);
            renderer.enabled = false;
        }

        // Rotation Control
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 180f);
        constant = 1f;

        // Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);

        // foreach (Collider2D hit in hits)
        // {
        //     if (hit.CompareTag("Wall"))
        //    {
        //     distance += Mathf.Sqrt((transform.position.x - hit.transform.position.x) * (transform.position.x - hit.transform.position.x) +
        //            (transform.position.y - hit.transform.position.y) * (transform.position.y - hit.transform.position.y));
        //    }
        // }
        noise.m_AmplitudeGain = intensity;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            TakeDamage(potentialDamage);
            StartCoroutine(ShakeOnce(10));
        }
        if (other.gameObject.CompareTag("Damage"))
        {
            TakeDamage(40f);
            StartCoroutine(ShakeOnce(10));
        }
        if (other.gameObject.CompareTag("Lava"))
        {
            CurrentHealth = 0f;
            StartCoroutine(ShakeOnce(10f));
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            StartCoroutine(StartDeath("CoinFlip"));

        }

    }
    void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }


    IEnumerator StartDeath(string bro)
    {
        transition.SetBool("Die", true);
        yield return new WaitForSeconds(1.8f);
        print("g=o");
        SceneManager.LoadScene(bro);

    }
    public void Shake(float intensity)
    {
        noise.m_AmplitudeGain = intensity;
    }

    public void StopShake()
    {
        noise.m_AmplitudeGain = 0f;
    }
    IEnumerator ShakeOnce(float strength)
    {
        intensity = 10f;
        Debug.Log("Shaking");
        yield return new WaitForSeconds(0.2f);
        intensity = 0f;
        Debug.Log("Not Shaking");
    }
}

