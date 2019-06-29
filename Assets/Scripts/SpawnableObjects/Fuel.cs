using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Object that increases health and speed
/// </summary>
public class Fuel : MonoBehaviour, ISpawnable
{
    public GameObject hitEffect;
    public Text text;

    int fuelValue;

    /// <summary>
    /// Get or set a fuel value which also updates the UI
    /// </summary>
    public int fuel
    {
        get
        {
            return fuelValue;
        }
        set
        {
            fuelValue = value;
            text.text = value.ToString();
        }
    }

    /// <summary>
    /// Method gets called when a car hits a fuel
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.parent.GetComponent<IFuel>().OnFuelHit(fuel);
        if (hitEffect)
        {
            Instantiate(hitEffect).transform.position = transform.position;
        }
        StartCoroutine(TravelToDestiantion(collision.transform));
        
    }

    /// <summary>
    /// Animation that flyes the fuel to the target
    /// </summary>
    /// <param name="target">Destiantion object</param>
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
        SoundManager.instance.Play("Fuel");
        gameObject.SetActive(false);
    }
}
