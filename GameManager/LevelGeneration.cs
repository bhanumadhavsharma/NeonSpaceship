using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    //public GameObject[] spawnPoints;
    //float timer;
    //bool timerReset = false;
    int num, num2;
    public bool gameWon = false;
    [SerializeField] float x1, x2;
    Vector3 x1Vector, x2Vector;
    [SerializeField] float spawnTime = 1.5f;
    SpriteRenderer sr;
    public float chance;

    GameObject objectSpawned;
    public GameObject[] arrayOfEnemies;
    public GameObject[] arrayOfEnemiesAfterLvl3;
    public GameObject[] arrayOfEnemiesAfterLvl8;
    public GameObject[] arrayOfPowerUpsAfterLvl2;
    public GameObject[] arrayOfPowerUpsAfterLvl5;
    public GameObject[] arrayOfPowerUpsAfterLvl9;

    float timer;
    bool reset;

    bool startTimerSet = false;
    float timeSinceStart = 0f;
    float totalTimePassed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        /*for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].gameObject.SetActive(false);
        } */

        sr = GetComponent<SpriteRenderer>();

        //string[] res = UnityStats.screenRes.Split('x');
        //Vector2 dist = UnityEditor.Handles.GetMainGameViewSize();
        //x1 = transform.position.x - dist.x;//int.Parse(res[0]);//Screen.currentResolution.width / 2; //Screen.width / 2;
        //x2 = transform.position.x + Screen.width / 2;

        x1 = transform.position.x - sr.bounds.size.x / 2;
        x2 = transform.position.x + sr.bounds.size.x / 2;

        //x1 = transform.position.x - Camera.main.pixelWidth / 2;
        //x2 = transform.position.x + Camera.main.pixelWidth / 2;
        x1Vector = new Vector3(x1, transform.position.y, transform.position.z);
        x2Vector = new Vector3(x2, transform.position.y, transform.position.z);

        //InvokeRepeating("SpawnEnemy", 0, spawnTime); //DO A TIMER INSTEAD TO MAKE IT SPAWN FASTER OVER TIME?
                                                     //        Invoke("SpawnEnemy", spawnTime);

        timer = 0;
        reset = true;
        totalTimePassed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        gameWon = PlayerStats.instance.gameWon;
        if (gameWon || PlayerStats.instance.playerDied)
        {
            /*Transform[] allChildren = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                child.gameObject.SetActive(false);
            } */

            /*for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            } */

            //this.gameObject.SetActive(false);
            spawnTime = 1.5f;
            startTimerSet = false;
        }
        else
        {
            if (!reset)
            {
                timer = spawnTime;
                reset = true;
            }
            else
            {
                timer -= Time.deltaTime;
            }

            if (timer <= 0 && Time.timeScale != 0f)
            {
                SpawnEnemy();
                reset = false;

                if (!startTimerSet)
                {
                    startTimerSet = true;
                    timeSinceStart = Time.time;
                }
            }
            
            if(Time.time >= timeSinceStart + totalTimePassed)
            {

                if (startTimerSet)
                {
                    spawnTime -= .15f;
                }
                if (spawnTime < .5f)
                {
                    spawnTime = .5f;
                }
                startTimerSet = false;
                timeSinceStart = 0f;
            }
        }
    }

    void SpawnEnemy()
    {
        if (!gameWon)
        {
            this.gameObject.SetActive(true);
            Vector2 SpawnPoint = new Vector2(Random.Range(x1, x2), transform.position.y);
            chance = Random.Range(0, 20) / 20f;

            if (chance > .05f || (PlayerStats.instance.playerLevel < 2))
            {
                if (PlayerStats.instance.playerLevel < 3)
                {
                    num = Random.Range(0, arrayOfEnemies.Length);
                    objectSpawned = Instantiate(arrayOfEnemies[num], SpawnPoint, Quaternion.identity, transform);
                }
                else if (PlayerStats.instance.playerLevel >= 3 && PlayerStats.instance.playerLevel < 8)
                {
                    num = Random.Range(0, arrayOfEnemiesAfterLvl3.Length);
                    objectSpawned = Instantiate(arrayOfEnemiesAfterLvl3[num], SpawnPoint, Quaternion.identity, transform);
                }
                else if (PlayerStats.instance.playerLevel >= 8)
                {
                    num = Random.Range(0, arrayOfEnemiesAfterLvl8.Length);
                    objectSpawned = Instantiate(arrayOfEnemiesAfterLvl8[num], SpawnPoint, Quaternion.identity, transform);
                }
                //enemy.transform.localScale = transform.parent.InverseTransformPoint(Vector3.one);
                Transform oldParent = transform.parent;
                objectSpawned.transform.parent = null;
                objectSpawned.transform.localScale = Vector3.one;
                objectSpawned.transform.parent = oldParent;
            }
            else
            {
                if (!GameManager.instance.powerUpActivated)
                {
                    if (PlayerStats.instance.playerLevel >= 2 && PlayerStats.instance.playerLevel < 5)
                    {
                        num = Random.Range(0, arrayOfPowerUpsAfterLvl2.Length);
                        objectSpawned = Instantiate(arrayOfPowerUpsAfterLvl2[num], SpawnPoint, Quaternion.identity, transform);
                    }
                    else if (PlayerStats.instance.playerLevel >= 5 && PlayerStats.instance.playerLevel < 9)
                    {
                        num = Random.Range(0, arrayOfPowerUpsAfterLvl5.Length);
                        objectSpawned = Instantiate(arrayOfPowerUpsAfterLvl5[num], SpawnPoint, Quaternion.identity, transform);
                    }
                    else if (PlayerStats.instance.playerLevel >= 9)
                    {
                        num = Random.Range(0, arrayOfPowerUpsAfterLvl9.Length);
                        objectSpawned = Instantiate(arrayOfPowerUpsAfterLvl9[num], SpawnPoint, Quaternion.identity, transform);
                    }
                    Transform oldParent = transform.parent;
                    objectSpawned.transform.parent = null;
                    objectSpawned.transform.localScale = Vector3.one;
                    objectSpawned.transform.parent = oldParent;
                }
            }
            
            /*if (!timerReset)
            {
                timer = Random.Range(2, 4);
                timerReset = true;
            }
            else
            {
                timer -= Time.deltaTime;
            }

            if (timer <= 0)
            {
                num = Random.Range(0, spawnPoints.Length);
                spawnPoints[num].SetActive(true);
                timerReset = false;
            }*/
        }
        else
        { 
            /*Transform[] allChildren = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                child.gameObject.SetActive(false);
            } */
            /*for (int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i].gameObject.SetActive(false);
            } */
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(x1Vector, x2Vector);
    }
}
