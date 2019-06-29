using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : SimpleMenu<GameScreen>
{
    public Button settings;
    public Text speedText;
    public Image speedometerImage;
    public Image progress;
    public Text currentLevel;
    public Text nextLevel;
    public RectTransform boostHolder;
    public RectTransform middleBoost;

    public Button nitrous;
    public Button magnet;
    public Button shield;

    public override void Init()
    {
        base.Init();
        nitrous.onClick.AddListener(delegate
        {
            CarController.instance.ActivateNitrous();
            GameManager.instance.data.nitrous--;
            iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
            ButtonsInteractable(false);
        });

        magnet.onClick.AddListener(delegate
        {
            CarController.instance.ActivateAtraction();
            GameManager.instance.data.magnet--;
            iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
            ButtonsInteractable(false);
        });

        shield.onClick.AddListener(delegate
        {
            CarController.instance.ActivateShield();
            GameManager.instance.data.shield--;
            iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
            ButtonsInteractable(false);
        });

        settings.onClick.AddListener(delegate
        {
            Settings.Open();
            Settings.isPaused = true;
            Time.timeScale = 0;
        });

        OnMenuOpened.AddListener(OnMenuOpen);
    }

    private void OnMenuOpen()
    {
        if (GameManager.instance.data.isRightHanded)
        {
            boostHolder.pivot = new Vector2(1, 0.5f);
            boostHolder.anchorMin = boostHolder.anchorMax = new Vector2(1, 0.5f);
            boostHolder.anchoredPosition = new Vector2(0, 0);
        }
        else
        {
            boostHolder.pivot = new Vector2(0, 0.5f);
            boostHolder.anchorMin = boostHolder.anchorMax = new Vector2(0, 0.5f);
            boostHolder.anchoredPosition = new Vector2(0, 0);
        }
        middleBoost.anchoredPosition = GameManager.instance.data.isRightHanded ? new Vector2(-131.8f, middleBoost.anchoredPosition.y): new Vector2(131.8f, middleBoost.anchoredPosition.y);
    }

    public void ButtonsInteractable(bool value)
    {
        nitrous.interactable = magnet.interactable = shield.interactable = value;
    }
}
