using UnityEngine;
using System.Collections;

public class Walker : MonoBehaviour {
	
	public Transform player;
	public float speed = 1f;
	public float turnSpeed = 0.5f;
	public float findFreqMin = 1;
	public float findFreqMax = 4;
	
	public float overlapDistance = 2.5f;
	public Vector3 goalPosition;
	
	private bool isMoving = true;
	private bool isOutsideCamera = false;
	
	private Vector3 startPosition;
	
	// Use this for initialization
	void Start ()
	{
		startPosition = transform.position;
	}
	
	public void TogglePhase(bool start)
	{
		isMoving = start;
		if (start)
		{
			transform.position = startPosition;
			transform.LookAt(player);
			StartCoroutine(FoundYou());
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		bool sleep = true;
		if (isMoving)
		{
			Vector3 dist = player.position - transform.position;
			if (isOutsideCamera || dist.magnitude <= overlapDistance)
			{
				sleep = false;
				if (rigidbody.IsSleeping())
				{
					rigidbody.WakeUp();
				}
				rigidbody.velocity = transform.forward * speed;
				//transform.position = Vector3.MoveTowards(transform.position, goalPosition, speed * Time.deltaTime);
			}
		}
		
		if (sleep)
		{
			rigidbody.Sleep();
		}
	}
	
	IEnumerator FoundYou()
	{
		while (isMoving)
		{
			goalPosition = player.position;
			
			Vector3 direction = player.position - transform.position;
			if (direction.magnitude > 0)
			{
				Quaternion startRot = transform.rotation;
				Quaternion endRot = Quaternion.LookRotation(direction);
	
				for (float t = 0; t <= 1; t += turnSpeed * Time.deltaTime)
				{
	 				transform.rotation = Quaternion.Slerp(startRot, endRot, t);
					yield return new WaitForEndOfFrame();
				}
			}
			yield return new WaitForSeconds(Random.Range(findFreqMin, findFreqMax));
		}
	}
	
	void ChildBecameVisible()
	{
		isOutsideCamera = false;
	}
	
	void ChildBecameInvisible()
	{
		isOutsideCamera = true;
	}
}
