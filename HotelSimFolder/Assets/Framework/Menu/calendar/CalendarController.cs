using UnityEngine;
using UnityEngine.UI;

public class CalendarController : MonoBehaviour {


    GameObject calendarTab;
    GameObject[] monthsHolder = new GameObject[12];
    GameObject datePopup;
    GameObject eventController;

	[SerializeField][Tooltip("Range of seasonal inquiry for given month. MIN and MAX value are editable.")]
	float[] a_Jan = new float[2];
	[SerializeField][Tooltip("Range of seasonal inquiry for given month. MIN and MAX value are editable.")]
	float[] a_Feb = new float[2];
	[SerializeField][Tooltip("Range of seasonal inquiry for given month. MIN and MAX value are editable.")]
	float[] a_Mar = new float[2];
	[SerializeField][Tooltip("Range of seasonal inquiry for given month. MIN and MAX value are editable.")]
	float[] a_Apr = new float[2];
	[SerializeField][Tooltip("Range of seasonal inquiry for given month. MIN and MAX value are editable.")]
	float[] a_May = new float[2];
	[SerializeField][Tooltip("Range of seasonal inquiry for given month. MIN and MAX value are editable.")]
	float[] a_Jun = new float[2];
	[SerializeField][Tooltip("Range of seasonal inquiry for given month. MIN and MAX value are editable.")]
	float[] a_Jul = new float[2];
	[SerializeField][Tooltip("Range of seasonal inquiry for given month. MIN and MAX value are editable.")]
	float[] a_Aug = new float[2];
	[SerializeField][Tooltip("Range of seasonal inquiry for given month. MIN and MAX value are editable.")]
	float[] a_Sep = new float[2];
	[SerializeField][Tooltip("Range of seasonal inquiry for given month. MIN and MAX value are editable.")]
	float[] a_Oct = new float[2];
	[SerializeField][Tooltip("Range of seasonal inquiry for given month. MIN and MAX value are editable.")]
	float[] a_Nov = new float[2];
	[SerializeField][Tooltip("Range of seasonal inquiry for given month. MIN and MAX value are editable.")]
	float[] a_Dec = new float[2];

	float[][] monthsArray = new float[12][];

	[System.NonSerialized]
	public float[] seasonalTrends = new float[366]; 

	int[] numberOfdaysPerMonths = new int[12]{31,28,31,30,31,30,31,31,30,31,30,31};

    [SerializeField][Tooltip("Array of images for each months to display. DO NOT CHANGE SIZE VALUE!")]
    Sprite[] ImagesOfMonth = new Sprite[12];
	void Awake()
	{
		monthsArray[0] = a_Jan;
		monthsArray[1] = a_Feb;
		monthsArray[2] = a_Mar;
		monthsArray[3] = a_Apr;
		monthsArray[4] = a_May;
		monthsArray[5] = a_Jun;
		monthsArray[6] = a_Jul;
		monthsArray[7] = a_Aug;
		monthsArray[8] = a_Sep;
		monthsArray[9] = a_Oct;
		monthsArray[10] = a_Nov;
		monthsArray[11] = a_Dec;

	}

	// Use this for initialization
	void Start () {
		SetSeasonal();
        eventController = transform.parent.transform.FindChild("EventController").gameObject;
        datePopup = GameObject.FindGameObjectWithTag("UI").transform.FindChild("Popups").transform.FindChild("Calendar_Popup").gameObject;

        calendarTab = GameObject.FindGameObjectWithTag("UI").transform.FindChild("Tabs").transform.FindChild("Calendar").gameObject;
        monthsHolder[0] = calendarTab.transform.FindChild("LayoutHolder").transform.FindChild("January").transform.FindChild("DayHolder").gameObject;
        monthsHolder[1] = calendarTab.transform.FindChild("LayoutHolder").transform.FindChild("February").transform.FindChild("DayHolder").gameObject;
        monthsHolder[2] = calendarTab.transform.FindChild("LayoutHolder").transform.FindChild("March").transform.FindChild("DayHolder").gameObject;
        monthsHolder[3] = calendarTab.transform.FindChild("LayoutHolder").transform.FindChild("April").transform.FindChild("DayHolder").gameObject;
        monthsHolder[4] = calendarTab.transform.FindChild("LayoutHolder").transform.FindChild("May").transform.FindChild("DayHolder").gameObject;
        monthsHolder[5] = calendarTab.transform.FindChild("LayoutHolder").transform.FindChild("June").transform.FindChild("DayHolder").gameObject;
        monthsHolder[6] = calendarTab.transform.FindChild("LayoutHolder").transform.FindChild("July").transform.FindChild("DayHolder").gameObject;
        monthsHolder[7] = calendarTab.transform.FindChild("LayoutHolder").transform.FindChild("August").transform.FindChild("DayHolder").gameObject;
        monthsHolder[8] = calendarTab.transform.FindChild("LayoutHolder").transform.FindChild("September").transform.FindChild("DayHolder").gameObject;
        monthsHolder[9] = calendarTab.transform.FindChild("LayoutHolder").transform.FindChild("October").transform.FindChild("DayHolder").gameObject;
        monthsHolder[10] = calendarTab.transform.FindChild("LayoutHolder").transform.FindChild("November").transform.FindChild("DayHolder").gameObject;
        monthsHolder[11] = calendarTab.transform.FindChild("LayoutHolder").transform.FindChild("December").transform.FindChild("DayHolder").gameObject;
        SetCalendar(2017, 1);
	}
	void SetSeasonal()
	{
		int current = 0;
		for(int i = 0; i < monthsArray.Length; i++)
		{
			for(int j = 0; j < numberOfdaysPerMonths[i]; j++)
			{
				seasonalTrends[current] = Random.Range(monthsArray[i][0],monthsArray[i][1]);;
				current++;
			}
		}
	}
    //function that displays all the dates base on the year.
    void SetCalendar(int year, int firstDay) 
    {
        //Sets the displayed year at the top of the tab. 
        calendarTab.transform.FindChild("txtYear").GetComponent<Text>().text = year.ToString();

        


        int dateAtCycle = 0;

        //12 months
        for (int i = 0; i < 12; i++)
        { 
            //check for february 29th.
            if(i == 2 && year%4 == 0)
            {
               numberOfdaysPerMonths[1] = 29;
            }

            if (firstDay > 1)
            {
                for (int k = 1; k < firstDay; k++)
                {
                    monthsHolder[i].transform.FindChild(k.ToString()).GetComponent<Button>().interactable = false;
                    monthsHolder[i].transform.FindChild(k.ToString()).transform.FindChild("Text").GetComponent<Text>().text = " ";
                }
            }
            int lastDay = 0;
            bool last = false;
            //for each days.
            for (int j = firstDay; j <= 42; j++) 
            {
                if (j <= numberOfdaysPerMonths[i]+firstDay-1)
                {
                    dateAtCycle++;
                    int month = i;
                    int day = (j-firstDay)+1;
                    int dateOfTheYear = dateAtCycle;
                    bool hasEvent = false;
                    monthsHolder[i].transform.FindChild(j.ToString()).transform.FindChild("Text").GetComponent<Text>().text = ((j - firstDay)+1).ToString();
                    if (CheckForEvent(dateAtCycle))//check if we have a special event for this date.
                    {
                        hasEvent = true;
                        monthsHolder[i].transform.FindChild(j.ToString()).GetComponent<Image>().color = Color.red;
                       
                    }
                    //add a listener on each button with the values of month,day,day of the year and a bool if we have special event.

                    monthsHolder[i].transform.FindChild(j.ToString()).GetComponent<Button>().onClick.AddListener(() => Display(month, day, dateOfTheYear, hasEvent));
                    if (!monthsHolder[i].transform.FindChild(j.ToString()).gameObject.activeInHierarchy) 
                    {
                        monthsHolder[i].transform.FindChild(j.ToString()).gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (!last)
                    {
                        lastDay = j-1 ;
                        last = true;
                    }
                    monthsHolder[i].transform.FindChild(j.ToString()).gameObject.SetActive(false);
                }

            }
            //calculate the first day for the next month.
            firstDay = (lastDay % 7)+1;
            if (firstDay == 0)
            {
                firstDay = 1;
            }
          
        }
    }
    //activates the Popup for date display. 
    public void Display(int month, int day, int dateInTheYear, bool hasEvent) 
    {
        datePopup.SetActive(true);
        
        if (hasEvent)
        {
            Debug.Log(dateInTheYear);
            GeneratedEvent currentEvent = eventController.GetComponent<PreRandomedEvents>().GetEvent(dateInTheYear);
            datePopup.transform.FindChild("txtDate").GetComponent<Text>().text = ((months)month).ToString() + " " + day.ToString() + " " + currentEvent.title;
            datePopup.transform.FindChild("IMG_Month").GetComponent<Image>().sprite = currentEvent.image;
            datePopup.transform.FindChild("Description_Panel").transform.FindChild("Text").GetComponent<Text>().text = currentEvent.description + " Expected bookings for the day: " + 
				(Mathf.Round((seasonalTrends[dateInTheYear]*BedroomBehaviour.allBedrooms.Count)/10f)*10f);
			//datePopup.transform.FindChild("IF_LYInquiry").transform.FindChild("Text").GetComponent<Text>().text = last yea numbers;
        }
        else 
        {
            datePopup.transform.FindChild("txtDate").GetComponent<Text>().text = ((months)month).ToString() + " " + day.ToString();
            datePopup.transform.FindChild("IMG_Month").GetComponent<Image>().sprite = ImagesOfMonth[month];
            datePopup.transform.FindChild("Description_Panel").transform.FindChild("Text").GetComponent<Text>().text = "No special events today." + " Expected bookings for the day: " + 
				(Mathf.Round((seasonalTrends[dateInTheYear]*BedroomBehaviour.allBedrooms.Count)/10f)*10f);
        }
    }

    bool CheckForEvent(int date) 
    {
        if(eventController.GetComponent<PreRandomedEvents>().GetEvent(date) != null)
        return true;
        else
            return false;
    }
}
