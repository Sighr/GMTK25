using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DailyManager : MonoBehaviour
{
	private static readonly int DimAnimatorProperty = Animator.StringToHash("Dim");

	[Header("Settings")]
	[SerializeField] private float dayDuration;
	[SerializeField] private float tickDuration;
	[SerializeField] private float dimDelay = 1f;
	
	[Header("Configuration")]
	[SerializeField] private CustomEvent dayEnded;
	[SerializeField] private Animator dimmerAnim;
	[SerializeField] private TextMeshProUGUI currentDayText;
	
	[Header("Runtime")]
	[SerializeField] private int currentDay;

	private Coroutine timerCoroutine;

	private int CurrentDay
	{
		get => currentDay;
		set
		{
			currentDayText.text = $"Day {value}";
			currentDay = value;
		}
	}

	private void OnEnable()
	{
		StartNewDay();
	}

	private void OnDisable()
	{
		StopCoroutine(timerCoroutine);
		timerCoroutine = null;
	}

	private void StartNewDay()
	{
		++CurrentDay;
		timerCoroutine = StartCoroutine(StartDayTimer());
	}

	private IEnumerator StartDayTimer()
	{
		for (float time = 0; time < dayDuration; time += tickDuration)
		{
			yield return new WaitForSeconds(tickDuration);
			// TODO: time progress bar
		}
		
		dimmerAnim.SetTrigger(DimAnimatorProperty);
		yield return new WaitForSeconds(dimDelay);
		dayEnded.Fire();
		yield return new WaitForSeconds(dimDelay);
		StartNewDay();
	}
}