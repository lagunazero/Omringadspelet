using UnityEngine;
using System.Collections;

public class Beatlight : MonoBehaviour {
	
	private float baseAngle;
	private float baseIntensity;

	private float dimSpeedAngle = 0;
	private float dimSpeedIntensity = 0;
	
	public float onBeatAngle = 5;
	public float onBeatIntensity = 2;
	
	void Start()
	{
		baseAngle = light.spotAngle;
		baseIntensity = light.intensity;
		
		StartCoroutine(Dim());
	}
	
	public void OnDrum(float timeUntilNext)
	{
		light.spotAngle = baseAngle + onBeatAngle;
		light.intensity = baseIntensity + onBeatIntensity;
		
		dimSpeedAngle = (light.spotAngle - baseAngle) / timeUntilNext;
		dimSpeedIntensity = (light.intensity - baseIntensity) / timeUntilNext;
	}
	
	IEnumerator Dim()
	{
		while (true)
		{
			light.spotAngle = Mathf.Max(0, light.spotAngle - dimSpeedAngle * Time.deltaTime);
			light.intensity = Mathf.Max(0, light.intensity - dimSpeedIntensity * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
	}
}
