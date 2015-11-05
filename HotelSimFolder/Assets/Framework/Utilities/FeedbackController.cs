#define ENABLE_TESTING 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;



#region Feedback Class
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

	public FeedBack(){}

	public FeedBack(int t, string p, string c, float r)
	{
		this.feedbackType = t;
		this.person = p;
		this.content = c;
		this.rating = r;
	}


}
#endregion
public class FeedbackController : MonoBehaviour {


	List<GameObject> listOfFeedbacks = new List<GameObject>();

	[SerializeField]
	FeedBack one;

	[SerializeField][Tooltip("Array of min/max rating you can receive for each type of feedbacks. Lowest possible is 0 and highest is 5. Elements are as follow. Too Expensive min:0 max:1 >> Dirty min:2 max:3 " +
		">> Full min 4: max:5 >> OverBooked min:6 max:7 >> Great Stay min:8 max:9 >> Special min:10 max:11.")]
	float[] rangesOfRatings = new float[12];

	//xml name file gets assign to a string array for random names.
	string[] nameArray = new string[1000];
	XmlTextReader reader = new XmlTextReader ("Names.xml");

	[SerializeField][Tooltip("Array of sprites holding the start ratings.")]
	Sprite[] stars = new Sprite[11];

	public GameObject feedBackPrefab;

	void Start()
	{
		SetNamingArray();//set the name array.
	}

	void DeleteOnClick(GameObject pop)
	{
		listOfFeedbacks.Remove(pop);

		for(int i = 0; i < listOfFeedbacks.Count; i++)
		{
			listOfFeedbacks[i].transform.position = new Vector3(Screen.width-240, 220f + (i * 120f), 0f);
		}

		Destroy(pop);
	}
	void GenerateFeedBack(string reason)//Generates a FeedBack based on a reason param.
	{
		FeedBack fb = new FeedBack();
		switch(reason)
		{
		case "Expensive":
			fb.rating = Random.Range(rangesOfRatings[0],rangesOfRatings[1]);
			fb.feedbackType = 2;
			fb.content = "The room was too expensive for my taste.";
			break;
		case "Dirty":
			fb.rating = Random.Range(rangesOfRatings[2],rangesOfRatings[3]);
			fb.feedbackType = 2;
			fb.content = "The cleanliness of this establishment is very poor.";
			break;
		case "Full":
			fb.rating = Random.Range(rangesOfRatings[4],rangesOfRatings[5]);
			fb.feedbackType = 2;
			fb.content = "Tyred to get a reservation but it was fully booked.";
			break;
		case "OverBooked":
			fb.rating = Random.Range(rangesOfRatings[6],rangesOfRatings[7]);
			fb.feedbackType = 2;
			fb.content = "Even though we phoned ahead of time and booked reservation we were told the hotel was fully booked once we arrived.";
			break;
		case "Great":
			fb.rating = Random.Range(rangesOfRatings[8],rangesOfRatings[9]);
			fb.feedbackType = 1;
			fb.content = "Had a nice stay.";
			break;
		case "Special":
			fb.rating = Random.Range(rangesOfRatings[10],rangesOfRatings[11]);
			fb.feedbackType = 3;
			fb.content = "Don't know yet what to use this for.";
			break;
		}

		fb.person = nameArray[Random.Range(0,1000)];
		//Instatiate a new popup with the comment infos.
		GameObject newFeedback = new GameObject();
		newFeedback = Instantiate(feedBackPrefab, new Vector3(Screen.width-240, 220f + (listOfFeedbacks.Count * 120f), 0f), Quaternion.identity) as GameObject;
		newFeedback.transform.SetParent(GameObject.FindGameObjectWithTag("UI").transform);
		newFeedback.transform.FindChild("txt_Name").GetComponent<Text>().text = fb.person;
		newFeedback.transform.FindChild("txt_Comment").GetComponent<Text>().text = fb.content;
		newFeedback.transform.FindChild("Img_Rating").GetComponent<Image>().sprite = stars[Mathf.RoundToInt((fb.rating/5f)*10)];
		newFeedback.GetComponent<Button>().onClick.AddListener(() => { DeleteOnClick(newFeedback); }); 
		newFeedback.transform.localScale = new Vector3(1f,1f,1f);

		listOfFeedbacks.Add(newFeedback);

	}

	#region OnStart
	public void SetNamingArray()//create an array of anmes with 1000 entries.
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
	#endregion
#if ENABLE_TESTING
	public void Test()
	{
		int testrnd = Random.Range(1,6);
		string testreason = "";
		switch(testrnd)
		{
		case 1:
			testreason = "Expensive";
			break;
		case 2:
			testreason = "Dirty";
			break;
		case 3:
			testreason = "Full";
			break;
		case 4:testreason = "OverBooked";
			break;
		case 5:testreason = "Great";
			break;
		case 6:testreason = "Special";
			break;
		}
		GenerateFeedBack(testreason);
	}
#endif
}
