using System.Collections;
using UnityEngine;

public class MainHeroDayController : MonoBehaviour, ICustomEventHandler
{
	[Header("Customization")]
	[SerializeField] private CustomEvent dayEnded;
	[SerializeField] private Rigidbody rb;
	[SerializeField] private Transform baseTransform;
	
	[SerializeField] private MainHeroMovementController movementController;

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
	
	[ContextMenu("On Day Ended")]
	private void OnDayEnded()
	{
		StartCoroutine(ResetToBase());
	}

	private IEnumerator ResetToBase()
	{
		movementController.enabled = false;
		rb.isKinematic = true;
		yield return new WaitForSeconds(0.1f);
		transform.position = baseTransform.position;
		transform.rotation = baseTransform.rotation;
		yield return new WaitForSeconds(1f);
		movementController.enabled = true;
		rb.isKinematic = false;
	}
}