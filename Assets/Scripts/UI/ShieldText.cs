using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldText : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
        text.text = GameManager.instance.data.shield.ToString();
        GameManager.OnShieldValueChanged += OnValueChanged;
    }

    private void OnValueChanged(int value)
    {
        text.text = value.ToString();
    }

    void OnDestroy()
    {
        GameManager.OnShieldValueChanged -= OnValueChanged;
    }
}
