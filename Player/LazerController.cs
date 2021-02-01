using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerController : MonoBehaviour
{
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
        else if (collision.gameObject.CompareTag("SpaceshipEnemy"))
        {
            collision.gameObject.GetComponent<ShooterController>().Die();
            LazerHitCameraShake();
            PlayerStats.instance.GainXP(3f);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "KillBox")
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("AsteroidEnemy") || collision.gameObject.GetComponent<AsteroidController>())
        {
            collision.gameObject.GetComponent<AsteroidController>().Die();
            LazerHitCameraShake();
            PlayerStats.instance.GainXP(1f);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.GetComponent<ShooterController>())//(collision.gameObject.CompareTag("SpaceshipEnemy") || collision.gameObject.GetComponent<ShooterController>()) 
        {
            collision.gameObject.GetComponent<ShooterController>().Die();
            LazerHitCameraShake();
            PlayerStats.instance.GainXP(3f);
            Destroy(this.gameObject);
        }

        else if (collision.gameObject.CompareTag("HEnemy") || collision.gameObject.GetComponent<HovererEnemyController>())
        {
            collision.gameObject.GetComponent<HovererEnemyController>().Die();
            LazerHitCameraShake();
            PlayerStats.instance.GainXP(5f);
            Destroy(this.gameObject);
        } 
        //else if for other 2 enemies   
    }
}
