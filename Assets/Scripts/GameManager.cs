using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles data and starts the level
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Action<int> OnCoinValueChanged;
    public static Action<int> OnMagnetValueChanged;
    public static Action<int> OnShieldValueChanged;
    public static Action<int> OnNitrousValueChanged;
    public Data data;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void Play()
    {
        CarController.instance.StartCar();
    }
}

[System.Serializable]
public class Data
{
    public int level;
    public float speed;
    public int accelerationLevel;
    public int speedLevel;
    public float health;
    public bool isRightHanded = true;


    string path;
    [SerializeField]
    int numberOfCoins;
    int numberOfNitrous;
    int numberOfShields;
    int numberOfMagnets;
    public int coins
    {
        get
        {
            return numberOfCoins;
        }
        set
        {
            GameManager.OnCoinValueChanged?.Invoke(value);
            numberOfCoins = value;
        }
    }

    public int shield
    {
        get
        {
            return numberOfShields;
        }
        set
        {
            GameManager.OnShieldValueChanged?.Invoke(value);
            numberOfShields = value;
        }
    }

    public int nitrous
    {
        get
        {
            return numberOfNitrous;
        }
        set
        {
            GameManager.OnNitrousValueChanged?.Invoke(value);
            numberOfNitrous = value;
        }
    }

    public int magnet
    {
        get
        {
            return numberOfMagnets;
        }
        set
        {
            GameManager.OnMagnetValueChanged?.Invoke(value);
            numberOfMagnets = value;
        }
    }



    public Data Load()
    {
        return new Data();
    }

    public void Save()
    {
    }
}