using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShoot : MonoBehaviour
{
    public GameObject lazerPrefab;
    public Transform shootPoint11, shootPoint21, shootPoint22, shootPoint31, shootPoint32, shootPoint33;
    //public Transform target;

    float timer;
    bool reset;

    [SerializeField] AudioSource audioSource1;
    [SerializeField] AudioClip clip1;

    //public int pooledAmount = 60;
    //List<GameObject> bullets;
    //bool amtHit;

    // Start is called before the first frame update
    void Start()
    {
        /*bullets = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(lazerPrefab);
            obj.SetActive(false);
            bullets.Add(obj);
        } */

        //InvokeRepeating("Fire", 0, (float)(2 / PlayerStats.instance.bulletShootingSpeed)); //to change, cancel invoke repeating and run it again.   
        timer = 0;
        reset = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!reset)
        {
            timer = PlayerStats.instance.bulletShootingSpeed;
            reset = true;
        }
        else
        {
            timer -= Time.fixedDeltaTime;//fixedDeltaTime;//deltaTime;
        }

        if (timer <= 0)
        {
            Fire();
            reset = false;
        }
    }

    public void Fire()
    {
        /*for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                if (PlayerStats.instance.amtBullets == 1)
                {
                    bullets[i].transform.position = shootPoint11.position;
                    bullets[i].SetActive(true);
                }
                else if (PlayerStats.instance.amtBullets == 2)
                {
                    bullets[i].transform.position = shootPoint21.position;
                    bullets[i].SetActive(true);
                    bullets[i + 1].transform.position = shootPoint22.position;
                    bullets[i + 1].SetActive(true);
                }
                else if (PlayerStats.instance.amtBullets == 3)
                {
                    bullets[i].tra
                }
                break;
            }
        } */

        audioSource1.PlayOneShot(clip1);

        if (PlayerStats.instance.amtBullets == 1)
        {
            GameObject newLazer = Instantiate(lazerPrefab, shootPoint11);
            Rigidbody2D rb = newLazer.GetComponent<Rigidbody2D>();
            //rb.MovePosition(new Vector2(shootPoint.transform.position.x, Time.time * bulletSpeed));
            rb.AddForce(new Vector2(0, PlayerStats.instance.bulletFlyingSpeed * 50));
        }
        else if (PlayerStats.instance.amtBullets == 2)
        {
            GameObject newLazer = Instantiate(lazerPrefab, shootPoint21);
            GameObject newLazer2 = Instantiate(lazerPrefab, shootPoint22);
            Rigidbody2D rb = newLazer.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = newLazer2.GetComponent<Rigidbody2D>();
            //rb.MovePosition(new Vector2(shootPoint.transform.position.x, Time.time * bulletSpeed));
            rb.AddForce(new Vector2(0, PlayerStats.instance.bulletFlyingSpeed * 50));
            rb2.AddForce(new Vector2(0, PlayerStats.instance.bulletFlyingSpeed * 50));
        }
        else if (PlayerStats.instance.amtBullets == 3)
        {
            GameObject newLazer = Instantiate(lazerPrefab, shootPoint31);
            GameObject newLazer2 = Instantiate(lazerPrefab, shootPoint32);
            GameObject newLazer3 = Instantiate(lazerPrefab, shootPoint33);
            Rigidbody2D rb = newLazer.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = newLazer2.GetComponent<Rigidbody2D>();
            Rigidbody2D rb3 = newLazer3.GetComponent<Rigidbody2D>();
            //rb.MovePosition(new Vector2(shootPoint.transform.position.x, Time.time * bulletSpeed));
            rb.AddForce(new Vector2(0, PlayerStats.instance.bulletFlyingSpeed * 50));
            rb2.AddForce(new Vector2(0, PlayerStats.instance.bulletFlyingSpeed * 50));
            rb3.AddForce(new Vector2(0, PlayerStats.instance.bulletFlyingSpeed * 50));
        }
    }
}
