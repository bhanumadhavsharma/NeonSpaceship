using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    float timer;
    bool reset;
    float shooterSpeed;
    float rotationSpeed;

    float startPoint;
    float stopPoint;
    float distance;
    bool isStopped;

    Rigidbody2D rb;
    [SerializeField] Transform shootPoint;
    [SerializeField] Transform playerCharacter;
    [SerializeField] GameObject enemyLazerPrefab;
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject[] collectibles;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        playerCharacter = FindObjectOfType<PlayerMovement>().gameObject.transform;

        timer = 0;
        reset = true;

        isStopped = false;
        startPoint = transform.position.y;
        distance = 4f;
    }

    private void FixedUpdate()
    {
        Movement();
        //Turn();
        SetSpeed();

        /*if (isStopped)
        { */
            if (!reset)
            {
                if (EnemyController.instance.slowMotionOn)
                {
                    timer = 4f;
                }
                else
                {
                    timer = 2f;
                }
                //timer = 2f;
                reset = true;
            }
            else
            {
                timer -= Time.deltaTime;//fixedDeltaTime;//deltaTime;
            }

            if (timer <= 0)
            {
                Fire();
                reset = false;
            }
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        Delete();
    }

    void Movement()
    {
        //rb.AddForce(new Vector2(0, -asteroidSpeed));
        rb.velocity = new Vector2(0, -shooterSpeed);
        stopPoint = transform.position.y;
        /*if (Mathf.Abs(stopPoint - startPoint) >= distance)
        {
            Stop();
            isStopped = true;
        } */
    }

    void Stop()
    {
        rb.velocity = Vector2.zero;
    }

    void Delete()
    {
        if (PlayerStats.instance.gameWon || PlayerStats.instance.playerDied || GameManager.instance.newGameReset || GameManager.instance.shopMenuOpened)
        {
            Destroy(this.gameObject);
            Instantiate(particle, transform);
        }
    }

    void Turn()
    {
        // transform.LookAt(playerCharacter);
        //playerCharacter.transform.position - transfor
        Vector3 targetVector = playerCharacter.position - transform.position;
        float rotatingIndex = Vector3.Cross(targetVector, transform.forward).z;
        rb.angularVelocity = 1 * rotatingIndex * rotationSpeed * Time.deltaTime;
    }

    void Fire()
    {
        GameObject newLazer = Instantiate(enemyLazerPrefab, shootPoint.transform.position, Quaternion.Euler(0,0,180), null);
        Rigidbody2D rigBody = newLazer.GetComponent<Rigidbody2D>();
        //newLazer.transform.rotation *= Quaternion.AngleAxis(180, transform.up);
        //rb.MovePosition(new Vector2(shootPoint.transform.position.x, Time.time * bulletSpeed));
        //rigBody.AddForce(new Vector2(0, 1.5f * 50));
        /*if (EnemyController.instance.slowMotionOn)
        {
            rigBody.velocity = (transform.up) * Time.deltaTime * (66f);
        }
        else
        {
            rigBody.velocity = (transform.up) * Time.deltaTime * (200f);
        } */
    }
    void SetSpeed()
    {
        if (EnemyController.instance.slowMotionOn)
        {
            shooterSpeed = EnemyController.instance.slowEnemySpeed;
            rotationSpeed = EnemyController.instance.slowRotatingSpeed;
        }
        else
        {
            shooterSpeed = EnemyController.instance.enemySpeed;
            rotationSpeed = EnemyController.instance.rotatingSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "KillBox")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.GetComponent<ShooterController>())
        {
            Die();
        }
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            Die();
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
