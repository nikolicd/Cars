using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CongratulationScreen : SimpleMenu<CongratulationScreen>
{
    public Button ok;
    public AccelerationUpgrade accelerationUpgrade;
    public SpeedUpgrade speedUpgrade;

    public override void Init()
    {
        base.Init();
        ok.onClick.AddListener(OnClick);
        OnMenuOpened.AddListener(OnMenuOpen);
    }

    private void OnMenuOpen()
    {
        accelerationUpgrade.level = GameManager.instance.data.accelerationLevel;
        speedUpgrade.level = GameManager.instance.data.speedLevel;
    }

    void OnClick()
    {
        Close();
    }
}
