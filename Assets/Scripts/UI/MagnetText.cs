using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetText : MonoBehaviour
{
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
        text.text = GameManager.instance.data.magnet.ToString();
        GameManager.OnMagnetValueChanged += OnValueChanged;
    }

    private void OnValueChanged(int value)
    {
        text.text = value.ToString();
    }

    void OnDestroy()
    {
        GameManager.OnMagnetValueChanged -= OnValueChanged;
    }
}
