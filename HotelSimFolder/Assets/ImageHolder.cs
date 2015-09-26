using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageHolder : MonoBehaviour {
	public Transform[] stars;
	public Sprite emptyStar;
	public Sprite fullStar;

	public void setupStars(int StarLevel)
	{
		for (int index = 0; index < 6; index++)
		{
			Image iHold = stars[index].GetComponent("Image") as Image;
			iHold.sprite = getStarImage (index, StarLevel);
		}
	}

	public Sprite getStarImage (int StarNum,int StarLevel)
	{
		if (StarNum < StarLevel)
		{
			return fullStar;
		}
		return emptyStar;
	}
}
