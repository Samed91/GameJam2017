using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class ItemHud
{
    public List<Item> items = new List<Item>();
}

public class CraftingHUD : MonoBehaviour
{
    public Transform listParent;
    public Transform craftParent;
    public GameObject itemButton;
    public List<ItemHud> itemHud = new List<ItemHud>();
    public List<GameObject> itemListButtons = new List<GameObject>();
    public List<GameObject> craftListItems = new List<GameObject>();
    public RectTransform scrollRectTransform;
    public RectTransform contentPanel;
    RectTransform selectedRectTransform;
    GameObject lastSelected;
    public CanvasGroup craftButton;
    public Image potion;
    public Text potionName;
    public GameObject craftedPosition;


    void Start()
    {
        RefreshList();
    }


    void RefreshList()
    {
        foreach (GameObject go in itemListButtons)
        {
            Destroy(go);
        }
        itemListButtons.Clear();
        itemHud.Clear();
        if (FindObjectOfType<Inventory>().items.Count > 0)
        {
            for (int i = 0; i < FindObjectOfType<Inventory>().items.Count; i++)
            {
                bool hasItem = false;
                if (itemHud.Count > 0)
                {
                    for (int j = 0; j < itemHud.Count; j++)
                    {
                        if (FindObjectOfType<Inventory>().items[i].itemName == itemHud[j].items[0].itemName)
                        {
                            hasItem = true;
                            itemHud[j].items.Add(FindObjectOfType<Inventory>().items[i]);
                        }
                    }
                }
                if (!hasItem)
                {
                    ItemHud temp = new ItemHud();
                    temp.items.Add(FindObjectOfType<Inventory>().items[i]);
                    itemHud.Add(temp);
                }
            }

            foreach (ItemHud hudItem in itemHud)
            {
                GameObject go = Instantiate(itemButton, transform.position, Quaternion.identity) as GameObject;
                go.transform.parent = listParent;
                go.transform.FindChild("Value").GetComponent<Text>().text = "X " + hudItem.items.Count;
                go.transform.localScale = new Vector3(1, 1, 1);
                GameObject temp = Resources.Load("Items/" + hudItem.items[0].itemName) as GameObject;
                Resource r = temp.GetComponent<Resource>();
                go.AddComponent(typeof(Resource));
                go.GetComponent<Resource>().icon = r.icon;
                go.GetComponent<Resource>().itemData = r.itemData;
                go.GetComponent<Image>().sprite = r.icon;
                itemListButtons.Add(go);
            }

            EventSystem.current.SetSelectedGameObject(itemListButtons[0], new BaseEventData(EventSystem.current));
        }
    }

    void OnEnable()
    {
        if (itemListButtons.Count > 0)
            EventSystem.current.SetSelectedGameObject(itemListButtons[0], new BaseEventData(EventSystem.current));
    }

    void Update()
    {
        UpdateInput();
        UpdatePrompt();
    }

    void UpdateInput()
    {
        if (Input.GetButtonDown("ButtonA"))
        {
            if (!craftedPosition.activeInHierarchy)
            {
                if (craftListItems.Count < 4)
                {
                    GameObject ingredient = Instantiate(EventSystem.current.currentSelectedGameObject, craftParent.transform.position, Quaternion.identity) as GameObject;
                    ingredient.transform.parent = craftParent;
                    ingredient.transform.localScale = new Vector3(1, 1, 1);
                    ingredient.transform.FindChild("Value").GetComponent<Text>().text = "";
                    craftListItems.Add(ingredient);
                    FindObjectOfType<Inventory>().RemoveItem(EventSystem.current.currentSelectedGameObject.GetComponent<Resource>().itemData);
                    RefreshList();
                }
            }
            else
            {
                craftedPosition.SetActive(false);
            }
        }

        if (Input.GetButtonDown("ButtonY"))
        {
            if (craftListItems.Count > 0)
            {
                Craft();
            }
        }

    }

    void Craft()
    {
        List<Item> currentIngredients = new List<Item>();
        foreach (GameObject go in craftListItems)
        {
            currentIngredients.Add(go.GetComponent<Resource>().itemData);
        }
        Potion p = FindObjectOfType<RecipeBook>().GetCraftedPotion(currentIngredients);
        Debug.Log("<color=purple>Potion: " + p.name + "</color>");
        potion.sprite = p.icon;
        potionName.text = p.name;
        craftedPosition.SetActive(true);
        foreach (GameObject go in craftListItems)
        {
            Destroy(go);
        }
        craftListItems.Clear();
    }

    void UpdatePrompt()
    {
        if (craftListItems.Count > 0)
            craftButton.alpha += 1 * Time.deltaTime;
        else
            craftButton.alpha -= 1 * Time.deltaTime;

    }


}
