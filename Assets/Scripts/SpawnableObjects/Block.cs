using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Block obstacle object
/// </summary>
public class Block : MonoBehaviour, ISpawnable
{
    public GameObject hitEffect;
    public Text text;
    ObstacleController obstacleController;
    int healthValue;

    /// <summary>
    /// Get or set a color of a block
    /// </summary>
    public Color color
    {
        get
        {
            return GetComponentInChildren<SpriteRenderer>().color;
        }
        set
        {
            GetComponentInChildren<SpriteRenderer>().color = value;
        }
    }

    /// <summary>
    /// Get or set a value that hurts the car if it collides with it
    /// </summary>
    public int health
    {
        get
        {
            return healthValue;
        }
        set
        {
            healthValue = value;
            text.text = value.ToString();
        }
    }

    public void Init(ObstacleController obstacleController)
    {
        this.obstacleController = obstacleController;
    }

    /// <summary>
    /// Method gets called when a car hits a block
    /// </summary>
    void OnTriggerEnter2D(Collider2D collision)
    {
        iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
        collision.transform.parent.GetComponent<IHealth>().OnBlockHit(health);
        Instantiate(hitEffect).transform.position = transform.position;
        gameObject.SetActive(false);
    }
}
