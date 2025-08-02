using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DustGeneration : MonoBehaviour
{
	[Header("Configuration")]
	[SerializeField] private CustomEvent dayEnded;
	[SerializeField] private List<DustTrigger> dustPrefabs;
	[SerializeField] private List<Transform> dustSpawnPoints;
	[SerializeField] private Transform dustContainer;
	
	[Header("Settings")]
	[SerializeField] private int dustMinAmount = 7;
	[SerializeField] private int dustMaxAmount = 13;
	[SerializeField] private float dustMinScale = 0.7f;
	[SerializeField] private float dustMaxScale = 1.1f;
	
	public int dustRemaining;

	private void Awake()
	{
		GenerateDust();
	}

	public void ClearRemainingDust()
	{
		for (int i = 0; i < dustContainer.childCount; i++)
		{
			Transform child = dustContainer.GetChild(i);
			Destroy(child.gameObject);
		}
		
		dustRemaining = 0;
	}

	public void GenerateDust()
	{
		int totalNumber = Random.Range(dustMinAmount, dustMaxAmount);
		List<Transform> localCopyDustSpawnPoints = new List<Transform>(dustSpawnPoints);
		for (int i = 0; i < totalNumber; i++)
		{
			Transform spawnPoint = localCopyDustSpawnPoints[Random.Range(0, localCopyDustSpawnPoints.Count)];
			localCopyDustSpawnPoints.Remove(spawnPoint);
			DustTrigger dustPrefab = dustPrefabs[Random.Range(0, dustPrefabs.Count)];
			Quaternion rotation = Quaternion.Euler(dustPrefab.transform.rotation.eulerAngles + Vector3.up * Random.Range(0, 360));
			DustTrigger instantiated = Instantiate(dustPrefab, spawnPoint.position, rotation, dustContainer);
			instantiated.generator = this;
		}

		dustRemaining = totalNumber;
	}
}