using System;
using System.Collections.Generic;
using UnityEngine;

public class WindowBreak : MonoBehaviour
{
	[Header("Configuration")]
	[SerializeField] private List<Rigidbody> fragmentRbs;
	[SerializeField] private List<GameObject> toDisable;
	[SerializeField] private List<GameObject> toEnable;
	
	[Header("Settings")]
	[SerializeField] private float forceMultiplier = 1f;
	[SerializeField] private float forceRadius = 1f;

	private void OnCollisionEnter(Collision collision)
	{
		if (!collision.gameObject.CompareTag("Ladder"))
		{
			return;
		}

		foreach (GameObject go in toDisable)
		{
			go.SetActive(false);
		}

		foreach (GameObject go in toEnable)
		{
			go.SetActive(true);
		}

		var contact = collision.contacts[0].point;
		
		foreach (Rigidbody rb in fragmentRbs)
		{
			rb.isKinematic = false;
			float verticalDistance = contact.y - rb.position.y;
			float force = Mathf.Max(verticalDistance, 0) * forceMultiplier;
			rb.AddExplosionForce(force, contact, forceRadius);
		}
	}
}