using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Acceleration upgrade. Check the base clas UpgradeItem for more details
/// </summary>
public class AccelerationUpgrade : UpgradeItem
{
    protected override void Start()
    {
        base.Start();
        level = GameManager.instance.data.accelerationLevel;
    }

    public override void OnCarUpgraded()
    {
        GameManager.instance.data.accelerationLevel = level;
        CongratulationScreen.Open();
    }
}
