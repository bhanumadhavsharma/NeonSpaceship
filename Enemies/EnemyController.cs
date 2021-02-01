using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    public float enemySpeed = 3f;
    public float slowEnemySpeed = 1f;
    public bool slowMotionOn = false;
    public float rotatingSpeed = 200f;
    public float slowRotatingSpeed = 10f;

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
    }

    
}
