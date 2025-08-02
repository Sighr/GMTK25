using System;
using UnityEngine;

public class DustTrigger : MonoBehaviour
{
	public DustGeneration generator;

	private void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.CompareTag("Player"))
		{
			return;
		}

		--generator.dustRemaining;
		Destroy(gameObject);
	}
}