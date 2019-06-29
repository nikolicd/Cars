using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NitrousText : MonoBehaviour
{
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
        text.text = GameManager.instance.data.nitrous.ToString();
        GameManager.OnNitrousValueChanged += OnValueChanged;
    }

    private void OnValueChanged(int value)
    {
        text.text = value.ToString();
    }

    void OnDestroy()
    {
        GameManager.OnNitrousValueChanged -= OnValueChanged;
    }
}
