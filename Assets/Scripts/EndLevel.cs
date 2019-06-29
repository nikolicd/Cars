using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndLevel : MonoBehaviour
{
    public GameObject[] fireworks;

    /// <summary>
    /// Collision detection when car hits the finish line
    /// </summary>
    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.parent.GetComponent<ILevel>().OnLevelCompleted();
        SoundManager.instance.Play("Completed");
        for (int i = 0; i < fireworks.Length; i++)
        {
            StartCoroutine(StartFirework(fireworks[i], i / 10.0f));
        }
    }

    /// <summary>
    /// Firework animation
    /// </summary>
    /// <param name="firework">Firework object that you want to activate</param>
    /// <param name="time">Delay of the animation in seconds</param>
    IEnumerator StartFirework(GameObject firework, float time)
    {
        yield return new WaitForSeconds(time);
        firework.SetActive(true);
    }
}
