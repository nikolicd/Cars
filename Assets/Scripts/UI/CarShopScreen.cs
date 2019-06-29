using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class CarShopScreen : SimpleMenu<CarShopScreen>
{
    public CarShopButton carShopButton;
    public Button coinShop;
    public Button backButton;

    [Header("Coins")]
    public Button gold50;
    public Button gold100;
    public Button gold300;
    public Button gold500;
    public Button gold750;
    public Button gold1000;

    [Header("Boosts")]
    public Button nitros1;
    public Button nitros3;
    public Button nitros5;

    public Button shield1;
    public Button shield3;
    public Button shield5;

    public Button magnet1;
    public Button magnet3;
    public Button magnet5;

    public int selectedCar;
    public CarItem[] carItems;
    public Transform buttonHolder;

    public override void Init()
    {
        base.Init();

        backButton.onClick.AddListener(delegate
        {
            MainMenuScreen.Open();
        });

        for (int i = 0; i < carItems.Length; i++)
        {
            var item = carItems[i];
            item.id = "c_" + i;

            carShopButton = Instantiate(this.carShopButton);
            carShopButton.transform.parent = buttonHolder;
            carShopButton.transform.localScale = Vector3.one;
            carShopButton.Init(item);
        }

        gold50.GetComponent<IAPButton>().onPurchaseComplete.AddListener(delegate
        {
            GameManager.instance.data.coins += 999;
        });
        gold100.GetComponent<IAPButton>().onPurchaseComplete.AddListener(delegate
        {
            GameManager.instance.data.coins += 1999;
        });
        gold300.GetComponent<IAPButton>().onPurchaseComplete.AddListener(delegate
        {
            GameManager.instance.data.coins += 4999;
        });
        gold500.GetComponent<IAPButton>().onPurchaseComplete.AddListener(delegate
        {
            GameManager.instance.data.coins += 6999;
        });
        gold750.GetComponent<IAPButton>().onPurchaseComplete.AddListener(delegate
        {
            GameManager.instance.data.coins += 9999;
        });
        gold1000.GetComponent<IAPButton>().onPurchaseComplete.AddListener(delegate
        {
            GameManager.instance.data.coins += 29999;
        });

    }
}