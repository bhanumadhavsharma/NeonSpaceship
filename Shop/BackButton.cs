using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void GoBack()
    {
        GameManager.instance.shopMenuOpened = false;
        SceneManager.LoadScene(0);
    }
}
