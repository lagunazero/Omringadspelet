using UnityEngine;
using System.Collections;

public class VisibilityChecker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (renderer.isVisible)
		{
			OnBecameVisible();
		}
		else
		{
			OnBecameInvisible();
		}
	}
	
	void OnBecameVisible()
	{
		transform.parent.SendMessage("ChildBecameVisible", SendMessageOptions.DontRequireReceiver);
	}

	void OnBecameInvisible()
	{
		transform.parent.SendMessage("ChildBecameInvisible", SendMessageOptions.DontRequireReceiver);
	}
}
