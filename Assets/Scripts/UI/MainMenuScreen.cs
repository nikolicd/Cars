using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class MainMenuScreen : SimpleMenu<MainMenuScreen>
{
    public Button settings;
    public Button carShopButton;
    public Button shop;
    public HorizontalScrollSnap horizontalScroll;

    Animator animator;

    public override void Init()
    {
        base.Init();
        settings.onClick.AddListener(OnSettingsClick);
        animator = settings.GetComponent<Animator>();

        carShopButton.onClick.AddListener(delegate
        {
            horizontalScroll.StartingScreen = 0;
            horizontalScroll.JumpOnEnable = true;
            if (horizontalScroll._screensContainer != null)
            {
                horizontalScroll.ChangePage(0);
            }
            CarShopScreen.Open();
        });

        shop.onClick.AddListener(delegate
        {
            horizontalScroll.StartingScreen = 1;
            horizontalScroll.JumpOnEnable = true;
            horizontalScroll.ChangePage(1);
            CarShopScreen.Open();
        });
    }

    private void OnSettingsClick()
    {
        Settings.Open();
        Settings.isPaused = true;
    }

    public void OpenGameUI()
    {
        GameScreen.Open();
        GameManager.instance.Play();
    }
}
