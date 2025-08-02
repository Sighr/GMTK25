using System;
using System.Collections;
using UnityEngine;

public class DailyManager : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private float dayDuration;
	[SerializeField] private float tickDuration;
	
	[Header("Configuration")]
	[SerializeField] private CustomEvent dayEnded;

	private Coroutine timerCoroutine;
	
	private void OnEnable()
	{
		timerCoroutine = StartCoroutine(StartDayTimer());
	}

	private void OnDisable()
	{
		StopCoroutine(timerCoroutine);
		timerCoroutine = null;
	}

	IEnumerator StartDayTimer()
	{
		for (float time = 0; time < dayDuration; time += tickDuration)
		{
			yield return new WaitForSeconds(tickDuration);
			// TODO: time progress bar
		}
		OnDayEnded();
	}

	private void OnDayEnded()
	{
		dayEnded.Fire();
	}
}