using UnityEngine;

public class ChargeController : MonoBehaviour
{
	[Header("Configuration")]
	[SerializeField] private GameObject filler;

	public void SetCharge(float percent)
	{
		float scale = Mathf.Clamp(percent, 0f, 1f);
		filler.transform.localScale = new Vector3(scale, 1, 1);
	}
}