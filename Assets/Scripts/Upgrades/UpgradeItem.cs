using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Base class for car upgrades
/// </summary>
public abstract class UpgradeItem : MonoBehaviour
{
    public Button upgrade;
    public Text valueText;
    public int[] levelValue;
    public Image[] levelImages;

    int upgradeLevel;

    /// <summary>
    /// Get or set a level of a car which will also update the UI
    /// </summary>
    public int level
    {
        get
        {
            return upgradeLevel;
        }
        set
        {
            upgradeLevel = value;
            valueText.text = price.ToString();

            foreach (var item in levelImages)
            {
                item.enabled = false;
            }

            for (int i = 0; i < value + 1; i++)
            {
                levelImages[i].enabled = true;
            }
        }
    }

    /// <summary>
    /// Get a price of the upgrade
    /// </summary>
    public int price
    {
        get
        {
            return levelValue[level];
        }
    }

    protected virtual void Start()
    {
        upgrade.onClick.AddListener(OnUpgradeButtonClick);
        level = 0;
        for (int i = 0; i < levelImages.Length; i++)
        {
            int p = Mathf.RoundToInt(10 * (i * 1.31f));
            levelValue[i] = p;
        }
    }

    /// <summary>
    /// Method called when users tries to purchase an upgrade
    /// </summary>
    void OnUpgradeButtonClick()
    {
        if (level < levelValue.Length && price <= GameManager.instance.data.coins)
        {
            GameManager.instance.data.coins -= price;
            level++;
            OnCarUpgraded();
        }
    }
    public abstract void OnCarUpgraded();
}
