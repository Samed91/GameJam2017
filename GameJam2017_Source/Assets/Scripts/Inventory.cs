using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<Item> items = new List<Item>();
    public List<Item> oneOfEach = new List<Item>();
    string myPath = "/Items";

    public void AddItem(Item r)
    {
        GameObject temp = Resources.Load("Items/" + r.itemName) as GameObject;
        if (!CanAddToInventory(temp.GetComponent<Resource>().itemData))
            return;
        items.Add(temp.GetComponent<Resource>().itemData);
        Debug.Log("<color=purple>Added: " + r.itemName + " To Inventory</color>");
    }

    public void RemoveItem(Item r)
    {
        items.Remove(r);
        Debug.Log("<color=red>Removed: " + r.itemName + " To Inventory</color>");
    }

    public void RefreshOneOfEach()
    {
        //oneOfEach.Clear();
        for (int i = 0; i < items.Count; ++i)
        {
            bool hasItem = false;
            for (int j = 0; j < oneOfEach.Count; j++)
            {
                if (oneOfEach[j].itemName == items[i].itemName)
                {
                    hasItem = true;
                    break;
                }
            }
            if (!hasItem)
                oneOfEach.Add(items[i]);
        }
    }

    public bool CanAddToInventory(Item current)
    {
        bool returnValue = false;
        //oneOfEach.Clear();
        bool hasCurrent = false;
        for (int j = 0; j < oneOfEach.Count; j++)
        {
            if (oneOfEach[j].itemName == current.itemName)
                hasCurrent = true;
        }

        if (hasCurrent)
            return true;

        if (oneOfEach.Count < 23)
        {
            if (!hasCurrent)
            {
                RefreshOneOfEach();
                returnValue = true;
            }
        }
        else
        {
            Debug.Log("<color=red>INVENTORY FULL</color>");
            returnValue = false;
        }

        return returnValue;

    }

}
