using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HovererEnemyController : MonoBehaviour
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

        Turn();
        if (isStopped)
        {
            if (!reset)
            {
                if (EnemyController.instance.slowMotionOn)
                {
                    timer = 2.5f;
                }
                else
                {
                    timer = 2f;
                }
                timer = 2f;
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
        }

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
        if (Mathf.Abs(stopPoint - startPoint) >= distance)
        {
            Stop();
            isStopped = true;
        }
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

        /*Vector3 targetVector = playerCharacter.position - transform.position;
        float rotatingIndex = Vector3.Cross(targetVector, transform.forward).z;
        rb.angularVelocity = 1 * rotatingIndex * rotationSpeed * Time.deltaTime; */

        playerCharacter = FindObjectOfType<PlayerMovement>().gameObject.transform;

        Vector3 direction = playerCharacter.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
    }

    void Fire()
    {
        GameObject newLazer = Instantiate(enemyLazerPrefab, shootPoint);
        Rigidbody2D rigBody = newLazer.GetComponent<Rigidbody2D>();
        //rb.MovePosition(new Vector2(shootPoint.transform.position.x, Time.time * bulletSpeed));
        //rigBody.AddForce(new Vector2(0, 1.5f * 50));
        rigBody.velocity = (newLazer.transform.up) * (1.6f * 50) * Time.deltaTime;
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

/*public class Enemy : MonoBehaviour
{
    private enum State
    { Walking, Knockback, Frozen, Dead } // WALKING: normal chasing, KNOCKBACK: when take damage, FROZEN: one of the effects, DEAD: when they die
    private State
        currentState;

    public int
        playerDamage,
        damageDoneToPlayer,
        xpAdded;
    public float
        moveSpeed = 2f;
    private Rigidbody2D
        rb;
    public Transform
        player;
    public int
        health = 5;
    public GameObject
        frozenImage;

    private float
        scanRadius = 10f;
    public LayerMask
        whatIsPlayer;
    private Collider2D
        target,
        hitTarget;
    bool
        knowsAboutPlayer = false;
    bool
        playerTouched = false;
    bool
        isFrozen = false;

    private float
        knockbackStartTime,
        knockbackDuration = .1f,
        frozenStartTime,
        frozenDuration = 2f;
    private Vector2
        movementDirection,
        knockbackSpeed = new Vector2(50f, 50f);

    /// <summary>
    /// MAIN FUNCTIONS
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("player").transform;
    }

    private void FixedUpdate()
    {
        //MoveCharacter(movement);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Walking:
                UpdateWalkingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Frozen:
                UpdateFrozenState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
        //Turn();
    }

    /// <summary>
    /// WALKING STATE
    /// </summary>
    private void EnterWalkingState()
    {

    }

    private void UpdateWalkingState()
    {
        CheckEnvironment();
        Turn();
        MoveCharacter(movementDirection);
        DamagePlayer();
    }

    private void ExitWalkingState()
    {

    }

    /// <summary>
    /// KNOCKBACK
    /// </summary>
    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movementDirection.Set(-movementDirection.x * knockbackSpeed.x, -movementDirection.y * knockbackSpeed.y);
        rb.velocity = movementDirection;

        /*knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRB.velocity = movement;
        aliveAnim.SetBool("knockback", true); */
    /*}

    private void UpdateKnockbackState()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Walking);
        }
    }

    private void ExitKnockbackState()
    {
        /*aliveAnim.SetBool("knockback", false); */
    /*}

    /// <summary>
    /// FROZEN
    /// </summary>

    private void EnterFrozenState()
    {
        isFrozen = true;
        frozenStartTime = Time.time;
        rb.velocity = new Vector2(0f, 0f);
        Quaternion tempRotation = transform.rotation;
        transform.rotation = tempRotation;
        frozenImage.SetActive(true);
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void UpdateFrozenState()
    {
        if (Time.time >= frozenStartTime + frozenDuration)
        {
            frozenImage.SetActive(false);
            rb.constraints = RigidbodyConstraints2D.None;
            isFrozen = false;
            SwitchState(State.Walking);
        }
    }

    private void ExitFrozenState()
    {

    }

    /// <summary>
    /// DEAD
    /// </summary>
    private void EnterDeadState()
    {
        //spawn chunks and blood
        /*Instantiate(deathChunkParticle, alive.transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, alive.transform.position, deathBloodParticle.transform.rotation);
        XPManager.instance.AddXP(enemyXP);
        Destroy(gameObject); */
    /*    PlayerStats.instance.AddXP(xpAdded);
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    /// <summary>
    /// STATE FUNCTIONS
    /// </summary>
    /// <param name="state"></param>

    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Frozen:
                ExitFrozenState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Frozen:
                EnterFrozenState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    /// <summary>
    /// MOVEMENT FUNCTIONS
    /// </summary>
    /// 
    private void CheckEnvironment()
    {
        target = Physics2D.OverlapCircle(transform.position, scanRadius, whatIsPlayer);
        hitTarget = Physics2D.OverlapCircle(transform.position, 1f, whatIsPlayer);
    }

    private void Turn()
    {
        /*Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movementDirection = direction; */
     /*   if (target != null)
        {
            if (knowsAboutPlayer == false)
            {
                knowsAboutPlayer = true;
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();
            movementDirection = direction;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // -90f is used for when the sprite is rotated the wrong way
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //vector3.forward is z axis, so this will rotate angle amount around z-axis
        }
    }

    private void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.fixedDeltaTime));

    }

    /// <summary>
    /// DAMAGE FUNCTIONS
    /// </summary>

    public void Die()
    {
        SwitchState(State.Dead);
    }

    public void Damage(int amount, int caseNumber)
    {
        health -= amount;
        if (health > 0)
        {
            if (!isFrozen)
            {
                SwitchState(State.Knockback);
            }
            if (caseNumber == 2)
            {
                SwitchState(State.Frozen);
            }
        }
        else if (health <= 0)
        {
            SwitchState(State.Dead);
        }
    }

    public void DamagePlayer()
    {
        if (hitTarget != null && playerTouched == false)
        {
            playerTouched = true;
            PlayerStats.instance.OnHit(damageDoneToPlayer);

            Invoke("PlayerTouchedFalse", 1f);
        }
    }

    void PlayerTouchedFalse()
    {
        playerTouched = false;
    }

    /// <summary>
    /// ON HIT FUNCTION
    /// </summary>
    /// <param name="efffect"></param>

    public void OnHit(int efffect)
    {
        playerDamage = PlayerStats.instance.damage;
        switch (efffect)
        {
            case 1:
                knockbackSpeed.Set(10f, 5f);
                Damage(playerDamage + 1, 1);
                break;
            case 2:
                Damage(playerDamage, 2);
                break;
            default:
                break;
                //nothing
        }
    }


}
*/