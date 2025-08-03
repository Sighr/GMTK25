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

	public void SetRating(float rating)
	{
		float starsRating = stars.Count * rating;
		int fullStarCount = Mathf.FloorToInt(starsRating);
		int lastFilledStar = 0;
		bool hasHalfStar = !(starsRating - fullStarCount < 1e-4);
		for (lastFilledStar = 0; lastFilledStar < fullStarCount; lastFilledStar++)
		{
			stars[lastFilledStar].sprite = fullStar;
		}
		if (hasHalfStar)
		{
			stars[lastFilledStar].sprite = halfStar;
			lastFilledStar++;
		}

		for (; lastFilledStar < stars.Count; lastFilledStar++)
		{
			stars[lastFilledStar].sprite = emptyStar;
		}
	}
}