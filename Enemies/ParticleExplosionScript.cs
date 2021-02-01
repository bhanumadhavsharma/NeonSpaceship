using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleExplosionScript : MonoBehaviour
{
    float timer = 3f;
    bool reset = false;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(DieAfterTime());
        timer = 3f;
        reset = false;
    }

    private void Update()
    {
        if (!reset)
        {
            timer = 3f;
            reset = true;
        }
        else
        {
            timer -= Time.fixedDeltaTime;//fixedDeltaTime;//deltaTime;
        }

        if (timer <= 0)
        {
            Die();
            reset = false;
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    IEnumerator DieAfterTime()
    {
        yield return new WaitForSeconds(3);
        Die();
    }
}
