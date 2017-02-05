using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StockItem
{
    public string stockName = "";
    public int stockID = 0;
}

public class Potion : MonoBehaviour
{
    public StockItem stockData;
    public Sprite icon;
}
