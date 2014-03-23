using UnityEngine;
using System.Collections;

public class GlowEyes : MonoBehaviour {
	
	public float timeToGlowMin = 10;
	public float timeToGlowMax = 200;
	public float timeToGlowRetry = 2;
	
	public float lightUpAmount = 1;
	
	public Transform body;
	
	// Use this for initialization
	public void BeginLighting ()
	{
		StartCoroutine(LightUp());
	}
	
	// Update is called once per frame
	IEnumerator LightUp ()
	{
		yield return new WaitForSeconds(Random.Range(timeToGlowMin, timeToGlowMax));
		
		while (body.renderer.IsVisibleFrom(Camera.main))
		{
			yield return new WaitForSeconds(timeToGlowRetry);
		}
		
		foreach (Light l in transform.GetComponentsInChildren<Light>())
		{
			l.intensity = lightUpAmount;
		}
	}
}
