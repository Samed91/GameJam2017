using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stock : MonoBehaviour
{
    public List<StockItem> items = new List<StockItem>();
    string myPath = "/Stock";

    public void AddItem(StockItem r)
    {
        GameObject temp = Resources.Load("Stock/" + r.stockName) as GameObject;
        items.Add(temp.GetComponent<Potion>().stockData);
        Debug.Log("<color=purple>Added: " + r.stockName + " To Inventory</color>");
    }

}
