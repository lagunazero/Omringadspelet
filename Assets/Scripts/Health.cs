using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int hitsTolerated = 5;
	private int hitsToleratedOrig;
	
	public float hitTime = 2;
	private float hitTimer = 0;
	
	public float knockbackForce = 10;
	public float knockbackDuration = 1;
	
	public float stretchEffectLimitMinMod = 0.8f;
	public float stretchEffectLimitMaxMod = 0f;
	
	public float glowIntensityEffect = 3;
	
	private CharacterController controller;
	private ContrastStretchEffect stretchEffect;
	private GlowEffect glowEffect;
	public GameObject walkersList;
	
	void Start()
	{
		hitsToleratedOrig = hitsTolerated;
		stretchEffectLimitMinMod = stretchEffectLimitMinMod / hitsTolerated;
		controller = GetComponent<CharacterController>();
		stretchEffect = GetComponent<ContrastStretchEffect>();
		glowEffect = GetComponent<GlowEffect>();
	}
	
	void Update()
	{
		hitTimer += Time.deltaTime;
		
		if (Input.GetKeyDown(KeyCode.Keypad5))
			hitsTolerated = 5;
		else if (Input.GetKeyDown(KeyCode.Keypad4))
			hitsTolerated = 4;
		else if (Input.GetKeyDown(KeyCode.Keypad3))
			hitsTolerated = 3;
		else if (Input.GetKeyDown(KeyCode.Keypad2))
			hitsTolerated = 2;
		else if (Input.GetKeyDown(KeyCode.Keypad1))
			hitsTolerated = 1;
		else
			return;
		StartNextPhase();
	}
	
	// Update is called once per frame
	void Damage (Transform other)
	{
		if (hitsTolerated > 0 && hitTimer >= hitTime)
		{
			Vector3 dir = transform.position - other.position;
			if (dir.magnitude > 0)
			{
				hitTimer = 0;
				if (hitsTolerated == hitsToleratedOrig)
				{
					foreach (GlowEyes ge in walkersList.GetComponentsInChildren<GlowEyes>())
					{
						ge.BeginLighting();
					}
				}

				StartCoroutine(Knockback(dir.normalized));
				StartNextPhase();
			}
		}
	}
	
	IEnumerator Knockback(Vector3 dir)
	{
		stretchEffect.limitMinimum += stretchEffectLimitMinMod;
		stretchEffect.limitMaximum += stretchEffectLimitMaxMod;
		glowEffect.glowIntensity = glowIntensityEffect;

		float force = knockbackForce;
		while (force > 0)
		{
			//Push player away
			controller.SimpleMove(dir * force);
			
			//Add visual effects
			glowEffect.glowIntensity = Mathf.Max(0,
				glowEffect.glowIntensity - (glowIntensityEffect / knockbackDuration) * Time.deltaTime);

			//Countdown force/timer, of course
			force -= knockbackForce * knockbackDuration * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}
	
	void StartNextPhase()
	{
		hitsTolerated--;
		if (hitsTolerated == 0)
		{
			GamePhases gp = transform.GetComponent<GamePhases>();
			gp.SetText(1);
		}
		
		Drum drum = transform.GetComponent<Drum>();
		drum.SetHitsTaken(hitsToleratedOrig - hitsTolerated);
	}
}
