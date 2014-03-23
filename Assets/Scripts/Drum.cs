using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Drum : MonoBehaviour {
	
	public List<List<float> > frequency;
	
	public List<Transform> listenerCollections;
	private List<Beatlight> listeners;
	public HUD hud;
	
	private int frequencyIndex = 0;
	private int innerIndex = 0;
	private float timer = 0;
	
	void Start()
	{
		listeners = new List<Beatlight>();
		foreach (Transform t in listenerCollections)
		{
			foreach (Beatlight beat in t.GetComponentsInChildren(typeof(Beatlight)))
			{
				listeners.Add(beat);
			}
		}
		
		frequency = new List<List<float> >();
		frequency.Add(new List<float>(){3.0f});
		frequency.Add(new List<float>(){0.3f, 2.0f});
		frequency.Add(new List<float>(){0.2f, 0.4f});
		frequency.Add(new List<float>(){0.15f, 0.15f, 0.4f, 0.15f, 0.15f, 0.4f, 0.15f, 0.15f, 0.15f, 0.15f, 0.6f});
		frequency.Add(new List<float>(){0.15f, 0.2f});
	}
	
	void Update ()
	{
		timer += Time.deltaTime;
		
		if (timer >= frequency[frequencyIndex][innerIndex])
		{
			timer -= frequency[frequencyIndex][innerIndex];
			audio.Play();
			innerIndex = (innerIndex >= frequency[frequencyIndex].Count - 1) ? 0 : innerIndex + 1;
			foreach (Beatlight t in listeners)
			{
				t.OnDrum(frequency[frequencyIndex][innerIndex]);
			}
			hud.OnDrum(frequency[frequencyIndex][innerIndex]);
		}
	}
	
	public void SetHitsTaken(int hits)
	{
		if (hits < 0 || hits >= frequency.Count)
		{
			Debug.LogError("Attempted to set frequency #" + hits + " but only " + frequency.Count + " frequencies registered.");
			return;
		}
		
		frequencyIndex = hits;
		innerIndex = 0;
		timer = 0;
	}
}
