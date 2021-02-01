using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 3;
    public int health = 3;
    public bool playerDied = false;

    public int amtBullets = 1;
    public float bulletFlyingSpeed = 1;
    public float bulletShootingSpeed = 3;
    [SerializeField] GameObject ship;
    public ParticleSystem playerExplosion;

    public float playerLevel = 1;
    public float xpRequired = 10;
    public float xp = 0;
    public ParticleSystem levelUpParticle;
    public TextMeshProUGUI playerLevelDisplay;
    public TextMeshProUGUI xpAmtText;
    public Image xpBar;

    public int coinCount;
    public bool coinX4Unlocked;
    public bool coinMagnetOn;

    public int shipColor;
    public int shipType;
    public bool shipColor0, shipColor1, shipColor2, shipColor3, shipColor4 = false;
    public bool shipType0, shipType1, shipType2, shipType3, shipType4 = false;
    public bool immune, asteroidImmune, shooterImmune, hovererImmune;
    bool aquaShip, lavaShip, lightningShip, terraShip = false;
    [SerializeField] Sprite spriteShipType0;
    [SerializeField] Sprite spriteShipType1;
    [SerializeField] Sprite spriteShipType2;
    [SerializeField] Sprite spriteShipType3;
    [SerializeField] Sprite spriteShipType4;

    int tempMaxHealth, tempHealth, tempAmtBullets;
    float tempBFS, tempBSS, tempPlayerLevel, tempXPR, tempXP;

    public Transform playerSpawnPoint;
    public GameObject playerShip;
    public bool loadedFile = false;

    public LevelUpUIController luuc;
    public bool gameWon = false;
    public bool shipTouched;

    public static PlayerStats instance;

    [SerializeField] AudioSource playerStatsAudioSource;
    [SerializeField] AudioClip playerDiedAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        SaveSystem.Load();

        RespawnShip();
        if (!loadedFile)
        {
            SetInitialStats();
            //RespawnShip();
        }
        else
        {
            LoadSaveFile();
            //RespawnShip();
        }
        

        //luuc = GameObject.Find("LevelUpCanvas").gameObject.GetComponent<LevelUpUIController>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayMainUI();
        //FixBugs();
        ShopChanges();
        //CheckScene();
    }

    void FixBugs()
    {
        if (bulletFlyingSpeed == 0 || bulletShootingSpeed == 0 || amtBullets == 0)
        {
            
        }
    }


    void ShopChanges()
    {
        // PUT IN ANY SHIP COLOR CHANGES OR SHIP TYPE CHANGES HERE
        Color newColor;

        switch (shipColor)
        {
            case 0:
                ship.GetComponent<SpriteRenderer>().sprite = spriteShipType0;
                newColor.r = 1f;
                newColor.g = 1f;
                newColor.b = 1f;
                newColor.a = 1f;
                ship.GetComponent<SpriteRenderer>().color = newColor;

                if (aquaShip)
                {
                    aquaShip = false;
                    maxHealth -= 1;
                }
                if (lavaShip)
                {
                    lavaShip = false;
                    bulletShootingSpeed += .25f;
                }
                if (lightningShip)
                {
                    lightningShip = false;
                    bulletFlyingSpeed -= 3f;
                }
                if (terraShip)
                {
                    terraShip = false;
                    EnemyController.instance.enemySpeed = 3f;
                }

                asteroidImmune = false;
                shooterImmune = false;
                hovererImmune = false;
                immune = false;
                break;
            case 1:
                ship.GetComponent<SpriteRenderer>().sprite = spriteShipType0;
                newColor.r = .243f;
                newColor.g = .871f;
                newColor.b = .871f;
                newColor.a = 1f;
                ship.GetComponent<SpriteRenderer>().color = newColor;

                if (!aquaShip)
                {
                    aquaShip = true;
                    maxHealth += 1;
                }
                if (lavaShip)
                {
                    lavaShip = false;
                    bulletShootingSpeed += .25f;
                }
                if (lightningShip)
                {
                    lightningShip = false;
                    bulletFlyingSpeed -= 3f;
                }
                if (terraShip)
                {
                    terraShip = false;
                    EnemyController.instance.enemySpeed = 3f;
                }

                asteroidImmune = false;
                shooterImmune = false;
                hovererImmune = false;
                immune = false;
                break;
            case 2:
                ship.GetComponent<SpriteRenderer>().sprite = spriteShipType0;
                newColor.r = .831f;
                newColor.g = .549f;
                newColor.b = .161f;
                newColor.a = 1f;
                ship.GetComponent<SpriteRenderer>().color = newColor;

                if (aquaShip)
                {
                    aquaShip = false;
                    maxHealth -= 1;
                }
                if (!lavaShip)
                {
                    lavaShip = true;
                    bulletShootingSpeed -= .25f;
                }
                if (lightningShip)
                {
                    lightningShip = false;
                    bulletFlyingSpeed -= 3f;
                }
                if (terraShip)
                {
                    terraShip = false;
                    EnemyController.instance.enemySpeed = 3f;
                }

                asteroidImmune = false;
                shooterImmune = false;
                hovererImmune = false;
                immune = false;
                break;
            case 3:
                ship.GetComponent<SpriteRenderer>().sprite = spriteShipType0;
                newColor.r = .831f;
                newColor.g = .827f;
                newColor.b = .129f;
                newColor.a = 1f;
                ship.GetComponent<SpriteRenderer>().color = newColor;

                if (aquaShip)
                {
                    aquaShip = false;
                    maxHealth -= 1;
                }
                if (lavaShip)
                {
                    lavaShip = false;
                    bulletShootingSpeed += .25f;
                }
                if (!lightningShip)
                {
                    lightningShip = true;
                    bulletFlyingSpeed += 3f;
                }
                if (terraShip)
                {
                    terraShip = false;
                    EnemyController.instance.enemySpeed = 3f;
                }

                asteroidImmune = false;
                shooterImmune = false;
                hovererImmune = false;
                immune = false;
                break;
            case 4:
                ship.GetComponent<SpriteRenderer>().sprite = spriteShipType0;
                newColor.r = .122f;
                newColor.g = .671f;
                newColor.b = .098f;
                newColor.a = 1f;
                ship.GetComponent<SpriteRenderer>().color = newColor;

                if (aquaShip)
                {
                    aquaShip = false;
                    maxHealth -= 1;
                }
                if (lavaShip)
                {
                    lavaShip = false;
                    bulletShootingSpeed += .25f;
                }
                if (lightningShip)
                {
                    lightningShip = false;
                    bulletFlyingSpeed -= 3f;
                }
                if (!terraShip)
                {
                    terraShip = true;
                    EnemyController.instance.enemySpeed = 1f;
                }

                asteroidImmune = false;
                shooterImmune = false;
                hovererImmune = false;
                immune = false;
                break;
        }

        switch (shipType)
        {
            case 0:
                break;
            case 1:
                ship.GetComponent<SpriteRenderer>().sprite = spriteShipType1;
                newColor.r = 1f;
                newColor.g = 1f;
                newColor.b = 1f;
                newColor.a = 1f;
                ship.GetComponent<SpriteRenderer>().color = newColor;

                if (aquaShip)
                {
                    aquaShip = false;
                    maxHealth -= 1;
                }
                if (lavaShip)
                {
                    lavaShip = false;
                    bulletShootingSpeed += .25f;
                }
                if (lightningShip)
                {
                    lightningShip = false;
                    bulletFlyingSpeed -= 3f;
                }
                if (terraShip)
                {
                    terraShip = false;
                    EnemyController.instance.enemySpeed = 3f;
                }

                asteroidImmune = true;
                shooterImmune = false;
                hovererImmune = false;
                immune = false;
                break;
            case 2:
                ship.GetComponent<SpriteRenderer>().sprite = spriteShipType2;
                newColor.r = 1f;
                newColor.g = 1f;
                newColor.b = 1f;
                newColor.a = 1f;
                ship.GetComponent<SpriteRenderer>().color = newColor;

                if (aquaShip)
                {
                    aquaShip = false;
                    maxHealth -= 1;
                }
                if (lavaShip)
                {
                    lavaShip = false;
                    bulletShootingSpeed += .25f;
                }
                if (lightningShip)
                {
                    lightningShip = false;
                    bulletFlyingSpeed -= 3f;
                }
                if (terraShip)
                {
                    terraShip = false;
                    EnemyController.instance.enemySpeed = 3f;
                }

                asteroidImmune = false;
                shooterImmune = true;
                hovererImmune = false;
                immune = false;
                break;
            case 3:
                ship.GetComponent<SpriteRenderer>().sprite = spriteShipType3;
                newColor.r = 1f;
                newColor.g = 1f;
                newColor.b = 1f;
                newColor.a = 1f;
                ship.GetComponent<SpriteRenderer>().color = newColor;

                if (aquaShip)
                {
                    aquaShip = false;
                    maxHealth -= 1;
                }
                if (lavaShip)
                {
                    lavaShip = false;
                    bulletShootingSpeed += .25f;
                }
                if (lightningShip)
                {
                    lightningShip = false;
                    bulletFlyingSpeed -= 3f;
                }
                if (terraShip)
                {
                    terraShip = false;
                    EnemyController.instance.enemySpeed = 3f;
                }

                asteroidImmune = false;
                shooterImmune = false;
                hovererImmune = true;
                immune = false;
                break;
            case 4:
                ship.GetComponent<SpriteRenderer>().sprite = spriteShipType4;
                newColor.r = 1f;
                newColor.g = 1f;
                newColor.b = 1f;
                newColor.a = 1f;
                ship.GetComponent<SpriteRenderer>().color = newColor;

                if (aquaShip)
                {
                    aquaShip = false;
                    maxHealth -= 1;
                }
                if (lavaShip)
                {
                    lavaShip = false;
                    bulletShootingSpeed += .25f;
                }
                if (lightningShip)
                {
                    lightningShip = false;
                    bulletFlyingSpeed -= 3f;
                }
                if (terraShip)
                {
                    terraShip = false;
                    EnemyController.instance.enemySpeed = 3f;
                }

                asteroidImmune = false;
                shooterImmune = false;
                hovererImmune = false;
                immune = true;
                break;
        }
    }

    /*void CheckScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && shopMenuOpened && !transitionToShopScene)
        {
            if (ship) { 
}
        }
        else if (SceneManager.GetActiveScene().buildIndex == 0 && !shopMenuOpened && !transitionToLevelScene)
        {
            MainMenuCanvas.SetActive(true);
            xpCanvas.SetActive(false);
            coinDisplayCanvas.SetActive(false);
            deathCanvas.SetActive(false);
            transitionToLevelScene = true;
            transitionToShopScene = false;
        }
    } */

    public void RespawnShip()
    {
        ship = Instantiate(playerShip, playerSpawnPoint);
        ResetStats();
        playerDied = false;
    }

    public void NewGameRespawnShip()
    {
        Destroy(ship);
        SetInitialStats();
        RespawnShip();
        playerDied = false;
        GameManager.instance.newGameReset = false;
    }

    public void LoadSaveFile()
    {
        SaveData data = SaveSystem.Load();
        health = data.health;
        maxHealth = data.maxHealth;
        amtBullets = data.amtBullets;

        bulletFlyingSpeed = data.bulletFlyingSpeed;
        bulletShootingSpeed = data.bulletShootingSpeed;

        playerLevel = data.playerLevel;
        xpRequired = data.xpRequired;
        xp = data.xp;

        coinCount = data.coinCount;
        coinX4Unlocked = data.coinX4Unlocked;

        shipColor = data.shipColor;
        shipType = data.shipType;

        shipColor0 = data.shipColor0;
        shipColor1 = data.shipColor1;
        shipColor2 = data.shipColor2;
        shipColor3 = data.shipColor3;
        shipColor4 = data.shipColor4;
        shipType0 = data.shipType0;
        shipType1 = data.shipType1;
        shipType2 = data.shipType2;
        shipType3 = data.shipType3;
        shipType4 = data.shipType4;

        immune = data.immune;
        asteroidImmune = data.asteroidImmune;
        shooterImmune = data.shooterImmune;
        hovererImmune = data.hovererImmune;

        //bool mt0 = data.musicType0;
        // WE ARE RUNNING INTO AN ERROR HERE, WHERE IT CANNOT ASSIGN THESE 4 VARIABLES BELOW US
        
        //GameManager.instance.musicType0 = data.musicType0;
        //GameManager.instance.musicType1 = data.musicType1;
        //GameManager.instance.musicType2 = data.musicType2;
        //GameManager.instance.musicType3 = data.musicType3;
    }

    public void SetInitialStats()
    {
        maxHealth = 3;
        health = 3;
        amtBullets = 1;
        bulletFlyingSpeed = 1;
        bulletShootingSpeed = 1.75f;

        playerLevel = 1;
        xpRequired = 10; 
        xp = 0;

        coinCount = 0;
        coinX4Unlocked = false;

        shipColor = 0;
        shipType = 0;
        shipColor0 = true;
        shipType0 = true;
        //insert function here to set those 2 things

        shipColor1 = false;
        shipColor2 = false;
        shipColor3 = false;
        shipColor4 = false;
        shipType1 = false;
        shipType2 = false;
        shipType3 = false;
        shipType4 = false;

        immune = false;
        asteroidImmune = false;
        shooterImmune = false;
        hovererImmune = false;

        GameManager.instance.musicType0 = true;
        GameManager.instance.musicType1 = true;
        GameManager.instance.musicType2 = true;
        GameManager.instance.musicType3 = true;

        coinMagnetOn = false;
    }

    void DisplayMainUI()
    {
        //xp bar
        playerLevelDisplay.text = playerLevel.ToString();
        xpBar.fillAmount = xp / xpRequired;
        xpAmtText.text = xp.ToString() + " / " + xpRequired.ToString();
    }

    /*void DisplayHealth()
    {
        for (int i = 0; i < healthBar.Length; i++)
        {
            healthBar[i] = ship.transform.Find("HealthBar").gameObject;//transform.Find("HB" + (i + 1)).gameObject;
            healthBar[i].SetActive(false);
        }
        for (int j = 0; j < health; j++)
        {
            
            healthBar[j].SetActive(true);
        }
    } */

    void ResetStats()
    {
        health = maxHealth;
    }

    public void DecreaseHealth()
    {
        health -= 1;
        if (health <= 0)
        {
            playerStatsAudioSource.PlayOneShot(playerDiedAudioClip);
            Instantiate(playerExplosion, ship.transform);
            Destroy(ship);
            playerDied = true;
            //shipTouched = false;
        }
    }

    public void GainXP(float amt)
    {
        xp += amt;
        while (xp >= xpRequired)
        {
            playerLevel++;
            xp -= xpRequired;
            SetXPToNextLevel(playerLevel + 1);
            Instantiate(levelUpParticle);
            gameWon = true;
            DisplayLevelUp();
        }
    }

    public void SetXPToNextLevel(float nextLevel)
    {
        xpRequired = (int)(Mathf.Pow(2, nextLevel) + (2 * nextLevel) + 8f); //old is xpRequired = (int)(Mathf.Pow(2, nextLevel) + 8);
    }

    void DisplayLevelUp()
    {
        luuc.DisplayLevelUpCanvas();
    }
}
