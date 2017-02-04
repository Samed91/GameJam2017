using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName = "";
    public int itemID = 0;
}

public class Resource : MonoBehaviour
{
    public Item itemData;
}
