using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostShopButton : MonoBehaviour
{
    public BoostType boostType;
    public int numberBoosts = 1;
    public int value = 100;

    public Text valueText;
    public Button button;

    void Start()
    {
        button.onClick.AddListener(OnClick);
        valueText.text = value.ToString();
    }

    void OnClick()
    {
        if (GameManager.instance.data.coins >= value)
        {
            GameManager.instance.data.coins -= value;

            switch (boostType)
            {
                case BoostType.Nitrous:
                    GameManager.instance.data.nitrous += numberBoosts;
                    break;
                case BoostType.Magnet:
                    GameManager.instance.data.magnet += numberBoosts;
                    break;
                case BoostType.Shield:
                    GameManager.instance.data.shield += numberBoosts;
                    break;
            }
        }
    }
}

public enum BoostType
{
    Nitrous,
    Shield,
    Magnet
}
