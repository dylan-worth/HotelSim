using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

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

	string[] nameArray = new string[1000];

	XmlTextReader reader = new XmlTextReader ("Names.xml");

	public void SetNamingArray()
	{


		int i = 0;
		int j = 0;
		while (reader.Read()) 
		{

			switch (reader.NodeType) 
			{
			case XmlNodeType.Element: // The node is an element.
				while (reader.MoveToNextAttribute()) // Read the attributes.

				if(j < 2)
				{
					nameArray[i] += reader.Value + " ";
					j++;
				}
				else
				{
					j = 1;
					nameArray[i+1] += reader.Value + " ";
					i++;
				}

				break;
			case XmlNodeType.Text: //Display the text in each element.
				break;
			case XmlNodeType.EndElement: //Display the end of the element.
				break;
			}

		}
	}
	void Start()
	{
		//SetNamingArray();set the name array.
	}
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
