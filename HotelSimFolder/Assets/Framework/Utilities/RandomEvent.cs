using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class GeneratedEvent
{
    [Tooltip("Displayed at the top of the event panel.")]
    public string title;
    [Tooltip("Displayed under the image inside event panel.")]
    public string description;
    [Tooltip("The image Displayed in the event panel.")]
    public Sprite image;
    [Tooltip("Background color of the panel.")]
    public Color panelColor;  
    [Tooltip("Number of options to select for current event.")][Range(0,3)]
    public int num_Options;
    //----------------------------------------------VALUES TO BUILD EVENT--------------------------------------//

    //OPTION ONE:
    [Tooltip("Text for button option one.")]
    public string txt_Btn_One;
    [Tooltip("Hotel exposure change for choosing option 1")]
    public float hotelExposureModifier_Opt_1;
    [Tooltip("Hotel Coporate exposure change for choosing option 1")]
    public float hotelCorporateExposureModifier_Opt_1;
    [Tooltip("Hotel tourism exposure change for choosing option 1")]
    public float hotelTourismExposureModifier_Opt_1;
    [Tooltip("Immidiate spending caused by choosing option 1. Negative value = a cash intake.")]
    public float directCost_Opt_1;
    [Tooltip("Amount to pay each period caused by choosing option 1. Negative value = a cash intake.")]
    public float AmountOverTime_Opt_1;
    [Tooltip("Duration of payments caused by choosing option 1.")]
    [Range(0, 60)]
    public int payementDuration_Opt_1;
    //OPTION TWO:
    [Tooltip("Text for button option two.")]
    public string txt_Btn_Two;
    [Tooltip("Hotel exposure change for choosing option 2")]
    public float hotelExposureModifier_Opt_2;
    [Tooltip("Hotel Coporate exposure change for choosing option 2")]
    public float hotelCorporateExposureModifier_Opt_2;
    [Tooltip("Hotel tourism exposure change for choosing option 2")]
    public float hotelTourismExposureModifier_Opt_2;
    [Tooltip("Immidiate spending caused by choosing option 2. Negative value = a cash intake.")]
    public float directCost_Opt_2;
    [Tooltip("Amount to pay each period caused by choosing option 2. Negative value = a cash intake.")]
    public float AmountOverTime_Opt_2;
    [Tooltip("Duration of payments caused by choosing option 2.")]
    [Range(0, 60)]
    public int payementDuration_Opt_2;
    //OPTION THREE:
    [Tooltip("Text for button option three.")]
    public string txt_Btn_Three;
    [Tooltip("Hotel exposure change for choosing option 3")]
    public float hotelExposureModifier_Opt_3;
    [Tooltip("Hotel Coporate exposure change for choosing option 3")]
    public float hotelCorporateExposureModifier_Opt_3;
    [Tooltip("Hotel tourism exposure change for choosing option 3")]
    public float hotelTourismExposureModifier_Opt_3;
    [Tooltip("Immidiate spending caused by choosing option 3. Negative value = a cash intake.")]
    public float directCost_Opt_3;
    [Tooltip("Amount to pay each period caused by choosing option 3. Negative value = a cash intake.")]
    public float AmountOverTime_Opt_3;
    [Tooltip("Duration of payments caused by choosing option 3.")][Range(0,60)]
    public int payementDuration_Opt_3;
    //For events that will be randomed.
    [Tooltip("Months during which this event can happen. 0 = January & 11 = December. EDIT SIZE TO ADD MORE MONTHS.")]
    public int[] probableMonths;

	string response;
	public string GetResponse()
	{
		return response;
	}
	public void SetResponse(string resp)
	{
		response = resp;
	}

    public GeneratedEvent() { }
    
}

public class RandomEvent : MonoBehaviour {
    
    [System.NonSerialized]
    public List<GeneratedEvent> listOfEvents = new List<GeneratedEvent>();

    [SerializeField]
    [Range(0f, 100f)]
    [Tooltip("Percentage chance to trigger a random event each week.")]
    float eventChance;
    [SerializeField]
    GeneratedEvent eventOne;
    [SerializeField]
    GeneratedEvent eventTwo;
    [SerializeField]
    GeneratedEvent eventThree;
    [SerializeField]
    GeneratedEvent eventFour;
    [SerializeField]
    GeneratedEvent eventFive;
    [SerializeField]
    GeneratedEvent eventSix;
    [SerializeField]
    GeneratedEvent eventSeven;
    [SerializeField]
    GeneratedEvent eventEight;
    [SerializeField]
    GeneratedEvent eventNine;
    [SerializeField]
    GeneratedEvent eventTen;

    GameObject eventPanel;
    GeneratedEvent currentEvent;

    void Awake() 
    {
       
        listOfEvents.Add(eventOne); listOfEvents.Add(eventTwo); listOfEvents.Add(eventThree);
        listOfEvents.Add(eventFour); listOfEvents.Add(eventFive); listOfEvents.Add(eventSix);
        listOfEvents.Add(eventSeven); listOfEvents.Add(eventEight); listOfEvents.Add(eventNine);
        listOfEvents.Add(eventTen);
        eventPanel = GameObject.FindWithTag("UI").transform.FindChild("Popups").transform.FindChild("EventPanel").gameObject;

    }
    
    public void InitiateRandomEvent() 
    {   
        if (Random.Range(0f, 100f) <= eventChance)//checks the eventchance percentage.
        {
            Time.timeScale = 0f;//pauses the passage of time.
            //First we desactivate the rest of the UI so we can't create conflicts.
            GameObject.FindWithTag("UI").transform.FindChild("Tabs").gameObject.SetActive(false);
            GameObject.FindWithTag("UI").transform.FindChild("OverWorld").gameObject.SetActive(false);
            //We select a random event from our list.
            currentEvent = GetRandomEvent();
            eventPanel.transform.FindChild("IMG_Main").GetComponent<Image>().sprite = currentEvent.image;
            eventPanel.transform.FindChild("Txt_Title").GetComponent<Text>().text = currentEvent.title;
            eventPanel.transform.FindChild("Txt_Description").GetComponent<Text>().text = currentEvent.description;

            if (currentEvent.num_Options > 0)
            {
                eventPanel.transform.FindChild("Btn_Option_1").gameObject.SetActive(true);
                eventPanel.transform.FindChild("Btn_Option_1").transform.FindChild("Text").GetComponent<Text>().text = currentEvent.txt_Btn_One;
            }
            else { eventPanel.transform.FindChild("Btn_Option_1").gameObject.SetActive(false); }
            if (currentEvent.num_Options > 1)
            {
                eventPanel.transform.FindChild("Btn_Option_2").gameObject.SetActive(true);
                eventPanel.transform.FindChild("Btn_Option_2").transform.FindChild("Text").GetComponent<Text>().text = currentEvent.txt_Btn_Two;
            }
            else { eventPanel.transform.FindChild("Btn_Option_2").gameObject.SetActive(false); }
            if (currentEvent.num_Options > 2)
            {
                eventPanel.transform.FindChild("Btn_Option_3").gameObject.SetActive(true);
                eventPanel.transform.FindChild("Btn_Option_3").transform.FindChild("Text").GetComponent<Text>().text = currentEvent.txt_Btn_Three;
            }
            else { eventPanel.transform.FindChild("Btn_Option_3").gameObject.SetActive(false); }



            eventPanel.GetComponent<Image>().color = currentEvent.panelColor;

            eventPanel.SetActive(true);
        }
    }

    public void SelectionMade(int selected) 
    {
        //First we reactivate the rest of the UI.
        GameObject.FindWithTag("UI").transform.FindChild("Tabs").gameObject.SetActive(true);
        GameObject.FindWithTag("UI").transform.FindChild("OverWorld").gameObject.SetActive(true);
        switch (selected) 
        {
            case 1:
                //Modifiers for exposure.
                MasterReference.hotelExposure += currentEvent.hotelExposureModifier_Opt_1;
                MasterReference.hotelCorporateExposure += currentEvent.hotelCorporateExposureModifier_Opt_1;
                MasterReference.hotelTourismExposure += currentEvent.hotelTourismExposureModifier_Opt_1;
                //Costs effectors.
                if (currentEvent.directCost_Opt_1 >= 0)
                {
                    MasterReference.accountsPayable += currentEvent.directCost_Opt_1;
                }
                else 
                {//Negative value gives the hotel bonus cash.
                    MasterReference.accountsReceivable += currentEvent.directCost_Opt_1;
                }
                //Cost over time.
                if (currentEvent.AmountOverTime_Opt_1 > 0 && currentEvent.payementDuration_Opt_1 > 0)
                {
                    AddNewPaymentOvertime(currentEvent.payementDuration_Opt_1, currentEvent.AmountOverTime_Opt_1);
                }
                else
                {//Negative value gives the hotel bonus cash.
                    AddNewPaymentOvertime(currentEvent.payementDuration_Opt_1, currentEvent.AmountOverTime_Opt_1);
                }
                break;
            case 2:
                //Modifiers for exposure.
                MasterReference.hotelExposure += currentEvent.hotelExposureModifier_Opt_2;
                MasterReference.hotelCorporateExposure += currentEvent.hotelCorporateExposureModifier_Opt_2;
                MasterReference.hotelTourismExposure += currentEvent.hotelTourismExposureModifier_Opt_2;
                //Costs effectors.
                if (currentEvent.directCost_Opt_2 >= 0)
                {
                    MasterReference.accountsPayable += currentEvent.directCost_Opt_2;
                }
                else
                {//Negative value gives the hotel bonus cash.
                    MasterReference.accountsReceivable += currentEvent.directCost_Opt_2;
                }
                //Cost over time.
                if (currentEvent.AmountOverTime_Opt_2 > 0 && currentEvent.payementDuration_Opt_2 > 0)
                {
                    AddNewPaymentOvertime(currentEvent.payementDuration_Opt_2, currentEvent.AmountOverTime_Opt_2);
                }
                else
                {//Negative value gives the hotel bonus cash.
                    AddNewPaymentOvertime(currentEvent.payementDuration_Opt_2, currentEvent.AmountOverTime_Opt_2);
                }
                break;
            case 3:
                //Modifiers for exposure.
                MasterReference.hotelExposure += currentEvent.hotelExposureModifier_Opt_3;
                MasterReference.hotelCorporateExposure += currentEvent.hotelCorporateExposureModifier_Opt_3;
                MasterReference.hotelTourismExposure += currentEvent.hotelTourismExposureModifier_Opt_3;
                //Costs effectors.
                if (currentEvent.directCost_Opt_3 >= 0)
                {
                    MasterReference.accountsPayable += currentEvent.directCost_Opt_3;
                }
                else
                {//Negative value gives the hotel bonus cash.
                    MasterReference.accountsReceivable += currentEvent.directCost_Opt_3;
                }
                //Cost over time.
                if (currentEvent.AmountOverTime_Opt_3 > 0 && currentEvent.payementDuration_Opt_3 > 0)
                {
                    AddNewPaymentOvertime(currentEvent.payementDuration_Opt_3, currentEvent.AmountOverTime_Opt_3);
                }
                else
                {//Negative value gives the hotel bonus cash.
                    AddNewPaymentOvertime(currentEvent.payementDuration_Opt_3, currentEvent.AmountOverTime_Opt_3);
                }
                break;
        }
        Time.timeScale = 1f;
        eventPanel.SetActive(false);
    }
   

    GeneratedEvent GetRandomEvent()
    {
        return listOfEvents[0];
        //return listOfEvents[Random.Range(0,11)];
    }

    

    public void Test() 
    {
        InitiateRandomEvent();
    }

    void AddNewPaymentOvertime(int duration, float amount) 
    {
    
    }

}
