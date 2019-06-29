using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RewardScreen : SimpleMenu<RewardScreen>
{
    public static Action<FortuneWheel.RewardType> OpenRewardAction;
    public CarShopScreen carShopScreen;
    public Button collect;
    public Image rewardImage;
    public Sprite magnet;
    public Sprite nitrous;
    public Sprite shield;
    public Sprite coin;
    public Sprite car;

    public override void Init()
    {
        base.Init();
        OpenRewardAction += OnOpenReward;
        collect.onClick.AddListener(OnClamClick);
    }

    private void OnClamClick()
    {
        Close();
        BlackScreen.instance.StarTransition(delegate
        {
            SceneManager.LoadScene(0);
            MainMenuScreen.Open();
        });
    }

    private void OnOpenReward(FortuneWheel.RewardType reward)
    {
        Open();
        switch (reward)
        {
            case FortuneWheel.RewardType.Attraction1:
                GameManager.instance.data.magnet += 1;
                rewardImage.sprite = magnet;
                rewardImage.SetNativeSize();
                break;
            case FortuneWheel.RewardType.Attraction2:
                GameManager.instance.data.magnet += 2;
                rewardImage.sprite = magnet;
                rewardImage.SetNativeSize();
                break;
            case FortuneWheel.RewardType.Attraction3:
                GameManager.instance.data.magnet += 3;
                rewardImage.sprite = magnet;
                rewardImage.SetNativeSize();
                break;
            case FortuneWheel.RewardType.Gold50:
                GameManager.instance.data.coins += 50;
                rewardImage.sprite = coin;
                rewardImage.SetNativeSize();
                break;
            case FortuneWheel.RewardType.Gold100:
                GameManager.instance.data.coins += 100;
                rewardImage.sprite = coin;
                rewardImage.SetNativeSize();
                break;
            case FortuneWheel.RewardType.Gold300:
                GameManager.instance.data.coins += 300;
                rewardImage.sprite = coin;
                rewardImage.SetNativeSize();
                break;
            case FortuneWheel.RewardType.Gold500:
                GameManager.instance.data.coins += 500;
                rewardImage.sprite = coin;
                rewardImage.SetNativeSize();
                break;
            case FortuneWheel.RewardType.Gold1000:
                GameManager.instance.data.coins += 1000;
                rewardImage.sprite = coin;
                rewardImage.SetNativeSize();
                break;
            case FortuneWheel.RewardType.CarSkin:
                List<CarItem> carItems = new List<CarItem>();
                foreach (var item in carShopScreen.carItems)
                {
                    if (item.isLocked)
                    {
                        carItems.Add(item);
                    }
                }
                int carIndex = UnityEngine.Random.Range(0, carItems.Count);
                rewardImage.sprite = carItems[carIndex].carSprite;
                carItems[carIndex].isLocked = false;
                rewardImage.SetNativeSize();
                RectTransform rectTransform = rewardImage.GetComponent<RectTransform>();
                rectTransform.sizeDelta *= 4;
                break;
            case FortuneWheel.RewardType.Nitrous1:
                GameManager.instance.data.nitrous += 1;
                rewardImage.sprite = nitrous;
                rewardImage.SetNativeSize();
                break;
            case FortuneWheel.RewardType.Nitrous2:
                GameManager.instance.data.nitrous += 2;
                rewardImage.sprite = nitrous;
                rewardImage.SetNativeSize();
                break;
            case FortuneWheel.RewardType.Nitrous3:
                GameManager.instance.data.nitrous += 3;
                rewardImage.sprite = nitrous;
                rewardImage.SetNativeSize();
                break;
            case FortuneWheel.RewardType.Shield1:
                GameManager.instance.data.shield += 1;
                rewardImage.sprite = shield;
                rewardImage.SetNativeSize();
                break;
            case FortuneWheel.RewardType.Shield2:
                GameManager.instance.data.shield += 2;
                rewardImage.sprite = shield;
                rewardImage.SetNativeSize();
                break;
            case FortuneWheel.RewardType.Shield3:
                GameManager.instance.data.shield += 3;
                rewardImage.sprite = shield;
                rewardImage.SetNativeSize();
                break;

        }

    }
}
