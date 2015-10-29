using UnityEngine;
using System.Collections;

[System.Serializable]
public class FeedBack
{
	[Range(1,4)][Tooltip("Types are: 1 = positive review, 2 = negative review, 3 = special review, 4 = Instructor review.")]
	public int feedbackType = 1;
	[Tooltip("Name of the person making the review. If left empty, a random name will be provided.")]
	public string person;
	[Tooltip("Actual content of the review.")]
	public string content;
	[Range(0f,100f)][Tooltip("Review grade out of 100.")]
	public float rating;

	public FeedBack(int t, string p, string c, float r)
	{
		this.feedbackType = t;
		this.person = p;
		this.content = c;
		this.rating = r;
	}

}

public class FeedbackController : MonoBehaviour {


	[SerializeField]
	FeedBack one;



	FeedBack GenerateFeedBack(string reason)
	{
		switch(reason)
		{
		case "Expensive":

			break;
		case "Dirty":
			break;
		case "Full":
			break;
		case "Other":
			break;
		case "Great":
			break;
		case "Special":
			break;
		}
		return null;
	}
}
