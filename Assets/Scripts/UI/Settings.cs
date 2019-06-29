using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : SimpleMenu<Settings>
{
    public static bool isPaused;
    public Toggle sound;
    public Button credits;
    public Button policy;
    public Button back;
    public Button hand;
    public Image handImage;
    public override void Init()
    {
        base.Init();
        sound.onValueChanged.AddListener(OnSoundValueChanged);
        credits.onClick.AddListener(OnCredits);
        policy.onClick.AddListener(OnPolicy);
        back.onClick.AddListener(OnBack);
        hand.onClick.AddListener(OnHandClick);

        if (!GameManager.instance.data.isRightHanded)
        {
            Vector3 scale = handImage.transform.localScale;
            scale.x *= -1;
            handImage.transform.localScale = scale;
        }
    }

    private void OnHandClick()
    {
        Vector3 scale = handImage.transform.localScale;
        scale.x *= -1;
        handImage.transform.localScale = scale;
        GameManager.instance.data.isRightHanded = !GameManager.instance.data.isRightHanded;
    }

    private void OnBack()
    {
        Close();
        Time.timeScale = 1;
        isPaused = false;
    }

    private void OnPolicy()
    {
        throw new NotImplementedException();
    }

    private void OnCredits()
    {
        throw new NotImplementedException();
    }

    private void OnSoundValueChanged(bool value)
    {
        SoundManager.instance.Mute(value);
    }
}
