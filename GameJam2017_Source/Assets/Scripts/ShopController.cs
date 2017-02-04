using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum MenuState { Main, Sell, Craft }

public class ShopController : MonoBehaviour
{
    public MenuState menuState;
    public GameObject shopMenu;
    public GameObject sellMenu;
    public GameObject craftMenu;

    public GameObject startingButton;

    void Start()
    {
        SetShop();
    }


    void Update()
    {
        if (Input.GetButtonDown("ButtonB"))
        {
            if (menuState == MenuState.Craft)
            {
                SetShop();
            }

            if (menuState == MenuState.Sell)
            {
                SetShop();
            }
        }
    }

    void SetShop()
    {
        sellMenu.SetActive(false);
        craftMenu.SetActive(false);
        shopMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(startingButton, new BaseEventData(EventSystem.current));
        menuState = MenuState.Main;
    }

    public void SetCraft()
    {
        sellMenu.SetActive(false);
        craftMenu.SetActive(true);
        shopMenu.SetActive(false);
        menuState = MenuState.Craft;
    }

    public void SetSell()
    {
        sellMenu.SetActive(true);
        craftMenu.SetActive(false);
        shopMenu.SetActive(false);
        menuState = MenuState.Craft;
    }

}
