using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /*float timeCounter = 0f;
    bool direction;

    float halfScreenWidth;
    float halfScreenHeight;
    Quaternion centerScreen;

    [SerializeField] GameObject centerOfTheScreen;
    GameObject hitObj;
    RaycastHit hit; 

    [SerializeField]
    float shipSpeed; */

    // touch offset allows ball not to shake when it starts moving
    float deltaX, deltaY;
    // reference to Rigidbody2D component
    Rigidbody2D rb;
    // ball movement not allowed if you touches not the ball at the first time
    bool moveAllowed = false;
    bool shipTouched = false;

    ShipShoot ss;

    public List<GameObject> healthBar = new List<GameObject>();
    public GameObject invincibilityBubble;

    [SerializeField] AudioSource audioSource1;
    [SerializeField] AudioClip impactSound;
    [SerializeField] AudioClip immuneSound;
    [SerializeField] AudioClip coinSound;
    [SerializeField] AudioClip healthSound;

    // Start is called before the first frame update
    void Start()
    {
        /*halfScreenWidth = Screen.width / 2;
        halfScreenHeight = Screen.height / 2;
        centerScreen = Quaternion.Euler(0f, 0f, 0f); */
        rb = GetComponent<Rigidbody2D>();
        ss = this.gameObject.GetComponent<ShipShoot>();
        GetHealth();

        invincibilityBubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Movement();
        //TouchDrag();
        DragObject();
        //TouchMovement();
        DisplayHealth();
        PlayerStats.instance.shipTouched = shipTouched;
    }

    void GamePause()
    {
        Time.timeScale = 0f;
    }

    void UnPause()
    {
        Time.timeScale = 1f;
    }

    private void OnApplicationPause(bool pause)
    {

    }

    void DragObject()
    {
        if (PlayerStats.instance.gameWon == false)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                if (GetComponent<BoxCollider2D>() == Physics2D.OverlapPoint(touchPos))
                {
                    shipTouched = true;
                }
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (shipTouched)
                        {
                            // get the offset between position you touches
                            // and the center of the game object
                            deltaX = touchPos.x - transform.position.x;
                            deltaY = touchPos.y - transform.position.y;
                            // if touch begins within the ball collider
                            // then it is allowed to move
                            moveAllowed = true;
                            // restrict some rigidbody properties so it moves
                            // more  smoothly and correctly
                            rb.freezeRotation = true;
                            rb.velocity = new Vector2(0, 0);
                        }
                        break;
                    case TouchPhase.Moved:
                        if (shipTouched || moveAllowed)
                        {
                            rb.MovePosition(new Vector2(touchPos.x - deltaX, rb.position.y));
                        }
                        break;
                    case TouchPhase.Ended:
                        shipTouched = false;
                        moveAllowed = false;
                        rb.freezeRotation = true;
                        break;
                }

                /*Touch touch2 = Input.GetTouch(1);
                if (touch2.phase == TouchPhase.Began)
                {
                    ss.Fire();
                } */
            }
        }
    }

    void GetHealth()
    {
        for (int i = 0; i < healthBar.Count; i++)
        {
            healthBar[i].SetActive(false);
        }
        DisplayHealth();
    }

    void DisplayHealth()
    {
        for (int j = 0; j < PlayerStats.instance.health; j++)
        {
            healthBar[j].SetActive(true);
        }

        if (PlayerStats.instance.immune)
        {
            invincibilityBubble.SetActive(true);
        }
        else
        {
            invincibilityBubble.SetActive(false);
        }
    }

    public void DecreaseHealth(int amt)
    {
        StartCoroutine(GameManager.instance.CameraShake(.15f, .4f));
        healthBar[amt].SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PlayerStats.instance.immune)
        {
            if (!PlayerStats.instance.asteroidImmune)
            {
                if (collision.gameObject.CompareTag("AsteroidEnemy"))
                {
                    PlayImpactSound();
                    //collision.gameObject.SendMessage("Die");
                    collision.gameObject.GetComponent<AsteroidController>().Die();
                    PlayerStats.instance.GainXP(1f);
                    PlayerStats.instance.DecreaseHealth();
                    DecreaseHealth(PlayerStats.instance.health);
                    
                }
            }
            else
            {
                if (collision.gameObject.CompareTag("AsteroidEnemy"))
                {
                    PlayImmuneSound();
                    collision.gameObject.GetComponent<AsteroidController>().Die();
                    PlayerStats.instance.GainXP(1f);
                }
            }

            if (!PlayerStats.instance.shooterImmune)
            {
                if (collision.gameObject.CompareTag("SpaceshipEnemyLazer"))//(collision.gameObject.CompareTag("SpaceshipEnemy") || collision.gameObject.GetComponent<EnemyLazerController>())
                {
                    PlayImpactSound();
                    Destroy(collision.gameObject);
                    PlayerStats.instance.DecreaseHealth();
                    DecreaseHealth(PlayerStats.instance.health);
                }
                if (collision.gameObject.CompareTag("SpaceshipEnemy") || collision.gameObject.GetComponent<ShooterController>())
                {
                    PlayImpactSound();
                    collision.gameObject.GetComponent<ShooterController>().Die();
                    PlayerStats.instance.GainXP(3f);
                    PlayerStats.instance.DecreaseHealth();
                    DecreaseHealth(PlayerStats.instance.health);
                }
            }
            else
            {
                if (collision.gameObject.CompareTag("SpaceshipEnemyLazer"))//(collision.gameObject.CompareTag("SpaceshipEnemy") || collision.gameObject.GetComponent<EnemyLazerController>())
                {
                    PlayImmuneSound();
                    Destroy(collision.gameObject);
                }
                if (collision.gameObject.CompareTag("SpaceshipEnemy") || collision.gameObject.GetComponent<ShooterController>())
                {
                    PlayImmuneSound();
                    collision.gameObject.GetComponent<ShooterController>().Die();
                    PlayerStats.instance.GainXP(3f);
                }

            }

            if (!PlayerStats.instance.hovererImmune)
            {
                if (collision.gameObject.CompareTag("HEnemy") || collision.gameObject.GetComponent<HovererEnemyController>())
                {
                    PlayImpactSound();
                    collision.gameObject.GetComponent<ShooterController>().Die();
                    PlayerStats.instance.GainXP(5f);
                    PlayerStats.instance.DecreaseHealth();
                    DecreaseHealth(PlayerStats.instance.health);
                }
            }
            else
            {
                if (collision.gameObject.CompareTag("HEnemy") || collision.gameObject.GetComponent<HovererEnemyController>())
                {
                    PlayImmuneSound();
                    collision.gameObject.GetComponent<ShooterController>().Die();
                    PlayerStats.instance.GainXP(5f);
                }
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("AsteroidEnemy"))
            {
                PlayImmuneSound();
                collision.gameObject.GetComponent<AsteroidController>().Die();
                PlayerStats.instance.GainXP(1f);
            }
            
            if (collision.gameObject.CompareTag("SpaceshipEnemyLazer") || collision.gameObject.CompareTag("HEnemyLazer") || collision.gameObject.GetComponent<EnemyLazerController>())//(collision.gameObject.CompareTag("SpaceshipEnemy") || collision.gameObject.GetComponent<EnemyLazerController>())
            {
                PlayImmuneSound();
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.CompareTag("SpaceshipEnemy") || collision.gameObject.GetComponent<ShooterController>())
            {
                PlayImmuneSound();
                collision.gameObject.GetComponent<ShooterController>().Die();
                PlayerStats.instance.GainXP(3f);
            }

            if (collision.gameObject.CompareTag("HEnemy") || collision.gameObject.GetComponent<HovererEnemyController>())
            {
                PlayImmuneSound();
                collision.gameObject.GetComponent<ShooterController>().Die();
                PlayerStats.instance.GainXP(5f);
            }
        }
        
        if (collision.gameObject.CompareTag("Collectible"))
        {
            //audioSource1.PlayOneShot(impactSound);
            int itemType = collision.gameObject.GetComponent<Collectible>().itemType;
            if (itemType % 2 == 0)
            {
                audioSource1.PlayOneShot(coinSound);
            }
            else
            {
                audioSource1.PlayOneShot(healthSound);
            }
            
            collision.gameObject.GetComponent<Collectible>().RunItemProperty();
            collision.gameObject.GetComponent<Collectible>().Die();
        }

        if (collision.gameObject.CompareTag("PowerUp"))
        {
            collision.gameObject.GetComponent<PowerUpController>().SwitchState(PowerUpController.State.Activated);
        }

    }

    void PlayImpactSound()
    {
        audioSource1.PlayOneShot(impactSound);
    }

    void PlayImmuneSound()
    {
        audioSource1.PlayOneShot(immuneSound);
    }

    /*
    
    //sin cos put in x,y coordinate to get degrees, convert to radian, put that into sin cos

    void TouchMovement()
    {
        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out hit, 20))
                    {
                        hitObj = hit.collider.gameObject;
                    }
                    break;
                case TouchPhase.Moved:
                    float distance = shipSpeed * Time.deltaTime;
                    if (hitObj != null)
                    {
                        hitObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
                    }
                    break;
            }
        }
    }

    void TouchDrag()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(touchDeltaPosition.x * shipSpeed, touchDeltaPosition.y * shipSpeed, 0);
        }
    }

    void Movement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                Vector2 touchedPos = new Vector3(touch.position.x, 0f);
                if (touchedPos.x < halfScreenWidth)
                {
                    direction = false;
                }
                else if (touchedPos.x > halfScreenWidth)
                {
                    direction = true;
                }

                if (direction)
                {
                    timeCounter += Time.deltaTime * shipSpeed;
                }
                else
                {
                    timeCounter -= Time.deltaTime * shipSpeed;
                }

                float x = Mathf.Cos(timeCounter);
                float y = Mathf.Sin(timeCounter);
                transform.position = new Vector2(x, y);
            }
        }
    } */
}
