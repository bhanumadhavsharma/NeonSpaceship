using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopButtonController : MonoBehaviour
{
    public List<string> itemTitles = new List<string> {"Ship Shooting Speed", "Bullet Speed", "Ship Shield Level", "Gun Barrel Amount", "Default Ship Color", "Aqua Ship Color", "Lava Ship Color",
        "Lightning Ship Color", "Terra Ship Color", "Default Ship", "Tugboat", "Transporter Ship", "Gunship", "The Esmeralda", "Default Music", "Hard Music", "Low-Fi Electronic Music",
        "80's Synth Music" };
    List<string> itemDescriptions = new List<string>
    {"This is how fast your ship will shoot.", "This is how fast your bullets will travel.", "This is how much health you have.", "This is how many bullets you shoot at once.",
        "This is the default ship color.(No Bonus)", "This is the aqua ship color. (Ship Shield Level + 1)", "This is the lava ship color.(Ship Shooting Speed - .25)",
        "This is the lightning ship color. (Bullet Speed + 3)", "This is the terra ship color. (Slow Motion Enemies)", "This is the default ship type. (No Bonus)",
        "This is the tugboat ship type. (Immunity to Asteroids)", "This is the transporter ship type. (Immunity to Alien Gunships)", "This is the gunship ship type. (Immunity to Directorate Vessels)",
        "This is The Esmeralda. (Complete Immunity From Enemies)", "This is the default music in the game.", "Bop your head to this music.", "Electronica for the sci-fi lovers.",
        "This is for all the 80s synth fans." };

    int shipColor;
    int shipType;

    [SerializeField] TextMeshProUGUI IIW_item_title;
    [SerializeField] TextMeshProUGUI IIW_item_description;

    [SerializeField] GameObject itemCostObject;
    TextMeshProUGUI itemCostText;
    
    [SerializeField] GameObject _button;
    TextMeshProUGUI _button_text;
    [SerializeField] TextMeshProUGUI notEnoughFundsText;

    int itemNumber;
    bool maxLevel_OR_AbilityToSet;
    bool clickable;
    public int curLevelOfItem = 1;
    string buttonWords = "";
    Color newColor, newDisabledColor;
    int cost;

    private void Start()
    {
        itemCostText = itemCostObject.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        _button_text = _button.GetComponentInChildren<TextMeshProUGUI>();
        notEnoughFundsText.gameObject.SetActive(false);

        shipColor = PlayerStats.instance.shipColor;
        shipType = PlayerStats.instance.shipType;
        maxLevel_OR_AbilityToSet = false;

        IIW_item_title.text = "SHOP";
        IIW_item_description.text = "Tap on an item above in order to find out more about it!";
        itemCostObject.SetActive(false);
        _button.SetActive(false);
    }

    private void Update()
    {
        Fix0BulletBug();
    }

    void Fix0BulletBug()
    {
        if (PlayerStats.instance.bulletFlyingSpeed <= 0 || PlayerStats.instance.bulletShootingSpeed <= 0)
        {
            DetermineItemLevel(0);
            int amt = SetItemCost(0);
            switch (amt)
            {
                case 50:
                    if (PlayerStats.instance.shipColor != 2)
                    {
                        PlayerStats.instance.bulletShootingSpeed = 1.75f;
                    }
                    else
                    {
                        PlayerStats.instance.bulletShootingSpeed = 1.5f;
                    }
                    break;
                case 75:
                    if (PlayerStats.instance.shipColor != 2)
                    {
                        PlayerStats.instance.bulletShootingSpeed = 1.5f;
                    }
                    else
                    {
                        PlayerStats.instance.bulletShootingSpeed = 1.25f;
                    }
                    break;
                case 100:
                    if (PlayerStats.instance.shipColor != 2)
                    {
                        PlayerStats.instance.bulletShootingSpeed = 1.25f;
                    }
                    else
                    {
                        PlayerStats.instance.bulletShootingSpeed = 1f;
                    }
                    break;
                case 150:
                    if (PlayerStats.instance.shipColor != 2)
                    {
                        PlayerStats.instance.bulletShootingSpeed = 1f;
                    }
                    else
                    {
                        PlayerStats.instance.bulletShootingSpeed = .5f;
                    }
                    break;
                case 0:
                    if (PlayerStats.instance.shipColor != 2)
                    {
                        PlayerStats.instance.bulletShootingSpeed = .5f;
                    }
                    else
                    {
                        PlayerStats.instance.bulletShootingSpeed = .25f;
                    }
                    break;
            }

            DetermineItemLevel(1);
            amt = SetItemCost(1);
            switch (amt)
            {
                case 50:
                    if (PlayerStats.instance.shipColor != 3)
                    {
                        PlayerStats.instance.bulletFlyingSpeed = 1f;

                    }
                    else
                    {
                        PlayerStats.instance.bulletFlyingSpeed = 4f;
                    }
                    break;
                case 75:
                    if (PlayerStats.instance.shipColor != 3)
                    {
                        PlayerStats.instance.bulletFlyingSpeed = 2f;

                    }
                    else
                    {
                        PlayerStats.instance.bulletFlyingSpeed = 5f;
                    }
                    break;
                case 100:
                    if (PlayerStats.instance.shipColor != 3)
                    {
                        PlayerStats.instance.bulletFlyingSpeed = 3f;
                    }
                    else
                    {
                        PlayerStats.instance.bulletFlyingSpeed = 6f;
                    }
                    break;
                case 150:
                    if (PlayerStats.instance.shipColor != 3)
                    {
                        PlayerStats.instance.bulletFlyingSpeed = 4f;
                    }
                    else
                    {
                        PlayerStats.instance.bulletFlyingSpeed = 7f;
                    }
                    break;
                case 0:
                    if (PlayerStats.instance.shipColor != 3)
                    {
                        PlayerStats.instance.bulletFlyingSpeed = 5f;
                    }
                    else
                    {
                        PlayerStats.instance.bulletFlyingSpeed = 8f;
                    }
                    break;
            }
        }
    }

    public void PressButtonToBuyOrSet()
    {
        
        maxLevel_OR_AbilityToSet = DetermineItemLevel(itemNumber);
        //Debug.Log("itemNumber: " + itemNumber + "\nmaxlevel: " + maxLevel_OR_AbilityToSet + "\nclickable: " + clickable);

        if (maxLevel_OR_AbilityToSet)
        {
            if (clickable) // you can set it
            {
                //Debug.Log("Maxlevel & clickable");
                // change playerstat ship color or type
                switch (itemNumber) //cases 4 - 17
                {
                    case 4: //default ship color
                        PlayerStats.instance.shipType = 0;
                        PlayerStats.instance.shipColor = 0;
                        break;
                    case 5: //aqua ship color
                        PlayerStats.instance.shipType = 0;
                        PlayerStats.instance.shipColor = 1;
                        break;
                    case 6: //lava ship color
                        PlayerStats.instance.shipType = 0;
                        PlayerStats.instance.shipColor = 2;
                        break;
                    case 7: //lightning ship color
                        PlayerStats.instance.shipType = 0;
                        PlayerStats.instance.shipColor = 3;
                        break;
                    case 8: ///terra ship color
                        PlayerStats.instance.shipType = 0;
                        PlayerStats.instance.shipColor = 4;
                        break;
                    case 9: //default ship type
                        PlayerStats.instance.shipType = 0;
                        break;
                    case 10: //tugboat
                        PlayerStats.instance.shipType = 1;
                        break;
                    case 11: //transporter ship
                        PlayerStats.instance.shipType = 2;
                        break;
                    case 12: //gunship
                        PlayerStats.instance.shipType = 3;
                        break;
                    case 13: //esmeralda
                        PlayerStats.instance.shipType = 4;
                        break;
                    case 14: //default music
                        PlayerPrefs.SetInt("musicType", 0);
                        //PlayerPrefs.SetInt("music", 1);
                        //GameManager.instance.gameObject.GetComponent<AudioSource>().Play();
                        //PlayerPrefs.Save();
                        GameManager.instance.musicChanged = true;
                        Debug.Log(itemNumber);
                        break;
                    case 15: //music 2
                        PlayerPrefs.SetInt("musicType", 1);
                        //PlayerPrefs.SetInt("music", 1);
                        //GameManager.instance.gameObject.GetComponent<AudioSource>().Play();
                        //PlayerPrefs.Save();
                        GameManager.instance.musicChanged = true;
                        Debug.Log(itemNumber);
                        break;
                    case 16: //music 3
                        PlayerPrefs.SetInt("musicType", 2);
                        //PlayerPrefs.SetInt("music", 1);
                        //GameManager.instance.gameObject.GetComponent<AudioSource>().Play();
                        //PlayerPrefs.Save();
                        GameManager.instance.musicChanged = true;
                        Debug.Log(itemNumber);
                        break;
                    case 17: //music 4
                        PlayerPrefs.SetInt("musicType", 3);
                        //PlayerPrefs.SetInt("music", 1);
                        //GameManager.instance.gameObject.GetComponent<AudioSource>().Play();
                        //PlayerPrefs.Save();
                        GameManager.instance.musicChanged = true;
                        Debug.Log(itemNumber);
                        break;
                }
            }
            else // disabled
            {
                //Debug.Log("Maxlevel & !clickable");
                // empty
            }
        }
        else
        {
            if (cost > PlayerStats.instance.coinCount) // not enough coins
            {
                // display NOT ENOUGH COINS
                //StartCoroutine(DisplayPopUp());
                //Debug.Log("!Maxlevel & not enough money");
            }
            else // you can buy
            {
                //Debug.Log("!Maxlevel & enough money");
                // change bools or ints or floats of objects
                switch (itemNumber) // cases 0 - 17, except for 4, 9, 14
                {
                    case 0: // bss
                        if (curLevelOfItem == 1)
                        {
                            PlayerStats.instance.bulletShootingSpeed -= .25f;
                            PlayerStats.instance.coinCount -= 50;
                        }
                        else if (curLevelOfItem == 2)
                        {
                            PlayerStats.instance.bulletShootingSpeed -= .25f;
                            PlayerStats.instance.coinCount -= 75;
                        }
                        else if (curLevelOfItem == 3)
                        {
                            PlayerStats.instance.bulletShootingSpeed -= .25f;
                            PlayerStats.instance.coinCount -= 100;
                        }
                        else if (curLevelOfItem == 4)
                        {
                            PlayerStats.instance.bulletShootingSpeed -= .5f;
                            PlayerStats.instance.coinCount -= 150;
                        }
                        break;
                    case 1: // bfs
                        if (curLevelOfItem == 1)
                        {
                            PlayerStats.instance.bulletFlyingSpeed += 1f;
                            PlayerStats.instance.coinCount -= 50;
                        }
                        else if (curLevelOfItem == 2)
                        {
                            PlayerStats.instance.bulletFlyingSpeed += 1f;
                            PlayerStats.instance.coinCount -= 75;
                        }
                        else if (curLevelOfItem == 3)
                        {
                            PlayerStats.instance.bulletFlyingSpeed += 1f;
                            PlayerStats.instance.coinCount -= 100;
                        }
                        else if (curLevelOfItem == 4)
                        {
                            PlayerStats.instance.bulletFlyingSpeed += 1f;
                            PlayerStats.instance.coinCount -= 150;
                        }
                        break; 
                    case 2: // health
                        if (curLevelOfItem == 1)
                        {
                            PlayerStats.instance.health += 1;
                            PlayerStats.instance.maxHealth += 1;
                            PlayerStats.instance.coinCount -= 150;
                        }
                        else if (curLevelOfItem == 2)
                        {
                            PlayerStats.instance.health += 1;
                            PlayerStats.instance.maxHealth += 1;
                            PlayerStats.instance.coinCount -= 300;
                        }
                        break;
                    case 3: // amt of bullets
                        if (curLevelOfItem == 1)
                        {
                            PlayerStats.instance.amtBullets += 1;
                            PlayerStats.instance.coinCount -= 150;
                        }
                        else if (curLevelOfItem == 2)
                        {
                            PlayerStats.instance.amtBullets += 1;
                            PlayerStats.instance.coinCount -= 300;
                        }
                        break;
                    case 5: //aqua ship color
                        PlayerStats.instance.shipColor1 = true;
                        PlayerStats.instance.coinCount -= 200;
                        break;
                    case 6: //lava ship color
                        PlayerStats.instance.shipColor2 = true;
                        PlayerStats.instance.coinCount -= 200;
                        break;
                    case 7: //lightning ship color
                        PlayerStats.instance.shipColor3 = true;
                        PlayerStats.instance.coinCount -= 200;
                        break;
                    case 8: ///terra ship color
                        PlayerStats.instance.shipColor4 = true;
                        PlayerStats.instance.coinCount -= 200;
                        break;
                    case 10: //tugboat
                        PlayerStats.instance.shipType1 = true;
                        PlayerStats.instance.coinCount -= 500;
                        break;
                    case 11: //transporter ship
                        PlayerStats.instance.shipType2 = true;
                        PlayerStats.instance.coinCount -= 1000;
                        break;
                    case 12: //gunship
                        PlayerStats.instance.shipType3 = true;
                        PlayerStats.instance.coinCount -= 1500;
                        break;
                    case 13: //esmeralda
                        PlayerStats.instance.shipType4 = true;
                        PlayerStats.instance.coinCount -= 5000;
                        break;
                    case 14: //default music
                        PlayerPrefs.SetInt("musicType", 0);
                        //PlayerPrefs.SetInt("music", 1);
                        //GameManager.instance.gameObject.GetComponent<AudioSource>().Play();
                        //PlayerPrefs.Save();
                        GameManager.instance.musicChanged = true;
                        Debug.Log(itemNumber);
                        break;
                    case 15: //music 2
                        PlayerPrefs.SetInt("musicType", 1);
                        //PlayerPrefs.SetInt("music", 1);
                        //GameManager.instance.gameObject.GetComponent<AudioSource>().Play();
                        //PlayerPrefs.Save();
                        GameManager.instance.musicChanged = true;
                        Debug.Log(itemNumber);
                        break;
                    case 16: //music 3
                        PlayerPrefs.SetInt("musicType", 2);
                        //PlayerPrefs.SetInt("music", 1);
                        //GameManager.instance.gameObject.GetComponent<AudioSource>().Play();
                        //PlayerPrefs.Save();
                        GameManager.instance.musicChanged = true;
                        Debug.Log(itemNumber);
                        break;
                    case 17: //music 4
                        PlayerPrefs.SetInt("musicType", 3);
                        //PlayerPrefs.SetInt("music", 1);
                        //GameManager.instance.gameObject.GetComponent<AudioSource>().Play();
                        //PlayerPrefs.Save();
                        GameManager.instance.musicChanged = true;
                        Debug.Log(itemNumber);
                        break;
                }
            }
        }
        ClickOptionInMenu(itemNumber);
        SaveSystem.Save();
    }

    public void ClickOptionInMenu(int item)
    {
        itemNumber = item;
        _button.SetActive(true);
        shipColor = PlayerStats.instance.shipColor;
        shipType = PlayerStats.instance.shipType;
        //IIW_item_title.text = itemTitles[item];
        //IIW_item_description.text = itemDescriptions[item];
        //Debug.Log("Before DetermineItemLevel():\nitemNumber: " + itemNumber + "\nmaxlevel: " + maxLevel_OR_AbilityToSet + "\nclickable: " + clickable);
        bool temp = DetermineItemLevel(item); //function returns bool for ShopMenuController script; returning bool is otherwise useless
        cost = SetItemCost(item); //function returns int for ShopMenuController script
        SetBuyButton(item);
        Display(item);
        //Debug.Log("After DetermineItemLevel():\nitemNumber: " + itemNumber + "\nmaxlevel: " + maxLevel_OR_AbilityToSet + "\nclickable: " + clickable);
    }
    
    public bool DetermineItemLevel(int item)
    {
        maxLevel_OR_AbilityToSet = false;
        clickable = false;
        switch (item)
        {
            case 0: //BSS
                //if shooting speed < max && red ship equipped, then NOT MAX
                if ((PlayerStats.instance.bulletShootingSpeed == .5 && shipColor != 2) || (shipColor == 2 && PlayerStats.instance.bulletShootingSpeed == .25f))
                {
                    maxLevel_OR_AbilityToSet = true;
                    //maxLevel_OR_AbilityToSet = false;
                    clickable = false;
                }
                else
                {
                    if (shipColor == 2)
                    {
                        PlayerStats.instance.bulletShootingSpeed += .25f;
                    }

                    if (PlayerStats.instance.bulletShootingSpeed == 1.75f)
                    {
                        curLevelOfItem = 1;
                    }
                    else if (PlayerStats.instance.bulletShootingSpeed == 1.5f)
                    {
                        curLevelOfItem = 2;
                    }
                    else if (PlayerStats.instance.bulletShootingSpeed == 1.25f)
                    {
                        curLevelOfItem = 3;
                    }
                    else if (PlayerStats.instance.bulletShootingSpeed == 1f)
                    {
                        curLevelOfItem = 4;
                    }

                    if (shipColor == 2)
                    {
                        PlayerStats.instance.bulletShootingSpeed -= .25f;
                    }
                    maxLevel_OR_AbilityToSet = false;
                    clickable = false;
                }
                break;
            case 1: //BFS
                // if (bfs == 5 && shipColor != 3) || bfs == 8, then MAX
                if ((PlayerStats.instance.bulletFlyingSpeed == 5 && shipColor != 3) || PlayerStats.instance.bulletFlyingSpeed == 8)
                {
                    maxLevel_OR_AbilityToSet = true;
                    clickable = false;
                }
                else
                {
                    if (shipColor == 3)
                    {
                        PlayerStats.instance.bulletFlyingSpeed -= 3f;
                    }

                    if (PlayerStats.instance.bulletFlyingSpeed == 1f)
                    {
                        curLevelOfItem = 1;
                    }
                    else if (PlayerStats.instance.bulletFlyingSpeed == 2f)
                    {
                        curLevelOfItem = 2;
                    }
                    else if (PlayerStats.instance.bulletFlyingSpeed == 3f)
                    {
                        curLevelOfItem = 3;
                    }
                    else if (PlayerStats.instance.bulletFlyingSpeed == 4f)
                    {
                        curLevelOfItem = 4;
                    }

                    if (shipColor == 3)
                    {
                        PlayerStats.instance.bulletFlyingSpeed += 3f;
                    }
                    maxLevel_OR_AbilityToSet = false;
                    clickable = false;
                }
                break;
            case 2: //health
                // if shipColor != 1 && shield == 5 || shield == 6
                if ((PlayerStats.instance.maxHealth == 5 && shipColor != 1) || PlayerStats.instance.maxHealth == 6)
                {
                    maxLevel_OR_AbilityToSet = true;
                    clickable = false;
                }
                else
                {
                    if (shipColor == 1)
                    {
                        PlayerStats.instance.maxHealth -= 1;
                    }

                    if (PlayerStats.instance.maxHealth == 3f)
                    {
                        curLevelOfItem = 1;
                    }
                    else if (PlayerStats.instance.maxHealth == 4f)
                    {
                        curLevelOfItem = 2;
                    }

                    if (shipColor == 1)
                    {
                        PlayerStats.instance.maxHealth += 1;
                    }
                    maxLevel_OR_AbilityToSet = false;
                    clickable = false;
                }
                break;
            case 3: //amtOfBullets
                if (PlayerStats.instance.amtBullets == 3)
                {
                    maxLevel_OR_AbilityToSet = true;
                    clickable = false;
                }
                else
                {
                    if (PlayerStats.instance.amtBullets == 1f)
                    {
                        curLevelOfItem = 1;
                    }
                    else if (PlayerStats.instance.amtBullets == 2f)
                    {
                        curLevelOfItem = 2;
                    }
                    maxLevel_OR_AbilityToSet = false;
                    clickable = false;
                }
                break;
            case 4: //default ship color
                maxLevel_OR_AbilityToSet = true;
                if (shipColor != 0 || shipType != 0)
                {
                    clickable = true;
                }
                else
                {
                    clickable = false;
                }
                break;
            case 5: //aqua ship color
                if (PlayerStats.instance.shipColor1)
                {
                    maxLevel_OR_AbilityToSet = true;
                    if (shipColor != 1 || shipType != 0)
                    {
                        clickable = true;
                    }
                    else
                    {
                        clickable = false;
                    }
                }
                break;
            case 6: //lava ship color
                if (PlayerStats.instance.shipColor2)
                {
                    maxLevel_OR_AbilityToSet = true;
                    if (shipColor != 2 || shipType != 0)
                    {
                        clickable = true;
                    }
                    else
                    {
                        clickable = false;
                    }
                }
                break;
            case 7: //lightning ship color
                if (PlayerStats.instance.shipColor3)
                {
                    maxLevel_OR_AbilityToSet = true;
                    if (shipColor != 3 || shipType != 0)
                    {
                        clickable = true;
                    }
                    else
                    {
                        clickable = false;
                    }
                }
                break;
            case 8: ///terra ship color
                if (PlayerStats.instance.shipColor4)
                {
                    maxLevel_OR_AbilityToSet = true;
                    if (shipColor != 4 || shipType != 0)
                    {
                        clickable = true;
                    }
                    else
                    {
                        clickable = false;
                    }
                }
                break;
            case 9: //default ship type
                maxLevel_OR_AbilityToSet = true;
                if (shipType != 0)
                {
                    clickable = true;
                }
                else
                {
                    clickable = false;
                }
                break;
            case 10: //tugboat
                if (PlayerStats.instance.shipType1)
                {
                    maxLevel_OR_AbilityToSet = true;
                    if (shipType != 1)
                    {
                        clickable = true;
                    }
                    else
                    {
                        clickable = false;
                    }
                }
                break;
            case 11: //transporter ship
                if (PlayerStats.instance.shipType2)
                {
                    maxLevel_OR_AbilityToSet = true;
                    if (shipType != 2)
                    {
                        clickable = true;
                    }
                    else
                    {
                        clickable = false;
                    }
                }
                break;
            case 12: //gunship
                if (PlayerStats.instance.shipType3)
                {
                    maxLevel_OR_AbilityToSet = true;
                    if (shipType != 3)
                    {
                        clickable = true;
                    }
                    else
                    {
                        clickable = false;
                    }
                }
                break;
            case 13: //esmeralda
                if (PlayerStats.instance.shipType4)
                {
                    maxLevel_OR_AbilityToSet = true;
                    if (shipType != 4)
                    {
                        clickable = true;
                    }
                    else
                    {
                        clickable = false;
                    }
                }
                break;
            case 14: //default music
                maxLevel_OR_AbilityToSet = true;
                if (PlayerPrefs.GetInt("musicType") != 0)
                {
                    clickable = true;
                }
                else
                {
                    clickable = false;
                }
                break;
            case 15: //music 2
                maxLevel_OR_AbilityToSet = true;
                if (PlayerPrefs.GetInt("musicType") != 1)
                {
                    clickable = true;
                }
                else
                {
                    clickable = false;
                }
                break;
            case 16: //music 3
                maxLevel_OR_AbilityToSet = true;
                if (PlayerPrefs.GetInt("musicType") != 2)
                {
                    clickable = true;
                }
                else
                {
                    clickable = false;
                }
                break;
            case 17: //music 4
                maxLevel_OR_AbilityToSet = true;
                if (PlayerPrefs.GetInt("musicType") != 3)
                {
                    clickable = true;
                }
                else
                {
                    clickable = false;
                }
                break;
        }
        return maxLevel_OR_AbilityToSet;
    }

    public int SetItemCost(int item)
    {
        int amt = 0;
        
        if (!maxLevel_OR_AbilityToSet)
        {
            switch (item)
            {
                case 0: //BSS
                    switch (curLevelOfItem)
                    {
                        case 1:
                            amt = 50;
                            break;
                        case 2:
                            amt = 75;
                            break;
                        case 3:
                            amt = 100;
                            break;
                        case 4:
                            amt = 150;
                            break;
                    }
                    break;
                case 1: //BFS
                    switch (curLevelOfItem)
                    {
                        case 1:
                            amt = 50;
                            break;
                        case 2:
                            amt = 75;
                            break;
                        case 3:
                            amt = 100;
                            break;
                        case 4:
                            amt = 150;
                            break;
                    }
                    break;
                case 2: //health
                    switch (curLevelOfItem)
                    {
                        case 1:
                            amt = 150;
                            break;
                        case 2:
                            amt = 300;
                            break;
                    }
                    break;
                case 3: //amtOfBullets
                    switch (curLevelOfItem)
                    {
                        case 1:
                            amt = 150;
                            break;
                        case 2:
                            amt = 300;
                            break;
                    }
                    break;
                case 4: //default ship color
                    amt = 0;
                    break;
                case 5: //aqua ship color
                    amt = 200;
                    break;
                case 6: //lava ship color
                    amt = 200;
                    break;
                case 7: //lightning ship color
                    amt = 200;
                    break;
                case 8: //terra ship color
                    amt = 200;
                    break;
                case 9: //default ship type
                    amt = 0;
                    break;
                case 10: //tugboat
                    amt = 500;
                    break;
                case 11: //transporter ship
                    amt = 1000;
                    break;
                case 12: //gunship
                    amt = 1500;
                    break;
                case 13: //esmeralda
                    amt = 5000;
                    break;
            }
        }
        else
        {
            amt = 0;
        }

        return amt;
    }

    void SetBuyButton(int item)
    {
        if (maxLevel_OR_AbilityToSet)
        {
            if (clickable)
            {
                buttonWords = "SET";
            }
            else
            {
                buttonWords = "MAX";
            }
        }
        else
        {
            buttonWords = "BUY/UPGRADE";
        }
        

        if (maxLevel_OR_AbilityToSet)
        {
            if (clickable) // set to blue if set as (23, 159, 157)
            {
                // blue (23, 159, 157, 255)
                newColor.r = .090f;
                newColor.g = .624f;
                newColor.b = .616f;
                newColor.a = 1f;
            }
            else // set to disabled if current set or MAX
            {
                // disabled (137, 137, 137, 128)
                newDisabledColor.r = .538f;
                newDisabledColor.g = .538f;
                newDisabledColor.b = .538f;
                newDisabledColor.a = .502f;
            }
        }
        else
        {
            if (cost > PlayerStats.instance.coinCount) // set to red if not enough funds (222, 54, 43)
            {
                // red for not enough funds (222, 54, 43, 255)
                newColor.r = .871f;
                newColor.g = .218f;
                newColor.b = .169f;
                newColor.a = 1f;

            }
            else // set to green if upgradable (38, 159, 22)
            {
                // green for upgradable (38, 159, 22, 255)
                newColor.r = .149f;
                newColor.g = .624f;
                newColor.b = .086f;
                newColor.a = 1f;
            }
        }
    }

    void Display(int item)
    {
        IIW_item_title.text = itemTitles[item];
        IIW_item_description.text = itemDescriptions[item];

        if (cost > 0)
        {
            itemCostObject.SetActive(true);
            itemCostText.text = cost.ToString();
        }
        else
        {
            itemCostObject.SetActive(false);
        }

        if (maxLevel_OR_AbilityToSet && !clickable)
        {
            _button.GetComponent<Button>().interactable = false;
        }
        else
        {
            _button.GetComponent<Button>().interactable = true;
        }

        _button_text.text = buttonWords;

        ColorBlock colors = ColorBlock.defaultColorBlock;
        colors.normalColor = newColor;
        colors.highlightedColor = newColor;
        colors.selectedColor = newColor;
        colors.disabledColor = newDisabledColor;
        _button.GetComponent<Button>().colors = colors;
        //var colors = _button.colors;
        //colors.normalColor = newColor;
        //_button.colors.normalColor = colors;          
    }

    IEnumerator DisplayPopUp()
    {
        float duration = 2f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            notEnoughFundsText.gameObject.SetActive(true);
            elapsed += Time.unscaledTime;
            yield return null;
        }
        notEnoughFundsText.gameObject.SetActive(false);
    }
}
