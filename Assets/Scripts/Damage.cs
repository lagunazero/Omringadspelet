using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {
	
	float eyeIntensityInc = 1;
	
	void OnCollisionEnter(Collision other)
	{
		if (other.contacts[0].normal.y < 0.9f)
		{
			other.collider.SendMessage("Damage", transform, SendMessageOptions.DontRequireReceiver);
			
			foreach (Light l in transform.GetComponentsInChildren<Light>())
			{
				l.intensity = eyeIntensityInc;
			}
		}
	}
}
