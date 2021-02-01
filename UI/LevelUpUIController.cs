using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUpUIController : MonoBehaviour
{
    public GameObject levelUpPanel, continueButton;
    public TextMeshProUGUI levelUpText, unlockedItems;
    
    // Start is called before the first frame update
    void Start()
    {
        levelUpPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayLevelUpCanvas()
    {
        Time.timeScale = 0f;
        levelUpText.text = "Level up to " + PlayerStats.instance.playerLevel + "!";
        levelUpPanel.SetActive(true);
        PrintUnlockedList((int)PlayerStats.instance.playerLevel);
    }

    void PrintUnlockedList(int num)
    {
        bool itemUnlocked = true;
        string text = "";
        if (num < 16)
        {
            switch (num)
            {
                case 2:
                    text = "New Power-Up: Slow Motion\nDon't forget to read the instructions in the pause menu!";
                    break;
                case 3:
                    //text = "New Enemy: Alien Gunships";
                    text = "New Enemy: Alien Gunships\n\n50 Coins Awarded!";
                    PlayerStats.instance.coinCount += 50;
                    break;
                case 4:
                    itemUnlocked = false;
                    text = "75 Coins Awarded!\nRemember, you can spend these coins at the FREE shop in the pause menu!";
                    PlayerStats.instance.coinCount += 75;
                    break;
                case 5:
                    //text = "New Power-Up: Lightning Gun";
                    //text = "New Power-Up: Coin Magnet\n\n100 Coins Awarded!";
                    itemUnlocked = false;
                    text = "100 Coins Awarded!";
                    PlayerStats.instance.coinCount += 100;
                    break;
                case 6:
                    itemUnlocked = false;
                    text = "500 Coins Awarded!";
                    PlayerStats.instance.coinCount += 500;
                    break;
                case 7:
                    //text = "New Ship Type In Shop: Tugboat";
                    itemUnlocked = false;
                    text = "50 Coins Awarded!";
                    PlayerStats.instance.coinCount += 50;
                    break;
                case 8:
                    //text = "New Enemy: Directorate Vessels";
                    text = "New Enemy: Directorate Vessels\n\n50 Coins Awarded!";
                    PlayerStats.instance.coinCount += 50;
                    break;
                case 9:
                    //text = "New Power-Up: Invincibility Shield";
                    itemUnlocked = false;
                    text = "500 Coins Awarded!";
                    PlayerStats.instance.coinCount += 500;
                    break;
                case 10:
                    //text = "New Ship Type In Shop: Transporter";
                    itemUnlocked = false;
                    text = "50 Coins Awarded!";
                    PlayerStats.instance.coinCount += 50;
                    break;
                case 11:
                    itemUnlocked = false;
                    text = "1000 Coins Awarded!";
                    PlayerStats.instance.coinCount += 1000;
                    break;
                case 12:
                    //text = "New Ship Type In Shop: Gunship";
                    itemUnlocked = false;
                    text = "50 Coins Awarded!";
                    PlayerStats.instance.coinCount += 50;
                    break;
                case 13:
                    itemUnlocked = false;
                    text = "2000 Coins Awarded!";
                    PlayerStats.instance.coinCount += 2000;
                    break;
                case 14:
                    text = "New Ship Type In Shop: The Esmeralda\n\nALL SHOP ITEMS AVAILABLE FOR PURCHASING";
                    break;
                case 15:
                    itemUnlocked = false;
                    text = "x4 To All Coins Collected!";
                    PlayerStats.instance.coinX4Unlocked = true;
                    break;
            }
            if (itemUnlocked)
            {
                unlockedItems.text = "Unlocked:\n" + text;
            }
            else
            {
                unlockedItems.text = text;
            }
        }
        else
        {
            itemUnlocked = false;
            text = "1000 Coins Awarded!";
        }

        
    }

    public void LoadNextLevel()
    {
        PlayerStats.instance.gameWon = false;
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
