using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour, ICustomEventHandler
{
	[Header("Customization")]
	[SerializeField] private CustomEvent dayEnded;
	[SerializeField] private CustomEvent escapeThroughDoor;
	[SerializeField] private CustomEvent escapeThroughWindow;
	[SerializeField] private CustomEvent heroStuck;
	[SerializeField] private CustomEvent heroReturnedToStation;
	
	[SerializeField] private DailyManager dailyManager;
	[SerializeField] private GameObject escapeThroughDoorEnding;
	[SerializeField] private GameObject escapeThroughWindowEnding;

	private bool isGameOver = false;

	private void OnEnable()
	{
		dayEnded.Subscribe(this);
		escapeThroughDoor.Subscribe(this);
		escapeThroughWindow.Subscribe(this);
		heroStuck.Subscribe(this);
		heroReturnedToStation.Subscribe(this);
	}

	private void OnDisable()
	{
		dayEnded.Unsubscribe(this);
		escapeThroughDoor.Unsubscribe(this);
		escapeThroughWindow.Unsubscribe(this);
		heroStuck.Unsubscribe(this);
		heroReturnedToStation.Unsubscribe(this);
	}
	
	public void HandleEvent(CustomEvent.EventType eventType)
	{
		switch (eventType)
		{
			case CustomEvent.EventType.DayEnded:
				OnDayEnded();
				break;
			case CustomEvent.EventType.EscapeThroughDoor:
				OnEscapeThroughDoor();
				break;
			case CustomEvent.EventType.EscapeThroughWindow:
				OnEscapeThroughWindow();
				break;
			case CustomEvent.EventType.MainHeroStuck:
				OnHeroStuck();
				break;
			case CustomEvent.EventType.MainHeroReturnedToStation:
				OnHeroReturnedToStation();
				break;
		}
	}

	private void OnDayEnded()
	{
		// TODO
	}

	private void OnEscapeThroughDoor()
	{
		EndGame();
		escapeThroughDoorEnding.SetActive(true);
	}

	private void OnEscapeThroughWindow()
	{
		EndGame();
		escapeThroughDoorEnding.SetActive(true);
	}

	private void EndGame()
	{
		dailyManager.enabled = false;
		isGameOver = true;
		// TODO: disable hero
	}

	private void OnHeroStuck()
	{
		// TODO: count stucks for bad ending
		dailyManager.EarlyStopDay();
	}

	private void OnHeroReturnedToStation()
	{
		dailyManager.EarlyStopDay();
	}
}