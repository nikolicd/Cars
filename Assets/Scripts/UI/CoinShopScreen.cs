using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinShopScreen : SimpleMenu<CoinShopScreen>
{
    public Button addCoins;
    public Button back;
    public override void Init()
    {
        base.Init();
        addCoins.onClick.AddListener(delegate
        {
            GameManager.instance.data.coins += 100;
        });
        back.onClick.AddListener(delegate
        {
            Close();
        });
    }
}
