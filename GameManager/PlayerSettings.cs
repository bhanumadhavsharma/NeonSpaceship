using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Image noMusic;
    [SerializeField] private AudioSource gameMusic;

    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Image noSound;
    [SerializeField] private AudioListener al;

    [SerializeField] AudioClip[] musicType;

    private void Awake()
    {
        //0
        /*if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
        {
            Debug.Log("First Time Opening");

            //Set first time opening to false
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);

            //Do your stuff here
            PlayerStats.instance.loadedFile = false;

        }
        else
        {
            Debug.Log("NOT First Time Opening");

            //Do your stuff here
        } */

        // 1
        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetInt("music", 1);
            musicToggle.isOn = true;
            gameMusic.enabled = true;
            noMusic.gameObject.SetActive(false);
            PlayerPrefs.Save();
        }

        else
        {
            if (PlayerPrefs.GetInt("music") == 0)
            {
                gameMusic.enabled = false;
                musicToggle.isOn = false;
                noMusic.gameObject.SetActive(true);
                //change image of soundtoggle
            }
            else
            {
                gameMusic.enabled = true;
                musicToggle.isOn = true;
                noMusic.gameObject.SetActive(false);
                //change image of soundtoggle
            }
        }

        //2
        if (!PlayerPrefs.HasKey("musicType"))
        {
            PlayerPrefs.SetInt("musicType", 0);
            gameMusic.clip = musicType[0];
            PlayerPrefs.Save();
        }
        else
        {
            if (PlayerPrefs.GetInt("musicType") == 0)
            {
                gameMusic.clip = musicType[0];
            }
            else if (PlayerPrefs.GetInt("musicType") == 1)
            {
                gameMusic.clip = musicType[1];
            }
            else if (PlayerPrefs.GetInt("musicType") == 2)
            {
                gameMusic.clip = musicType[2];
            }
            else if (PlayerPrefs.GetInt("musicType") == 3)
            {
                gameMusic.clip = musicType[3];
            }
        }

        //3
        if (!PlayerPrefs.HasKey("sound"))
        {
            PlayerPrefs.SetInt("sound", 1);
            al.enabled = true;
            soundToggle.isOn = true;
            noSound.gameObject.SetActive(false);
            PlayerPrefs.Save();
        }
        else
        {
            if (PlayerPrefs.GetInt("sound") == 0)
            {
                al.enabled = false;
                soundToggle.isOn = false;
                noSound.gameObject.SetActive(true);
            }
            else
            {
                al.enabled = true;
                soundToggle.isOn = true;
                noSound.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("musicType") == 0)
        {
            gameMusic.clip = musicType[0];
        }
        else if (PlayerPrefs.GetInt("musicType") == 1)
        {
            gameMusic.clip = musicType[1];
        }
        else if (PlayerPrefs.GetInt("musicType") == 2)
        {
            gameMusic.clip = musicType[2];
        }
        else if (PlayerPrefs.GetInt("musicType") == 3)
        {
            gameMusic.clip = musicType[3];
        }
    }

    public void ToggleMusic()
    {
        if (musicToggle.isOn)
        {
            PlayerPrefs.SetInt("music", 1);
            gameMusic.enabled = true;
            noMusic.gameObject.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("music", 0);
            gameMusic.enabled = false;
            noMusic.gameObject.SetActive(true);
        }
        PlayerPrefs.Save();
    }

    public void ToggleSound()
    {
        if (soundToggle.isOn)
        {
            PlayerPrefs.SetInt("sound", 1);
            al.enabled = true;
            noSound.gameObject.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("sound", 0);
            al.enabled = false;
            noSound.gameObject.SetActive(true);
        }
        PlayerPrefs.Save();
    }
}
