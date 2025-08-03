using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DailyHateTextController : MonoBehaviour, ICustomEventHandler
{
	[Header("Configuration")]
	[SerializeField] private List<string> texts;
	[SerializeField] private TextMeshProUGUI text;
	[SerializeField] private CustomEvent dayEnded;

	private void OnEnable()
	{
		dayEnded.Subscribe(this);
	}

	private void OnDisable()
	{
		dayEnded.Unsubscribe(this);
	}

	public void HandleEvent(CustomEvent.EventType eventType)
	{
#if (UNITY_EDITOR)
		if (eventType == CustomEvent.EventType.DayEnded)
		{
			OnDayEnded();
		}
		else
		{
			Debug.LogError($"Misconfigured event type for {gameObject.name}: expected dayEnded, received {eventType}");
		}
#else
		OnDayEnded();		
#endif
	}

	private void OnDayEnded()
	{
		text.text = texts[Random.Range(0, texts.Count)];
	}
}