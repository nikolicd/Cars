using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	/// <summary>
    /// Length of the camera shake
    /// </summary>
	public static float shakeDuration = 0f;

    /// <summary>
    ///  Amplitude of the shake. A larger value shakes the camera harder.
    /// </summary>
    public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	
	Vector3 originalPos;
	
	
	void Start()
	{
		originalPos = transform.localPosition;
	}

	void Update()
	{
		if (shakeDuration > 0)
		{
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			transform.localPosition = originalPos;
		}
	}
}
