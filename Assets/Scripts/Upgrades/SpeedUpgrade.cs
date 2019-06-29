using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Speed upgrade. Check the base clas UpgradeItem for more details
/// </summary>
public class SpeedUpgrade : UpgradeItem
{
    protected override void Start()
    {
        base.Start();
        level = GameManager.instance.data.speedLevel;
    }

    private void OnEnable()
    {
        level = GameManager.instance.data.speedLevel;
    }

    public override void OnCarUpgraded()
    {
        GameManager.instance.data.speedLevel = level;
        CongratulationScreen.Open();
    }
}
