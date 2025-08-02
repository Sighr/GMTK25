using UnityEngine;

public class EarlyReturnToBaseTrigger : MonoBehaviour
{
	[SerializeField] private CustomEvent dayEnded;
	[SerializeField] private CustomEvent heroReturned;
	
	[SerializeField] private float minTimeToReturn;
}