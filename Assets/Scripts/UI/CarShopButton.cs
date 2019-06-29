using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarShopButton : MonoBehaviour
{
    public Image carImage;
    public Image lockImage;
    public Text price;

    CarItem carInfo;
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (carInfo.isLocked)
        {
            if (carInfo.price <= GameManager.instance.data.coins)
            {
                carImage.sprite = carInfo.carSprite;
                carInfo.isLocked = false;
                GameManager.instance.data.coins -= carInfo.price;
                price.transform.parent.gameObject.SetActive(false);
                lockImage.enabled = false;
                SoundManager.instance.Play("CarPurchase");
            }
            else
            {
                Debug.Log("Not enough coins");
            }
        }
        else
        {
            CarController.instance.SetSkin(carInfo.carSprite);
            iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
            MainMenuScreen.Open();
        }
    }

    public void Init(CarItem carItem)
    {
        this.carInfo = carItem;
        if (carItem.isLocked)
        {
            carImage.sprite = carItem.carShadowSprite;
            price.text = carItem.price.ToString();
            price.transform.parent.gameObject.SetActive(true);
            lockImage.enabled = true;
        }
        else
        {
            carImage.sprite = carItem.carSprite;
            price.text = carItem.price.ToString();
            price.transform.parent.gameObject.SetActive(false);
            lockImage.enabled = false;
        }
    }
}
