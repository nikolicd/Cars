using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    public int activeOnStartMenu;
    public Menu[] menus;
    Menu openMenu;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].Init();
            if (i == activeOnStartMenu)
            {
                menus[i].gameObject.SetActive(true);
            }
            else
            {
                menus[i].gameObject.SetActive(false);
            }
        }
        openMenu = menus[activeOnStartMenu];
    }

    public void Open(Menu menu)
    {
        if (menu.isPreviousMenuDisabled)
        {
            if (openMenu != menu)
            {
                openMenu.Hide();
                openMenu = menu;
            }
        }
    }
}
