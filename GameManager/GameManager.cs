using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool gameStart = false;
    public GameObject MainMenuCanvas, xpCanvas, coinDisplayCanvas, deathCanvas;
    public bool afterInitialStart = false;

    public bool newGameReset = false;
    public bool shopMenuOpened = false;
    bool transitionToShopScene, transitionToLevelScene;

    public float defaultTimeScale = 1f;
    public float runningTimeScale = 1f;
    public float pausedTimeScale = 0f;
    public float slowMotionTimeScale = .3f;
    public float fixedDeltaTimeValue = .02f;

    public bool powerUpActivated = false;
    public bool musicType0, musicType1, musicType2, musicType3 = true;
    public bool musicChanged = false;

    private void Awake()
    {
        afterInitialStart = false;
    }

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
        //MainMenuCanvas = GameObject.Find("MainMenuCanvas").gameObject;
        //xpCanvas = GameObject.Find("XPCanvas").gameObject;
        MainMenuCanvas.SetActive(true);
        xpCanvas.SetActive(false);
        coinDisplayCanvas.SetActive(true);
        //deathCanvas = GameObject.Find("DeathCanvas").gameObject;
        deathCanvas.SetActive(false);
        DontDestroyOnLoad(this.gameObject);
        Application.targetFrameRate = 30;

        if (PlayerPrefs.GetInt("music") == 1)
        {
            this.gameObject.GetComponent<AudioSource>().Play();
            musicChanged = false;
        }
    }

    private void Update()
    {
        //CheckScene();
        if (!afterInitialStart)
        {
            CheckIfInitialStart();
        }
        if (Input.touchCount > 0)
        {
            if (!PlayerStats.instance.gameWon && PlayerStats.instance.shipTouched && !shopMenuOpened) //is touching screen, no level up, ship being touched
            {
                gameStart = true;
            }
        }
        else //not touching screen
        {
            gameStart = false;
        }
        CheckIfGameStart();
        CheckIfMusicChange();
    }

    void CheckIfMusicChange()
    {
        if (musicChanged)
        {
            this.gameObject.GetComponent<AudioSource>().Stop();
            this.gameObject.GetComponent<AudioSource>().Play();
            musicChanged = false;
        }
    }

    /*void CheckScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && shopMenuOpened) //&& !transitionToShopScene)
        {
            MainMenuCanvas.SetActive(false);
            xpCanvas.SetActive(false);
            coinDisplayCanvas.SetActive(false);
            deathCanvas.SetActive(false);
            //transitionToShopScene = true;
            //transitionToLevelScene = false;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 0 && !shopMenuOpened) //&& !transitionToLevelScene)
        {
            MainMenuCanvas.SetActive(true);
            xpCanvas.SetActive(false);
            coinDisplayCanvas.SetActive(false);
            deathCanvas.SetActive(false);
            //transitionToLevelScene = true;
            //transitionToShopScene = false;
        }
    } */

    /*private void OnLevelWasLoaded(int level)
    {
        MainMenuCanvas = GameObject.Find("MainMenuCanvas").gameObject;
        //xpCanvas = GameObject.Find("XPCanvas").gameObject;
        MainMenuCanvas.SetActive(true);
        xpCanvas.SetActive(false);
        coinDisplayCanvas.SetActive(true);
        deathCanvas = GameObject.Find("DeathCanvas").gameObject;
        deathCanvas.SetActive(false);
        Application.targetFrameRate = 30;
    } */

    public void CheckIfGameStart()
    {
        if (gameStart) //is touching screen
        {
            Time.timeScale = defaultTimeScale;
            MainMenuCanvas.SetActive(false);
            xpCanvas.SetActive(true);
            coinDisplayCanvas.SetActive(true);
            if (PlayerStats.instance.playerDied)
            {
                Time.timeScale = pausedTimeScale;
                xpCanvas.SetActive(false);
                coinDisplayCanvas.SetActive(false);
                deathCanvas.SetActive(true);
            }
        }
        else
        {
            SaveSystem.Save();
            Time.timeScale = pausedTimeScale;
            coinDisplayCanvas.SetActive(false);
            if (!PlayerStats.instance.gameWon && !PlayerStats.instance.playerDied)// && !PlayerStats.instance.shipTouched)//&& not shopMenu) //not touching screen, not a level up
            {
                MainMenuCanvas.SetActive(true);
                xpCanvas.SetActive(false);
                //coinDisplayCanvas.SetActive(false);
            }
            // OKAY, SO NOW TRY IT SO THAT UPON DYING, TOUCHING THE SCREEN DOES NOT CAUSE GAME TO RESUME; 
            // IE. implement shiptouched = false on player death
            // OR implement !PlayerStats,instance.playerDied on line 44 
            else if (PlayerStats.instance.gameWon)//not touching screen, is a level up
            {
                xpCanvas.SetActive(true);
                //coinDisplayCanvas.SetActive(false);
                //if shopMenu
                //   xpCanvas.SetActive(false);
            }
            else if (PlayerStats.instance.playerDied)
            {
                deathCanvas.SetActive(true);
            }
        }
        Time.fixedDeltaTime = fixedDeltaTimeValue;
    }

    void CheckIfInitialStart()
    {
        if (Time.time > 0 && Input.touchCount > 0 && PlayerStats.instance.shipTouched)
        {
            afterInitialStart = true;
        }
    }

    public IEnumerator CameraShake(float duration, float magnitude) {
        Vector3 ogCamPos = Camera.main.transform.localPosition;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.localPosition = new Vector3(x, y, ogCamPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.localPosition = ogCamPos;
    }
}
