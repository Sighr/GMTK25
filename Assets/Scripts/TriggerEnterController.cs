using System;
using UnityEngine;

public class TriggerEnterController : MonoBehaviour
{
	[SerializeField] private CustomEvent triggerEnter;

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Player"))
		{
			return;
		}

		triggerEnter.Fire();
	}
}