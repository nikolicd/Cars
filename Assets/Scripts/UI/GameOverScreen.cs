using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : SimpleMenu<GameOverScreen>
{
    public static Action OpenAndUpdate;
    public Button video;
    public Text timer;
    public Text tapToContinue;
    public Button restart;

    public override void Init()
    {
        base.Init();
        video.onClick.AddListener(OnVideoButtonClick);
        restart.onClick.AddListener(OnTapToContinue);
        OpenAndUpdate += OnOpen;
    }

    private void OnOpen()
    {
        Open();
        StartCoroutine(Timer());
        EnableToContinue(false);
        video.GetComponent<Animator>().Play("BounceUIEffect");
    }

    IEnumerator Timer()
    {
        float time = 5;
        timer.text = "5";
        video.interactable = true;
        while ((int)time != 0)
        {
            time -= Time.deltaTime;
            timer.text = ((int)time).ToString();
            yield return null;
        }
        video.interactable = false;
        EnableToContinue(true);
    }

    void EnableToContinue(bool value)
    {
        restart.interactable = value;
        tapToContinue.enabled = value;
    }
    public void OnVideoButtonClick()
    {
        CarController.instance.ShowAndStartCar();
        GameScreen.Open();
    }

    public void OnTapToContinue()
    {
        GameManager.instance.data.speed = 0;
        GameManager.instance.data.health = 0;
        BlackScreen.instance.StarTransition(delegate
        {
            SceneManager.LoadScene(0);
            MainMenuScreen.Open();
        });
    }
}
