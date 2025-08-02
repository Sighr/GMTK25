using UnityEngine;

public class HitDayInteractionObject : DayInteractionObject
{
	[Header("Configuration")]
	[SerializeField] private GameObject interactableObject;
	[SerializeField] private GameObject nonInteractableObject;

	[SerializeField] private bool useNormal;
	[SerializeField] private Vector3 normal;
	[SerializeField] private float maxNormalsAngle;

	[Header("Runtime")]
	[SerializeField] private bool isInteractable = true;
	
	protected override void PerformInteractionChange()
	{
		interactableObject.SetActive(false);
		nonInteractableObject.SetActive(true);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!isInteractable)
		{
			return;
		}
		
		if (collision.gameObject.layer != LayerMask.NameToLayer("MainHero"))
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