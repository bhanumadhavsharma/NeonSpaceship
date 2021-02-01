using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    //[SerializeField]
    float collectibleSpeed = 3f; 
    public int itemType; //0 is coin, 1 is extra shield, 2 is +5 coin
    [SerializeField] ParticleSystem particle;

    Rigidbody2D rb;

    bool inMagnetRange = false;
    float timeStamp;
    [SerializeField] GameObject ship;

    [SerializeField] AudioSource audioSource1;
    [SerializeField] AudioClip coinSound;
    [SerializeField] AudioClip healthSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        ship = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //rb.AddForce(new Vector2(0, -collectibleSpeed));
    }

    private void FixedUpdate()
    {
        SetSpeed();
        if (!PlayerStats.instance.coinMagnetOn || itemType == 1)
        {
            Movement();
        }
        else if (PlayerStats.instance.coinMagnetOn && (itemType == 2 || itemType == 0) && inMagnetRange)
        {
            Vector2 direction = -(transform.position - ship.transform.position).normalized;
            rb.velocity = new Vector2(direction.x, direction.y) * collectibleSpeed * (Time.time / timeStamp);
        }
    }

    void Movement()
    {
        rb.velocity = new Vector2(0, -collectibleSpeed);
    }

    void SetSpeed()
    {
        if (EnemyController.instance.slowMotionOn)
        {
            collectibleSpeed = EnemyController.instance.slowEnemySpeed;
        }
        else
        {
            collectibleSpeed = EnemyController.instance.enemySpeed;
        }
    }

    public void RunItemProperty()
    {
        switch (itemType)
        {
            case 0:
                if (PlayerStats.instance.coinX4Unlocked)
                {
                    PlayerStats.instance.coinCount += 4;
                }
                else
                {
                    PlayerStats.instance.coinCount++;
                }
                audioSource1.PlayOneShot(coinSound);
                break;
            case 1:
                if (PlayerStats.instance.health < PlayerStats.instance.maxHealth)
                {
                    PlayerStats.instance.health++;
                }
                audioSource1.PlayOneShot(healthSound);
                break;
            case 2:
                if (PlayerStats.instance.coinX4Unlocked)
                {
                    PlayerStats.instance.coinCount += 20;
                }
                else
                {
                    PlayerStats.instance.coinCount += 5;
                }
                audioSource1.PlayOneShot(coinSound);
                break;
        }
    }

    public void Die()
    {
        Instantiate(particle, this.transform);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("KillBox"))
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.name.Equals("Exhaust") && PlayerStats.instance.coinMagnetOn)
        {
            if (PlayerStats.instance.coinMagnetOn)
            {
                timeStamp = Time.time;
                inMagnetRange = true;
            }
            else
            {
                inMagnetRange = false;
            }
        }
    }
}
