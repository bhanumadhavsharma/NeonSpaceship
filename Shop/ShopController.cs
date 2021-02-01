using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private GameObject _mainLevelObject;
    [SerializeField] private GameObject _shopObject;

    private void Start()
    {
        _shopObject.SetActive(false);
    }

    private void Update()
    {
        //Fix0BulletBug();
    }

    public void OpenShop()
    {
        GameManager.instance.shopMenuOpened = true;
        _mainLevelObject.SetActive(false);
        _shopObject.SetActive(true);
    }

    public void BackOutOfShop()
    {
        GameManager.instance.shopMenuOpened = false;
        _mainLevelObject.SetActive(true);
        _shopObject.SetActive(false);
    }

    
}
