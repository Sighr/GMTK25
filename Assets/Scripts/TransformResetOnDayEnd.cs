using System.Collections.Generic;
using UnityEngine;

public class TransformResetOnDayEnd : MonoBehaviour, ICustomEventHandler
{
	[Header("Configuration")]
	[SerializeField] private CustomEvent dayEnded;

	[SerializeField] private List<Transform> baseTransforms;
	
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
#if UNITY_EDITOR
		if (baseTransforms.Count == 0)
		{
			Debug.LogError($"No baseTransforms have been assigned for {gameObject.name}");
		}
#endif
		var baseTransform = baseTransforms[Random.Range(0, baseTransforms.Count)];
		transform.position = baseTransform.position;
		transform.rotation = baseTransform.rotation;
	}
}