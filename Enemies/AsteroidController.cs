using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    //[SerializeField]
    float asteroidSpeed;
    Rigidbody2D rb;
    float chance;
    [SerializeField]
    ParticleSystem particle;
    [SerializeField] ParticleSystem asteriodExplosion;

    [SerializeField] GameObject[] collectibles;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        chance = Random.Range(1, 3) % 2;
        particle.gameObject.SetActive(false);
        asteroidSpeed = EnemyController.instance.enemySpeed;
    }

    private void FixedUpdate()
    {
        Movement();
        SetSpeed();
    }

    private void Update()
    {
        //Movement();
        Delete();
    }

    void Movement()
    {
        //rb.AddForce(new Vector2(0, -asteroidSpeed));
        rb.velocity = new Vector2(0, -asteroidSpeed);
        if (chance == 0)
        {
            transform.Rotate(new Vector3(0, 0, Random.Range(45, 90)) * Time.deltaTime);
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, Random.Range(-45, -90)) * Time.deltaTime);
        }
    }

    void SetSpeed()
    {
        if (EnemyController.instance.slowMotionOn)
        {
            asteroidSpeed = EnemyController.instance.slowEnemySpeed;
        }
        else
        {
            asteroidSpeed = EnemyController.instance.enemySpeed;
        }
    }

    void Delete()
    {
        if (PlayerStats.instance.gameWon || PlayerStats.instance.playerDied || GameManager.instance.newGameReset || GameManager.instance.shopMenuOpened)
        {
            Instantiate(particle, transform);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "KillBox")
        {
            Destroy(this.gameObject);
        }
        /*if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        } */
    }

    public void Die()
    {
        //Debug.Log("asteroid destroyed");
        Instantiate(particle, transform);
        DropItem();
        Destroy(this.gameObject);
    }

    void DropItem()
    {
        float dropChance = (Random.Range(0, 100)) / 10;
        Vector2 SpawnPosition = this.transform.localPosition;
        if (0f < dropChance && dropChance <= 3.5f)
        {
            //do nothing
        }
        else if (3.5f < dropChance && dropChance <= 7.0f)
        {
            Instantiate(collectibles[0], SpawnPosition, Quaternion.identity, transform.parent);
        }
        else if (7.0f < dropChance && dropChance <= 8.0f)
        {
            Instantiate(collectibles[1], SpawnPosition, Quaternion.identity, transform.parent);
        }
        else if (8.0f < dropChance && dropChance <= 10.0f)
        {
            Instantiate(collectibles[2], SpawnPosition, Quaternion.identity, transform.parent);
        }
    }
}
