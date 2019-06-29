using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ShopItem
{
    public string id;
    public int price = 100;
    public bool isLocked = true;
}
