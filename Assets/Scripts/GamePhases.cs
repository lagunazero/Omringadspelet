using UnityEngine;
using System.Collections;

public class GamePhases : MonoBehaviour {
	
	public GameObject hud;
	public GameObject walkers;
	public string[] texts;
	
	public AnimationClip fadeToPauseAnimation;
	public AnimationClip fadeFromPauseAnimation;
	
	public float minPausedTime = 2;
	
	public float textDisplaySpeed = 0.01f;
	
	private bool isPaused = true;
	private float pausedTime = 0;
	
	private int currentPhase = 0;
	
	void Start()
	{
		ScreenOverlay overlay = transform.GetComponent<ScreenOverlay>();
		overlay.intensity = -2;
		
		hud.guiText.text = texts[0];
		Pause(true);
	}
	
	void Update()
	{
		Screen.lockCursor = true;
		
		if (isPaused)
		{
			pausedTime += Time.deltaTime;
			if (pausedTime >= minPausedTime && Input.anyKeyDown && currentPhase < texts.Length - 1)
			{
				animation.clip = fadeFromPauseAnimation;
				animation.Play();
				Pause(false);
				StartCoroutine(HideText());
			}
		}
	}
	
	// Use this for initialization
	public void SetText (int phaseIndex)
	{
		if (phaseIndex >= texts.Length || phaseIndex < 0)
		{
			Debug.LogError("No text for phase index: " + phaseIndex);
			return;
		}
		currentPhase = phaseIndex;
		
		animation.clip = fadeToPauseAnimation;
		animation.Play();
		Pause(true);
	}
	
	private void Pause(bool in_pause)
	{
		isPaused = in_pause;
		foreach (Walker w in walkers.transform.GetComponentsInChildren<Walker>())
			w.TogglePhase(!in_pause);
		transform.GetComponent<CharacterMotor>().canControl = !in_pause;
		pausedTime = 0;
	}
	
	void FadedToPause()
	{
		StartCoroutine(DisplayText(texts[currentPhase]));
	}
	
	void FadedFromPause()
	{
	}
	
	IEnumerator DisplayText(string text)
	{
		hud.guiText.enabled = true;
		int i = 0;
		hud.guiText.text = "";
		while (hud.guiText.text.Length != text.Length)
		{
			hud.guiText.text += text[i];
			i++;
			yield return new WaitForSeconds(textDisplaySpeed);
		}
	}
	
	IEnumerator HideText()
	{
		while (hud.guiText.text.Length > 0)
		{
			char letter = hud.guiText.text[0];
			hud.guiText.text = hud.guiText.text.TrimStart(letter);
			yield return new WaitForSeconds(textDisplaySpeed);
		}
		hud.guiText.enabled = false;
	}
}
