using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftingHUD : MonoBehaviour
{
    public Transform listParent;
    public GameObject itemButton;
    public List<GameObject> itemListButtons = new List<GameObject>();
    public RectTransform scrollRectTransform;
    public RectTransform contentPanel;
    RectTransform selectedRectTransform;
    GameObject lastSelected;


    void Start()
    {
        if (FindObjectOfType<Inventory>().items.Count > 0)
        {
            foreach (Item i in FindObjectOfType<Inventory>().items)
            {
                GameObject go = Instantiate(itemButton, transform.position, Quaternion.identity) as GameObject;
                go.transform.parent = listParent;
                go.transform.FindChild("Text").GetComponent<Text>().text = i.itemName;
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

}
