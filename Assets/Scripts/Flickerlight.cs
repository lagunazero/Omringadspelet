using UnityEngine;
using System.Collections;

public class Flickerlight : MonoBehaviour
{
	public float flickerIterationsMin = 1;
	public float flickerIterationsMax = 15;
	
	public float flickerIntensityRange = 1;
	public float flickerIntensityAmount = 0.4f;
	
	public float flickerAngleRange;
	public float flickerAngleAmount;
	
	private float originalIntensity;
	private float originalAngle;
	
	// Use this for initialization
	void Start ()
	{
		originalIntensity = light.intensity;
		originalAngle = light.spotAngle;
		StartCoroutine(Flicker());
	}

	// Update is called once per frame
	IEnumerator Flicker()
	{
		while (true)
		{
			float startAngle = light.spotAngle;
			float goalAngle = Mathf.Clamp(
				light.spotAngle + Random.Range(-flickerAngleAmount, flickerAngleAmount),
				originalAngle - flickerAngleRange, originalAngle + flickerAngleRange);
			
			float startIntensity = light.intensity;
			float goalIntensity = Mathf.Clamp(
				light.intensity + Random.Range(-flickerIntensityAmount, flickerIntensityAmount),
				originalIntensity - flickerIntensityRange, originalIntensity + flickerIntensityRange);
			
			float speed = 1f / Random.Range(flickerIterationsMin, flickerIterationsMax);
			for(float t = 0; t < 1; t += speed)
			{
				light.spotAngle = Mathf.Lerp(startAngle, goalAngle, t);
				light.intensity = Mathf.Lerp(startIntensity, goalIntensity, t);
				yield return new WaitForEndOfFrame();
			}
		}
	}
}
