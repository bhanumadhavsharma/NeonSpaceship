using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    //[SerializeField] private GameObject _mainLevelObject;
    //[SerializeField] private GameObject _shopObject;

    [SerializeField] private Button settingsButton, shopButton;
    [SerializeField] private Button backOutOfSettingsButton, newGameButton;
    [SerializeField] private Toggle musicToggleButton;

    [SerializeField] private GameObject mainMenuTopPanel, mainMenuBottomPanel;
    [SerializeField] private Canvas settingsCanvas;

    [SerializeField] private GameObject xpCanvas, shopLockedImageObject;
    [SerializeField] private GameObject coinDisplayCanvas;

    [SerializeField] private GameObject areYouSureCanvas;

    [SerializeField] private TextMeshProUGUI instructions;
    private void Start()
    {
        mainMenuTopPanel.SetActive(true);
        mainMenuBottomPanel.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
        /*xpCanvas = GameObject.Find("XPCanvas").gameObject;
        coinDisplayCanvas = GameObject.Find("CoinDisplayCanvas").gameObject;
        areYouSureCanvas = GameObject.Find("CoinDisplayCanvas").gameObject;*/
        xpCanvas.SetActive(false);
        coinDisplayCanvas.SetActive(true);
        areYouSureCanvas.SetActive(false);
    }

    private void Update()
    {
        instructions.text = "Hold Down Ship at bottom of screen to Start Game!\nShoots Automatically\nUse collected coins to upgrade/buy from FREE Shop";
        shopLockedImageObject.SetActive(false);
    }

    public void openSettings()
    {
        PlayerStats.instance.gameWon = false;
        mainMenuBottomPanel.SetActive(false);
        mainMenuTopPanel.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
        xpCanvas.SetActive(false);
        coinDisplayCanvas.SetActive(false);
    }

    public void closeSettings()
    {
        PlayerStats.instance.gameWon = false;
        mainMenuTopPanel.SetActive(true);
        mainMenuBottomPanel.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
        xpCanvas.SetActive(false);
        coinDisplayCanvas.SetActive(true);
    }

    /*public void OpenShop()
    {
        // ADD SCENE TRANSITION
        if (PlayerStats.instance.playerLevel >= 4)
        {
            GameManager.instance.shopMenuOpened = true;
            _mainLevelObject.SetActive(false);
            _shopObject.SetActive(true);
        }
    }

    public void BackOutOfShop()
    {
        GameManager.instance.shopMenuOpened = false;
        _mainLevelObject.SetActive(true);
        _shopObject.SetActive(true);
    } */

    public void DisplayAreYouSure()
    {
        //display "are you sure?" screen
        areYouSureCanvas.SetActive(true);
        backOutOfSettingsButton.interactable = false;
        //musicToggleButton.interactable = false;
        musicToggleButton.enabled = false;
        newGameButton.interactable = false;
    }

    public void NewGameNo()
    {
        //go back to settings
        areYouSureCanvas.SetActive(false);
        backOutOfSettingsButton.interactable = true;
        musicToggleButton.interactable = true;
        newGameButton.interactable = true;
    }

    public void NewGameYes()
    {
        GameManager.instance.newGameReset = true;

        //start a new game
        areYouSureCanvas.SetActive(false);
        backOutOfSettingsButton.interactable = true;
        //musicToggleButton.interactable = true;
        musicToggleButton.enabled = true;
        newGameButton.interactable = true;
        closeSettings();

        PlayerPrefs.SetInt("musicType", 0);
        GameManager.instance.musicChanged = true;

        PlayerStats.instance.playerDied = true;
        PlayerStats.instance.NewGameRespawnShip();
        SaveSystem.Save();
    }
}
