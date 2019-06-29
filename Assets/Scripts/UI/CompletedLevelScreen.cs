using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompletedLevelScreen : SimpleMenu<CompletedLevelScreen>
{
    public static Action<string> OpenAndUpdate;

    public Button next;
    public Button home;
    public Button collection;
    public Image progress;
    public Text level;
    public Text prevLevel;
    public Text nextLevel;
    public Text speed;


    public override void Init()
    {
        base.Init();

        next.onClick.AddListener(delegate
        {
            if (GameManager.instance.data.level % 10 == 0)
            {
                FortuneWheelScreen.Open();
            }
            else
            {
                BlackScreen.instance.StarTransition(delegate
                {
                    SceneManager.LoadScene(0);
                    MainMenuScreen.Open();
                });
            }
        });
        home.onClick.AddListener(delegate
        {
            if (GameManager.instance.data.level % 10 == 0)
            {
                FortuneWheelScreen.Open();
            }
            else
            {
                BlackScreen.instance.StarTransition(delegate
                {
                    SceneManager.LoadScene(0);
                    MainMenuScreen.Open();
                });
            }
        });

        collection.onClick.AddListener(delegate
        {
            BlackScreen.instance.StarTransition(delegate
            {
                SceneManager.LoadScene(0);
                CarShopScreen.Open();
            });
        });

        OpenAndUpdate += OnOpen;
    }

    private void OnOpen(string speed)
    {
        Open();
        level.text = "Level " + GameManager.instance.data.level;
        StartCoroutine(FillProgess());
        this.speed.text = speed + " MPH";
        next.GetComponent<Animator>().Play("BounceUIEffect");
    }

    IEnumerator FillProgess()
    {
        float time = 0;
        int start = GameManager.instance.data.level - GameManager.instance.data.level % 10;
        int end = start + 10;

        float startFill = (GameManager.instance.data.level - 1) / (float)end;
        float targetFill = GameManager.instance.data.level / (float)end;

        prevLevel.text = start.ToString();
        nextLevel.text = end.ToString();

        while (time < 1)
        {
            time += Time.deltaTime;
            progress.fillAmount = Mathf.Lerp(startFill, targetFill, time);
            yield return null;
        }
        yield return null;
    }
}
