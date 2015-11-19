using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("Feedbacks")]
[XmlInclude(typeof(FeedBack))] // include type class.
public class FeedbackList       //Class holding a list of reports. Used to storing and loading purposes.
{
	[XmlArray("FeedBackList")]
	public List<FeedBack> feedbackList = new List<FeedBack>();
	
	[XmlElement("Listname")]
	public string Listname { get; set; }
	
	// Constructor
	public FeedbackList() { }
	
	public FeedbackList(string name)
	{
		this.Listname = name;
	}
	
	public void Add(FeedBack report)
	{
		feedbackList.Add(report);
	}
}

[System.Serializable]
[XmlRoot("Feedback")]
public class FeedBack
{
	[Range(1,4)][Tooltip("Types are: 1 = positive review, 2 = negative review, 3 = special review, 4 = Instructor review.")]
	public int feedbackType = 1;
	[Tooltip("Name of the person making the review. If left empty, a random name will be provided.")]
	public string person;
	[Tooltip("Actual content of the review.")]
	public string content;
	[Range(0f,5f)][Tooltip("Review grade out of 5.")]
	public float rating;

	public FeedBack() { }
	
	public FeedBack(int t, string p, string c, float r)
	{
		this.feedbackType = t;
		this.person = p;
		this.content = c;
		this.rating = r;
	}
}
