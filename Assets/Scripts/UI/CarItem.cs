using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarItem : ShopItem
{
    public Sprite carSprite;
    public Sprite carShadowSprite;

    public bool selected
    {
        get
        {
            return PlayerPrefs.GetInt("selected" + id, 0) > 0;
        }
        set
        {
            PlayerPrefs.SetInt("selected" + id, value ? 1 : 0);
        }
    }
}
