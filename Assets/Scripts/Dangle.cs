using UnityEngine;
using System.Collections;

public class Dangle : MonoBehaviour {
	
	public float dangleSpeed = 10;
	public float dangleSpeedRandomness = 7;
	
	public float dangleAmount = 5;
	public float dangleAmountRandomness = 4;
	
	// Use this for initialization
	void Start ()
	{
		float x = Random.Range(dangleAmount - dangleAmountRandomness, dangleAmount + dangleAmountRandomness);
		transform.Rotate(Vector3.right, x);
		
		dangleSpeed += Random.Range(-dangleSpeedRandomness, dangleSpeedRandomness);
		if(Random.Range(0, 1) == 1)
		{
			dangleSpeed = -dangleSpeed;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate(0, dangleSpeed * Time.deltaTime, 0, Space.World);
	}
}
