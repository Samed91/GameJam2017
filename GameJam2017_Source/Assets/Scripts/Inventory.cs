using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<Resource> items = new List<Resource>();
    string myPath = "/Items";

    public void AddItem(Resource r)
    {
        GameObject temp = Resources.Load("Items/" + r.itemName) as GameObject;
        items.Add(temp.GetComponent<Resource>());
        Debug.Log("<color=purple>Added: " + r.itemName + " To Inventory</color>");
    }

    public void RemoveItem(Resource r)
    {
        items.Remove(r);
        Debug.Log("<color=red>Removed: " + r.itemName + " To Inventory</color>");
    }

}
