using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBackground : MonoBehaviour
{
    //Transform item;
    public float xEuler, yEuler, zEuler;

    // Start is called before the first frame update
    void Start()
    {
        //item = this.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //item.Rotate(new Vector3(.01f, 1f));
        transform.Rotate(xEuler, yEuler, zEuler);
    }
}
