using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarRatingController : MonoBehaviour
{
	[Header("Configuration")]
	[SerializeField] private List<Image> stars;
	[SerializeField] private Sprite fullStar;
	[SerializeField] private Sprite halfStar;
	[SerializeField] private Sprite emptyStar;

	public void SetRating(int current, int max)
	{
		// TODO
	}
}