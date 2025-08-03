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
	[SerializeField] private DustGeneration dustGeneration;
	[SerializeField] private StarRatingController ratingController;
	[SerializeField] private GameObject escapeThroughDoorEnding;
	[SerializeField] private GameObject escapeThroughWindowEnding;
	[SerializeField] private GameObject strikesEnding;
	[SerializeField] private GameObject hero;
	[SerializeField] private GameObject endGameCamera;
	[SerializeField] private GameObject gameHUD;
	
	[Header("Settings")]
	[SerializeField] private int maxRemainingDustAllowed = 6;
	[SerializeField] private int maxStrikesAllowed = 30;

	private int strikes;
	private bool isGameOver = false;
	private bool hadStrikeThisDay = false;

	public int Strikes
	{
		get => strikes;
		set
		{
			strikes = value;
			
			ratingController.SetRating(1 - (float) strikes / maxStrikesAllowed);
		}
	}

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
		if (dustGeneration.dustRemaining > maxRemainingDustAllowed)
		{
			GetStrike();
		}

		dustGeneration.ClearRemainingDust();
		dustGeneration.GenerateDust();
		hadStrikeThisDay = false;
	}

	private void OnEscapeThroughDoor()
	{
		EndGame();
		escapeThroughDoorEnding.SetActive(true);
	}

	private void OnEscapeThroughWindow()
	{
		EndGame();
		escapeThroughWindowEnding.SetActive(true);
	}

	private void GetStrike()
	{
		if (hadStrikeThisDay)
		{
			return;
		}
		hadStrikeThisDay = true;
		++Strikes;
		if (Strikes >= maxStrikesAllowed)
		{
			EndGame();
			strikesEnding.SetActive(true);
		}
	}

	private void EndGame()
	{
		dailyManager.enabled = false;
		isGameOver = true;
		hero.SetActive(false);
		endGameCamera.SetActive(true);
		gameHUD.SetActive(false);
	}

	private void OnHeroStuck()
	{
		GetStrike();
		dailyManager.EarlyStopDay();
	}

	private void OnHeroReturnedToStation()
	{
		dailyManager.EarlyStopDay();
	}
}