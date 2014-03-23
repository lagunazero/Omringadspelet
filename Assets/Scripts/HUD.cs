using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	
	private Vector3 originalPosition;
	private Vector3 offset = new Vector3(0, 0, 0);
	
	public Vector3 drumOffset;
	public float offsetFadeSpeed = 10;
	
	private Vector3 lastRotation;
	public Vector3 turnEffect = new Vector3(0.01f, 0, 0);
	
	void Start()
	{
		originalPosition = transform.localPosition;
		
	}
	
	void Update()
	{
		offset -= offset * offsetFadeSpeed * Time.deltaTime;
		
		transform.localPosition = originalPosition + offset;
		
		float diff = lastRotation.y - transform.eulerAngles.y;
		if (diff != 0)
		{
			if (diff > 180) diff -= 360;
			else if (diff < -180) diff += 360;
			transform.localPosition += diff * turnEffect;
		}
		lastRotation = transform.eulerAngles;
	}
	
	public void OnDrum(float timeUntilNext)
	{
		offset += drumOffset;
	}
}
