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
	[SerializeField] private ChargeController chargeController;
	[SerializeField] private AudioSource suckingSoundSource;
	[SerializeField] private AudioSource startSuckingSoundSource;
	
	[Header("Runtime")]
	[SerializeField] private int currentDay;

	private Coroutine timerCoroutine;

	private int CurrentDay
	{
		get => currentDay;
		set
		{
			currentDayText.text = $"Loop {value}";
			currentDay = value;
		}
	}

	private void OnEnable()
	{
		StartNewDay();
	}

	private void OnDisable()
	{
		StopDailyCoroutine();
	}

	private void StopDailyCoroutine()
	{
		if (timerCoroutine != null)
		{
			StopCoroutine(timerCoroutine);
		}

		timerCoroutine = null;
	}

	private void StartNewDay()
	{
		startSuckingSoundSource.Play();
		suckingSoundSource.PlayDelayed(2f);
		++CurrentDay;
		timerCoroutine = StartCoroutine(StartDayTimer());
	}

	public void EarlyStopDay()
	{
		StopDailyCoroutine();
		StartCoroutine(FinishDaySubroutine());
	}

	private IEnumerator StartDayTimer()
	{
		for (float time = 0; time < dayDuration; time += tickDuration)
		{
			yield return new WaitForSeconds(tickDuration);
			chargeController.SetCharge((dayDuration - time) / dayDuration);
		}

		yield return FinishDaySubroutine();
	}

	private IEnumerator FinishDaySubroutine()
	{
		dimmerAnim.SetTrigger(DimAnimatorProperty);
		suckingSoundSource.Stop();
		yield return new WaitForSeconds(dimDelay);
		dayEnded.Fire();
		yield return new WaitForSeconds(dimDelay);
		StartNewDay();
	}
}