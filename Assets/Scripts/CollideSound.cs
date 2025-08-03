using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CollideSound : MonoBehaviour
{
	[Header("Customisation")] [SerializeField]
	private AudioSource source;

	[Header("Settings")] [SerializeField] private string tagToCompare = "Player";
	[SerializeField] private float soundDelay = 0f;

	private float timeSinceLastSound = 0f;

	private void OnCollisionEnter(Collision other)
	{
		if (!other.gameObject.CompareTag(tagToCompare))
		{
			return;
		}

		if (timeSinceLastSound < soundDelay)
		{
			return;
		}

		timeSinceLastSound = 0;
		source.PlayOneShot(source.clip);
	}

	private void Update()
	{
		timeSinceLastSound += Time.deltaTime;
	}
}