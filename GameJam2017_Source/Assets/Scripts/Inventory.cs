using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<Item> items = new List<Item>();
    string myPath = "/Items";

    public void AddItem(Item r)
    {
        GameObject temp = Resources.Load("Items/" + r.itemName) as GameObject;
        items.Add(temp.GetComponent<Resource>().itemData);
        Debug.Log("<color=purple>Added: " + r.itemName + " To Inventory</color>");
    }

    public void RemoveItem(Item r)
    {
        items.Remove(r);
        Debug.Log("<color=red>Removed: " + r.itemName + " To Inventory</color>");
    }

}
