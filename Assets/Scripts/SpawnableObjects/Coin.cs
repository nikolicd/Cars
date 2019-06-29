using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Object that gives you coins
/// </summary>
public class Coin : MonoBehaviour, ISpawnable
{
    public GameObject hitEffect;
    ObstacleController obstacleController;

    public void Init(ObstacleController obstacleController)
    {
        this.obstacleController = obstacleController;
    }

    /// <summary>
    /// Method gets called when a car hits a coin
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.parent.GetComponent<ICoin>().OnCoinHit();
        if (hitEffect)
        {
            Instantiate(hitEffect).transform.position = transform.position;
        }
        StartCoroutine(TravelToDestiantion(collision.transform));
    }

    /// <summary>
    /// Animation that flyes the coin to the target
    /// </summary>
    /// <param name="target">Destination object</param>
    IEnumerator TravelToDestiantion(Transform target)
    {
        float time = 0;
        Vector2 start = transform.position;

        while (time < 1)
        {
            time += Time.deltaTime / 0.2f;
            transform.position = Vector2.Lerp(start, target.position, time);
            yield return null;
        }
        SoundManager.instance.Play("Coin");
        gameObject.SetActive(false);
    }
}
