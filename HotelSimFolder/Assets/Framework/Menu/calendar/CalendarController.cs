using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CalendarController : MonoBehaviour {


    GameObject calendarTab;
    GameObject[] monthsHolder = new GameObject[12];
    GameObject datePopup;
    GameObject eventController;


    [SerializeField][Tooltip("Array of images for each months to display. DO NOT CHANGE SIZE VALUE!")]
    Sprite[] ImagesOfMonth = new Sprite[12];
	// Use this for initialization
	void Start () {
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
    //function that displays all the dates base on the year.
    void SetCalendar(int year, int firstDay) 
    {
        //Sets the displayed year at the top of the tab. 
        calendarTab.transform.FindChild("txtYear").GetComponent<Text>().text = year.ToString();
        date currentDate =  Calendar.getDate().deepCopy();
        int[] numberOfdaysPerMonths = new int[12]{31,28,31,30,31,30,31,31,30,31,30,31};


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
            datePopup.transform.FindChild("Description_Panel").transform.FindChild("Text").GetComponent<Text>().text = currentEvent.description;
        }
        else 
        {
            datePopup.transform.FindChild("txtDate").GetComponent<Text>().text = ((months)month).ToString() + " " + day.ToString();
            datePopup.transform.FindChild("IMG_Month").GetComponent<Image>().sprite = ImagesOfMonth[month];
            datePopup.transform.FindChild("Description_Panel").transform.FindChild("Text").GetComponent<Text>().text = "No special events today.";
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
