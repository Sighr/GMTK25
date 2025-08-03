using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
	[Header("Configuration")]
	[SerializeField] private GameObject anyKeyText;
	
	[Header("Settings")]
	[SerializeField] private float minTimeToShowEnding = 5f;
	
	private InputAction anyKey;
	private float time = 0;
	private bool readyToShowEnding = false;

	private void OnEnable()
	{
		anyKey = InputSystem.actions.FindAction("AnyKey");
		anyKey.performed += PressedAnyKey;
	}
	private void OnDisable()
	{
		anyKey.performed -= PressedAnyKey;
	}

	private void PressedAnyKey(InputAction.CallbackContext obj)
	{
		if (!readyToShowEnding)
		{
			return;
		}

		SceneManager.LoadScene(0);
	}

	private void Update()
	{
		if (!readyToShowEnding)
		{
			if (time > minTimeToShowEnding)
			{
				readyToShowEnding = true;
				anyKeyText.SetActive(true);
			}
			time += Time.deltaTime;
		}
	}
}