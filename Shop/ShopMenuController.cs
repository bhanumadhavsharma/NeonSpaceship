using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopMenuController : MonoBehaviour
{
    [SerializeField] GameObject shipType_Tugboat; // 7
    [SerializeField] GameObject shipType_Transporter; // 10
    [SerializeField] GameObject shipType_Gunship; // 12
    [SerializeField] GameObject shipType_esmeralda; // 14

    [SerializeField] GameObject[] gridListContent;
    //GameObject[] allChildren;
    bool maxedOut = false;

    [SerializeField] TextMeshProUGUI _coinAmount;

    private void Start()
    {
        //StoreChildrenGameObjects();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfUnlocked();
        MarkCompleted();
        DisplayCoins();
    }

    void DisplayCoins()
    {
        _coinAmount.text = PlayerStats.instance.coinCount.ToString();
    }

    /*void StoreChildrenGameObjects()
    {
        int i = 0;
        allChildren = new GameObject[gridListContent.transform.childCount];
        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }
    } */

    void CheckIfUnlocked() 
    {
        /*if (PlayerStats.instance.playerLevel >= 7)
        {
            shipType_Tugboat.SetActive(true);
        }
        else
        {
            shipType_Tugboat.SetActive(false);
        }

        if (PlayerStats.instance.playerLevel >= 10)
        {
            shipType_Transporter.SetActive(true);
        }
        else
        {
            shipType_Transporter.SetActive(false);
        }

        if (PlayerStats.instance.playerLevel >= 12)
        {
            shipType_Gunship.SetActive(true);
        }
        else
        {
            shipType_Gunship.SetActive(false);
        } */

        if (PlayerStats.instance.playerLevel >= 14)
        {
            shipType_esmeralda.SetActive(true);
        }
        else
        {
            shipType_esmeralda.SetActive(false);
        }
    }

    void MarkCompleted()
    {
        for (int i = 0; i < gridListContent.Length; i++)
        {
            maxedOut = this.gameObject.GetComponent<ShopButtonController>().DetermineItemLevel(i);
            if (maxedOut)
            {
                Image itemBox = gridListContent[i].GetComponent<Image>();
                Color newColor = itemBox.color;
                newColor.r = .32f;
                newColor.g = .89f;
                newColor.b = .73f;
                newColor.a = .46f;
                itemBox.color = newColor;
            }
            else
            {
                Image itemBox = gridListContent[i].GetComponent<Image>();
                Color newColor = itemBox.color;
                newColor.r = 1f;
                newColor.g = 1f;
                newColor.b = 1f;
                newColor.a = 0f;
                itemBox.color = newColor;
            }

            int cost = this.gameObject.GetComponent<ShopButtonController>().SetItemCost(i);
            gridListContent[i].transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = this.gameObject.GetComponent<ShopButtonController>().itemTitles[i];
            /*if (!maxedOut)
            {
               gridListContent[i].transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = this.gameObject.GetComponent<ShopButtonController>().itemTitles[i] + 
                    " [" + cost.ToString() + " COINS]";
            }
            else
            {
                gridListContent[i].transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = this.gameObject.GetComponent<ShopButtonController>().itemTitles[i];
            } */
            //Debug.Log(gridListContent[i]);
        }
    }
}
