using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLazerController : MonoBehaviour
{

    private void FixedUpdate()
    {
        Rigidbody2D rigBody = this.GetComponent<Rigidbody2D>();
        //rb.MovePosition(new Vector2(shootPoint.transform.position.x, Time.time * bulletSpeed));
        //rigBody.AddForce(new Vector2(0, 1.5f * 50));
        if (EnemyController.instance.slowMotionOn)
        {
            rigBody.velocity = (transform.up) * Time.deltaTime * (66f);
        }
        else
        {
            rigBody.velocity = (transform.up) * Time.deltaTime * (200f);
        }
    }

    void LazerHitCameraShake()
    {
        StartCoroutine(GameManager.instance.CameraShake(.05f, .1f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "KillBox")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "KillBox")
        {
            Destroy(this.gameObject);
        }
        /*else if (collision.gameObject.CompareTag("Player") || collision.gameObject.GetComponent<PlayerMovement>())
        {
            PlayerStats.instance.DecreaseHealth();
            collision.gameObject.GetComponent<PlayerMovement>().DecreaseHealth(PlayerStats.instance.health);
            //PlayerStats.instance.DecreaseHealth();
            LazerHitCameraShake();
            //PlayerStats.instance.GainXP(3f);
            Destroy(this.gameObject);
        } */
        //else if for other 2 enemies   
    }
}
