using UnityEngine;

public class EarlyReturnToBaseTrigger : MonoBehaviour, ICustomEventHandler
{
	[Header("Configuration")]
	[SerializeField] private CustomEvent dayEnded;
	[SerializeField] private CustomEvent heroReturned;
	
	[SerializeField] private float minTimeToReturn;

	private float timePassed = 0;
	
	private void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.CompareTag("Player"))
		{
			return;
		}

		if (minTimeToReturn > timePassed)
		{
			return;
		}
		heroReturned.Fire();
	}
	
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
		timePassed = 0;
	}

	private void Update()
	{
		timePassed += Time.deltaTime;
	}
}