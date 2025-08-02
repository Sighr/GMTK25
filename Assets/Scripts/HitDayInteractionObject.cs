using System.Collections.Generic;
using UnityEngine;

public class HitDayInteractionObject : DayInteractionObject
{
	[Header("Configuration")]
	[SerializeField] private List<GameObject> objectsToDisable;
	[SerializeField] private List<GameObject> objectsToEnable;
	[SerializeField] private List<Rigidbody> rbsToDisable;
	[SerializeField] private List<Rigidbody> rbsToEnable;

	[SerializeField] private bool useNormal;
	[SerializeField] private Vector3 normal;
	[SerializeField] private float maxNormalsAngle;

	[Header("Runtime")]
	[SerializeField] private bool isInteractable = true;
	
	protected override void PerformInteractionChange()
	{
		foreach (GameObject go in objectsToDisable)
		{
			go.SetActive(false);
		}

		foreach (GameObject go in objectsToEnable)
		{
			go.SetActive(true);		
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

		foreach (Rigidbody rb in rbsToDisable)
		{
			rb.isKinematic = true;
		}
		
		foreach (Rigidbody rb in rbsToEnable)
		{
			rb.isKinematic = false;
		}
	}
}