using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject[] objects;
    float timer;
    bool timerReset = false;
    int num;

    // Start is called before the first frame update
    void Start()
    {
        num = Random.Range(0, objects.Length);
        GameObject instance = (GameObject)Instantiate(objects[num], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }

    private void Update()
    {
        if (!timerReset)
        {
            timer = Random.Range(2, 5);
            timerReset = true;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            num = Random.Range(0, objects.Length);
            GameObject instance = (GameObject)Instantiate(objects[num], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
            timerReset = false;
        }
    }
}
