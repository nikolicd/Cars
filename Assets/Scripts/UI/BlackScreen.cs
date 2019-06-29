using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fades in a black sreen until the level is loaded.
/// </summary>
public class BlackScreen : MonoBehaviour
{
    public static BlackScreen instance;
    public static Action OnScreenBlack;
    public CanvasGroup canvasGroup;
    public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        Hide();
    }

    public void StarTransition(Action action)
    {
        OnScreenBlack = null;
        OnScreenBlack += action;
        gameObject.SetActive(true);
        StartCoroutine(Transition());
    }
    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowCourutine());
    }

    public void Hide()
    {
        StartCoroutine(HideCourutine());
    }

    IEnumerator Transition()
    {
        float time = 0;

        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        while (time < 1)
        {
            time += Time.unscaledDeltaTime / 0.2f;
            canvasGroup.alpha = Mathf.Lerp(0, 1, curve.Evaluate(time));
            yield return null;
        }

        OnScreenBlack?.Invoke();
        yield return new WaitForSeconds(0.5f);
        Hide();
    }
    IEnumerator ShowCourutine()
    {
        float time = 0;

        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        while (time < 1)
        {
            time += Time.unscaledDeltaTime / 0.2f;
            canvasGroup.alpha = Mathf.Lerp(0, 1, curve.Evaluate(time));
            yield return null;
        }
    }

    IEnumerator HideCourutine()
    {
        float time = 0;

        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        while (time < 1)
        {
            time += Time.unscaledDeltaTime / 0.1f;
            canvasGroup.alpha = Mathf.Lerp(1, 0, curve.Evaluate(time));
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
