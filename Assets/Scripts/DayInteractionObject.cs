using UnityEngine;

public abstract class DayInteractionObject : MonoBehaviour, ICustomEventHandler
{
	[Header("Configuration")]
	[SerializeField] private CustomEvent dayEnded;
	
	protected bool wasInteractedToday = false;
	
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
		if (wasInteractedToday)
		{
			PerformInteractionChange();
		}
	}

	protected abstract void PerformInteractionChange();
}