using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    Text coinText;

    void Start()
    {
        coinText = GetComponent<Text>();
        coinText.text = GameManager.instance.data.coins.ToString();
        GameManager.OnCoinValueChanged += OnCoinValueChanged;
    }

    private void OnCoinValueChanged(int value)
    {
        coinText.text = value.ToString();
    }

    void OnDestroy()
    {
        GameManager.OnCoinValueChanged -= OnCoinValueChanged;
    }
}
