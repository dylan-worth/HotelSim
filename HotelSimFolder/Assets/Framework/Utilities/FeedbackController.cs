using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using UnityEngine.UI;

public class FeedbackController : MonoBehaviour {


	List<GameObject> listOfFeedbacks = new List<GameObject>();//Active List on screen.

    [System.NonSerialized]
	public List<FeedBack> archivedListOfFeedbacks = new List<FeedBack>();//List of all previous feedback.

	[SerializeField]
	FeedBack one;

	[SerializeField][Tooltip("Array of min/max rating you can receive for each type of feedbacks. Lowest possible is 0 and highest is 5. Elements are as follow. Too Expensive min:0 max:1 >> Dirty min:2 max:3 " +
		">> Full min 4: max:5 >> OverBooked min:6 max:7 >> Great Stay min:8 max:9 >> Special min:10 max:11.")]
	float[] rangesOfRatings = new float[12];

	//xml name file gets assign to a string array for random names.
	string[] nameArray = new string[1000];
	XmlTextReader readerNames  = new XmlTextReader ("Names.xml");

	//xml file gets assign to array of string array.
	List<List<string>> comments = new List<List<string>>();
	XmlTextReader readerComments  = new XmlTextReader ("Comments.xml");

	[SerializeField][Tooltip("Array of sprites holding the start ratings.")]
	Sprite[] stars = new Sprite[11];

	public GameObject feedBackPrefab;

	void Start()
	{

		SetNamingArray();//set the name array.

		//Lists of all the different comments
		List<string> Expensive = new List<string>();
		List<string> Dirty = new List<string>();                        //Comments are listed in comments.xml file.
		List<string> Full = new List<string>();                         //Can be edited by adding/removing lines in the file.
        List<string> Overbooked = new List<string>();                   //I.E. <Expensive>We found the hotel too expensive.</Expensive>
        List<string> Great = new List<string>();                        //This would had "We found the hotel too expensive." comments to the expensive list.
		List<string> Special = new List<string>();
		comments.Add(Expensive);
		comments.Add(Dirty);
		comments.Add(Full);
		comments.Add(Overbooked);
		comments.Add(Great);
		comments.Add(Special);
		SetCommentsArray();//Sets the list of list of comments.

		
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
	void GenerateFeedBack(string reason)//Generates a FeedBack based on a reason parameter. It then instantiate a window to display it.
	{
		FeedBack fb = new FeedBack();
		switch(reason)
		{
		case "Expensive":
			fb.rating = Random.Range(rangesOfRatings[0],rangesOfRatings[1]);
			fb.feedbackType = 2;
			fb.content = comments[0][Random.Range(0,comments[0].Count)];
			break;
		case "Dirty":
			fb.rating = Random.Range(rangesOfRatings[2],rangesOfRatings[3]);
			fb.feedbackType = 2;
			fb.content = comments[1][Random.Range(0,comments[1].Count)];
			break;
		case "Full":
			fb.rating = Random.Range(rangesOfRatings[4],rangesOfRatings[5]);
			fb.feedbackType = 2;
			fb.content = comments[2][Random.Range(0,comments[2].Count)];
			break;
		case "OverBooked":
			fb.rating = Random.Range(rangesOfRatings[6],rangesOfRatings[7]);
			fb.feedbackType = 2;
			fb.content = comments[3][Random.Range(0,comments[3].Count)];
			break;
		case "Great":
			fb.rating = Random.Range(rangesOfRatings[8],rangesOfRatings[9]);
			fb.feedbackType = 1;
			fb.content = comments[4][Random.Range(0,comments[4].Count)];
			break;
		case "Special":
			fb.rating = Random.Range(rangesOfRatings[10],rangesOfRatings[11]);
			fb.feedbackType = 3;
			fb.content = comments[5][Random.Range(0,comments[5].Count)];
			break;
		}
		float sizingFactor = Screen.width/800f;
		fb.person = nameArray[Random.Range(0,1000)];
		//Instatiate a new popup with the comment infos.
		GameObject newFeedback = new GameObject();
		newFeedback = Instantiate(feedBackPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		newFeedback.transform.SetParent(GameObject.FindGameObjectWithTag("UI").transform);

		newFeedback.transform.position = new Vector3(Screen.width-( newFeedback.GetComponent<RectTransform>().sizeDelta.x/2f*sizingFactor),
		                                             200f + (listOfFeedbacks.Count * newFeedback.GetComponent<RectTransform>().sizeDelta.y *2f), 0f);
		newFeedback.transform.FindChild("txt_Name").GetComponent<Text>().text = fb.person;
		newFeedback.transform.FindChild("txt_Comment").GetComponent<Text>().text = fb.content;
		newFeedback.transform.FindChild("Img_Rating").GetComponent<Image>().sprite = stars[Mathf.RoundToInt((fb.rating/5f)*10)];
		newFeedback.GetComponent<Button>().onClick.AddListener(() => { DeleteOnClick(newFeedback); }); 
		newFeedback.transform.localScale = new Vector3(1f,1f,1f);

		listOfFeedbacks.Add(newFeedback);//Added to list of active feedback on the screen.
		archivedListOfFeedbacks.Add(fb);//Added to archived list. Saved so we can access at a later time.
	}

	public void TryGenFeedBack(string reason, float percentChance )
	{

		bool genBool = (Random.Range(0f,100f) < percentChance);
	
		if(genBool)
		{
			GenerateFeedBack(reason);
		}
	}

	#region OnStart
	public void SetNamingArray()//create an array of names with 1000 entries.
	{
		int i = 0;
		int j = 0;
		while (readerNames.Read()) 
		{
			switch (readerNames.NodeType) 
			{
			case XmlNodeType.Element: // The node is an element.
				while (readerNames.MoveToNextAttribute()) // Read the attributes.
					
				if(j < 2)
				{
					nameArray[i] += readerNames.Value + " ";
					j++;
				}
				else
				{
					j = 1;
					nameArray[i+1] += readerNames.Value + " ";
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
	public void SetCommentsArray()
	{

		int arrayLoc = 0;
		while (readerComments.Read()) 
		{
			switch (readerComments.NodeType) 
			{
			case XmlNodeType.Element: // The node is an element.
				switch(readerComments.LocalName)
				{
				case "Expensive":
					arrayLoc = 0;
					break;
				case "Dirty":
					arrayLoc = 1;
					break;
				case "Full":
					arrayLoc = 2;
					break;
				case "Overbooked":
					arrayLoc = 3;
					break;
				case "Great":
					arrayLoc = 4;
					break;
				case "Special":
					arrayLoc = 5;
					break;
				}
				break;
			case XmlNodeType.Text: //Display the text in each element.
				comments[arrayLoc].Add(readerComments.Value);

				break;
			case XmlNodeType.EndElement: //Display the end of the element.
				break;
			}
		}
	}
	#endregion

}
