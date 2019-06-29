using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Menu controls to show and hide a certain menus 
/// </summary>
/// <typeparam name="T">Type of menu</typeparam>
[RequireComponent(typeof(CanvasGroup))]
public abstract class Menu<T> : Menu where T : Menu<T>
{
    public static T instance;
    public CanvasGroup canvasGroup;
    public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public override void Init()
    {
        if (instance == null)
        {
            instance = (T)this;
        }
        canvasGroup = GetComponent<CanvasGroup>();
        if(canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    /// <summary>
    /// Shows the menu with an animation
    /// </summary>
    public override void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowCourutine());
    }

    /// <summary>
    /// Hides a menu with an animation
    /// </summary>
    public override void Hide()
    {
        StartCoroutine(HideCourutine());
    }

    /// <summary>
    /// Show animation
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowCourutine()
    {
        float time = 0;

        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        while (time < 1)
        {
            time += Time.unscaledDeltaTime / 0.1f;
            canvasGroup.alpha = Mathf.Lerp(0, 1, curve.Evaluate(time));
            yield return null;
        }
    }

    /// <summary>
    /// Hide animation 
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Static method to open a curtain menu.
    /// </summary>
    public static void Open()
    {
        instance.Show();
        instance.OnMenuOpened.Invoke();
        MenuController.instance.Open(instance);
    }

    /// <summary>
    /// Static method to close a curtain menu.
    /// </summary>
    public static void Close()
    {
        instance.Hide();
        instance.OnMenuClosed.Invoke();
    }
}

public abstract class Menu : MonoBehaviour
{
    public bool isPreviousMenuDisabled = true;
    /// <summary>
    /// Called when the menu is open
    /// </summary>
    public UnityEvent OnMenuOpened;
    /// <summary>
    /// Called when the menu is close
    /// </summary>
    public UnityEvent OnMenuClosed;

    public abstract void Init();
    public abstract void Show();
    public abstract void Hide();

}
