using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpController : MonoBehaviour
{
    public PowerUp powerup;

    [SerializeField] SpriteRenderer sr;
    Rigidbody2D rb;

    public enum State { Falling, Activated, Deactivated }
    public State currentState;

    public int powerUpType;
    float powerUpSpeed = 2;
    float chance;
    float powerUpStartTime;
    public float powerUpDuration = 5f;
    bool powerUpHit = false;

    float ogBSS, ogBFS;

    [SerializeField] AudioSource gameManagerAudioSource;
    [SerializeField] AudioSource audioSource1;
    [SerializeField] AudioClip slowMotionSoundEffect;
    [SerializeField] AudioClip invincibilitySoundEffect;
    [SerializeField] AudioClip coinMagnetSoundEffect;

    [SerializeField] GameObject slowMotionPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = powerup.artwork;

        chance = Random.Range(1, 3) % 2;
        rb = this.gameObject.GetComponent<Rigidbody2D>();

        slowMotionPanel.SetActive(false);

        currentState = State.Falling;
        if (powerup.slowMotion)
        {
            powerUpType = 0;
        }
        else if (powerup.invincibility)
        {
            powerUpType = 1;
        }
        else if (powerup.fastShooting)
        {
            powerUpType = 2;
        }
        else if (powerup.coinMagnet)
        {
            powerUpType = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Falling:
                UpdateFallingState();
                break;
            case State.Activated:
                UpdateActivatedState();
                break;
            case State.Deactivated:
                UpdateDeactivatedState();
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>

    void EnterFallingState()
    {
        //
    }

    void UpdateFallingState()
    {
        Movement();
        Delete();
    }

    void ExitFallingState()
    {
        sr.enabled = false;
    }

    void EnterActivatedState()
    {
        GameManager.instance.powerUpActivated = true;
        powerUpDuration = 10f;
        powerUpStartTime = Time.unscaledTime;
        PlaySoundEffect();
        DoEffect();
    }

    void UpdateActivatedState()
    {
        if (Time.unscaledTime >= powerUpStartTime + powerUpDuration)
        {
            SwitchState(State.Deactivated);
        }
        Delete();
    }

    void ExitActivatedState()
    {

    }

    void EnterDeactivatedState()
    {
        StopEffect();
        StopSoundEffect();
        GameManager.instance.powerUpActivated = false;
        Destroy(this.gameObject);
    }

    void UpdateDeactivatedState()
    {

    }

    void ExitDeactivatedState()
    {

    }

    public void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Falling:
                ExitFallingState();
                break;
            case State.Activated:
                ExitActivatedState();
                break;
            case State.Deactivated:
                ExitDeactivatedState();
                break;
        }

        switch (state)
        {
            case State.Falling:
                EnterFallingState();
                break;
            case State.Activated:
                EnterActivatedState();
                break;
            case State.Deactivated:
                EnterDeactivatedState();
                break;
        }

        currentState = state;
    }

    /// <summary>
    /// 
    /// </summary>

    void Movement()
    {
        rb.velocity = new Vector2(0, -powerUpSpeed);
        if (chance == 0)
        {
            transform.Rotate(new Vector3(0, 0, Random.Range(45, 90)) * Time.deltaTime);
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, Random.Range(-45, -90)) * Time.deltaTime);
        }
    }

    void DoEffect()
    {
        switch (powerUpType)
        {
            case 0:
                ActivateSlowMotion();
                break;
            case 1:
                ActivateInvincibility();
                break;
            case 2:
                ActivateFastShooting();
                break;
            case 3:
                ActivateCoinMagnet();
                break;
        }
    }

    void StopEffect()
    {
        switch (powerUpType)
        {
            case 0:
                DeactivateSlowMotion();
                break;
            case 1:
                DeactivateInvincibility();
                break;
            case 2:
                DeactivateFastShooting();
                break;
            case 3:
                DeactivateCoinMagnet();
                break;
        }
    }

    void PlaySoundEffect()
    {
        switch (powerUpType)
        {
            case 0:
                audioSource1.PlayOneShot(slowMotionSoundEffect);
                gameManagerAudioSource = GameManager.instance.gameObject.GetComponent<AudioSource>();
                gameManagerAudioSource.pitch = Time.timeScale = .5f;
                break;
            case 1:
                audioSource1.PlayOneShot(invincibilitySoundEffect);
                break;
            case 2:
                break;
            case 3:
                //audioSource1.PlayOneShot(coinMagnetSoundEffect);
                break;
        }
    }

    void StopSoundEffect()
    {
        switch (powerUpType)
        {
            case 0:
                gameManagerAudioSource = GameManager.instance.gameObject.GetComponent<AudioSource>();
                gameManagerAudioSource.pitch = Time.timeScale = 1f;
                break;
        }
    }

    void Delete()
    {
        if (PlayerStats.instance.gameWon || PlayerStats.instance.playerDied || GameManager.instance.newGameReset || GameManager.instance.shopMenuOpened)
        {
            GameManager.instance.powerUpActivated = false;
            EnterDeactivatedState();
        }
    }

    /// <summary>
    /// 
    /// </summary>

    void ActivateSlowMotion()
    {
        //GameManager.instance.defaultTimeScale = GameManager.instance.slowMotionTimeScale;
        slowMotionPanel.SetActive(true);
        EnemyController.instance.slowMotionOn = true;
    }

    void DeactivateSlowMotion()
    {
        //GameManager.instance.defaultTimeScale = GameManager.instance.runningTimeScale;
        slowMotionPanel.SetActive(false);
        EnemyController.instance.slowMotionOn = false;
    }

    void ActivateInvincibility()
    {
        //PlayerStats.instance.homingTarget
        PlayerStats.instance.immune = true;
    }

    void DeactivateInvincibility()
    {
        PlayerStats.instance.immune = false;
    }

    void ActivateFastShooting()
    {
        ogBSS = PlayerStats.instance.bulletShootingSpeed;
        ogBFS = PlayerStats.instance.bulletFlyingSpeed;
        PlayerStats.instance.bulletShootingSpeed = .25f;
        PlayerStats.instance.bulletFlyingSpeed = 8f;
    }

    void DeactivateFastShooting()
    {
        PlayerStats.instance.bulletShootingSpeed = ogBSS;
        PlayerStats.instance.bulletFlyingSpeed = ogBFS;
    }

    void ActivateCoinMagnet()
    {
        PlayerStats.instance.coinMagnetOn = true;
    }

    void DeactivateCoinMagnet()
    {
        PlayerStats.instance.coinMagnetOn = false;
    }

    /* private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerStats>())
        {
            //Destroy(this.gameObject);
            //DoEffect();
            SwitchState(State.Activated);
        }
    } */
}
