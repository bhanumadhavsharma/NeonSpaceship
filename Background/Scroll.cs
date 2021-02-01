using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float ScrollX = -0.2f;
    public float ScrollY = 0f;

    // Update is called once per frame
    void Update()
    {
        if (EnemyController.instance.slowMotionOn)
        {
            float offsetX = Time.time * ScrollX * (2/3f);
            float offsetY = Time.time * ScrollY * (2/3f);
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);
        }
        else
        {
            float offsetX = Time.time * ScrollX;
            float offsetY = Time.time * ScrollY;
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);
        }
    }
}
