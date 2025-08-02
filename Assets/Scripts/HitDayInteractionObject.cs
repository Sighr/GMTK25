using System.Collections.Generic;
using UnityEngine;

public class HitDayInteractionObject : DayInteractionObject
{
	[Header("Configuration")]
	[SerializeField] private List<GameObject> objectsToDisable;
	[SerializeField] private List<GameObject> objectsToEnable;

	[SerializeField] private bool useNormal;
	[SerializeField] private Vector3 normal;
	[SerializeField] private float maxNormalsAngle;

	[Header("Runtime")]
	[SerializeField] private bool isInteractable = true;
	
	protected override void PerformInteractionChange()
	{
		foreach (GameObject interactableObject in objectsToDisable)
		{
			interactableObject.SetActive(false);
		}

		foreach (GameObject nonInteractableObject in objectsToEnable)
		{
			nonInteractableObject.SetActive(true);		
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!isInteractable)
		{
			return;
		}
		
		if (!collision.gameObject.CompareTag("Player"))
		{
			return;
		}

		if (useNormal)
		{
			var contact = collision.GetContact(0);
			float angle = Vector3.Angle(normal, contact.normal);
			if (angle > maxNormalsAngle)
			{
				return;
			}
		}

		isInteractable = false;
		wasInteractedToday = true;

		// TODO: animation
		// PerformAnimation();
	}
}