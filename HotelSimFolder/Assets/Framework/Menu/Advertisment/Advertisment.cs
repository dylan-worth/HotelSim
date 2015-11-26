using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Advertisment : MonoBehaviour {//Handles the expenses and effect of advertissement.

    [System.NonSerialized]
    public float hotelExposureBonus;
    [System.NonSerialized]
    public float hotelCorporateExposureBonus;
    [System.NonSerialized]
    public float hotelTourismExposureBonus;

    [SerializeField]
    [Tooltip("Max amount of exposure for any types.")]
    float max_Exposure;

    [SerializeField]
    [Tooltip("Hotel exposure normal decay every month.")]
    float decay_HotelExposure;
    [SerializeField]
    [Tooltip("Resting point beyound which decay doesn't happen.")]
    float min_HotelExposure;
    [SerializeField]
    [Tooltip("Hotel exposure corporate decay every month.")]
    float decay_CorporateExposure;
    [SerializeField]
    [Tooltip("Resting point beyound which decay doesn't happen.")]
    float min_CorporateExposure;
    [SerializeField]
    [Tooltip("Hotel exposure tourism decay every month.")]
    float decay_TourismExposure;
    [SerializeField]
    [Tooltip("Resting point beyound which decay doesn't happen.")]
    float min_TourismExposure;
    [SerializeField]
    [Tooltip("Text to display on the button.")]
    string btn_1;

    [SerializeField]
    [Tooltip("Total costs as a one of for a local campaign.")]
    float localCampaignCost;
    [SerializeField]
    [Tooltip("Duration of the campaign in months.")]
    int lcDuration;
    [SerializeField]
    [Tooltip("Effects of one month's worth of advertising.")]
    float lcHotelExposureBonus;
    [SerializeField]
    [Tooltip("Effects of one month's worth of advertising.")]
    float lcCorporateExposureBonus;
    [SerializeField]
    [Tooltip("Effects of one month's worth of advertising.")]
    float lcTourismExposureBonus;
    [SerializeField]
    [Tooltip("Text to display on the button.")]
    string btn_2;

    [SerializeField]
    [Tooltip("Total costs as a one of for a regional campaign.")]
    float regionalCampaignCost;
    [SerializeField]
    [Tooltip("Duration of the campaign in months.")]
    int rcDuration;
    [SerializeField]
    [Tooltip("Effects of one month's worth of advertising.")]
    float rcHotelExposureBonus;
    [SerializeField]
    [Tooltip("Effects of one month's worth of advertising.")]
    float rcCorporateExposureBonus;
    [SerializeField]
    [Tooltip("Effects of one month's worth of advertising.")]
    float rcTourismExposureBonus;
    [SerializeField]
    [Tooltip("Text to display on the button.")]
    string btn_3;

    [SerializeField]
    [Tooltip("Total costs as a one of for a statewide campaign.")]
    float statewideCampaignCost;
    [SerializeField]
    [Tooltip("Duration of the campaign in months.")]
    int scDuration;
    [SerializeField]
    [Tooltip("Effects of one month's worth of advertising.")]
    float scHotelExposureBonus;
    [SerializeField]
    [Tooltip("Effects of one month's worth of advertising.")]
    float scCorporateExposureBonus;
    [SerializeField]
    [Tooltip("Effects of one month's worth of advertising.")]
    float scTourismExposureBonus;
    [SerializeField]
    [Tooltip("Text to display on the button.")]
    string btn_4;

    [SerializeField]
    [Tooltip("Total costs as a one of for a contrywide campaign.")]
    float countrywideCampaignCost;
    [SerializeField]
    [Tooltip("Duration of the campaign in months.")]
    int ccDuration;
    [SerializeField]
    [Tooltip("Effects of one month's worth of advertising.")]
    float ccHotelExposureBonus;
    [SerializeField]
    [Tooltip("Effects of one month's worth of advertising.")]
    float ccCorporateExposureBonus;
    [SerializeField]
    [Tooltip("Effects of one month's worth of advertising.")]
    float ccTourismExposureBonus;
    [SerializeField]
    [Tooltip("Text to display on the button.")]
    string btn_5;

    [SerializeField]
    [Tooltip("Total costs as a one of for a worldwide campaign.")]
    float worldwideCampaignCost;
    [SerializeField]
    [Tooltip("Duration of the campaign in months.")]
    int wcDuration;
    [SerializeField]
    [Tooltip("Effects of one month's worth of advertising.")]
    float wcHotelExposureBonus;
    [SerializeField]
    [Tooltip("Effects of one month's worth of advertising.")]
    float wcCorporateExposureBonus;
    [SerializeField]
    [Tooltip("Effects of one month's worth of advertising.")]
    float wcTourismExposureBonus;
    [SerializeField]
    [Tooltip("Text to display on the button.")]
    string btn_6;


    List<AdCampaign> currentAdCampaigns = new List<AdCampaign>();
    List<AdCampaign> archivedAdCampaigns = new List<AdCampaign>();

    string[] buttonInnerText = new string[6];

    GameObject advertTab;
    GameObject buttons;

    void Awake()
    {
        buttonInnerText[0] = btn_1;
        buttonInnerText[1] = btn_2;
        buttonInnerText[2] = btn_3;
        buttonInnerText[3] = btn_4;
        buttonInnerText[4] = btn_5;
        buttonInnerText[5] = btn_6;
    }

    void Start()
    {
        advertTab = GameObject.FindGameObjectWithTag("UI").transform.FindChild("Tabs").transform.FindChild("Advertisment").gameObject;
        buttons = advertTab.transform.FindChild("Campaign_Buttons").gameObject;
        SetTab();
    }
    void SetTab()
    {
        for (int i = 1; i <= 6; i++)
        {
            buttons.transform.FindChild(i.ToString()).transform.FindChild("Text").GetComponent<Text>().text = buttonInnerText[i-1];
        }
    }

    public void Tick()//ticks all campaing by one month/period, decreases exposure due to lost of interest and increases exposure due to campaigns currently running.
    {
        float monthlyCost = 0f;
        
        for (int i = 0; i < currentAdCampaigns.Count; i++)
        {
            //calculate total monthly costs.
            monthlyCost += currentAdCampaigns[i].cost;
            //adds all 3 types of bonus exposures.
            MasterReference.hotelExposure += currentAdCampaigns[i].hotelExposureMonthlyBonus;
            MasterReference.hotelCorporateExposure += currentAdCampaigns[i].hotelCorporateExposureMonthlyBonus;
            MasterReference.hotelTourismExposure += currentAdCampaigns[i].hotelTourismExposureMonthlyBonus;


            //counts the duration down for all campaigns.
            currentAdCampaigns[i].remainingDuration--;
            if(currentAdCampaigns[i].remainingDuration <= 0)
            {
                buttons.transform.FindChild(currentAdCampaigns[i].typeOfCampaign.ToString()).gameObject.GetComponent<Button>().interactable = true;
                archivedAdCampaigns.Add(currentAdCampaigns[i]);//remove from current list and add to archived.
                currentAdCampaigns.Remove(currentAdCampaigns[i]);
            }
        }
        MasterReference.accountsPayable += monthlyCost;//should add to a different variable so we can track the total spent on ads.


        //removes some exposure each months due to decaying exposure. Can't get lower than min decay.
        MasterReference.hotelExposure = Mathf.Clamp(MasterReference.hotelExposure -= decay_HotelExposure, min_HotelExposure, max_Exposure);
        MasterReference.hotelCorporateExposure = Mathf.Clamp(MasterReference.hotelCorporateExposure -= decay_CorporateExposure, min_CorporateExposure, max_Exposure);
        MasterReference.hotelTourismExposure = Mathf.Clamp(MasterReference.hotelTourismExposure -= decay_TourismExposure, min_TourismExposure, max_Exposure);


    }

   // float costOfAdvert = 0f;
	public void RunAdvert(Button type)//Based on the button pressed.
	{
		//costOfAdvert = 0f;
		switch(type.name)
		{
		case "1"://local campaign.
                AdCampaign newLocal = new AdCampaign(Calendar.GetDate(),
                                                        int.Parse(type.name), 
                                                        localCampaignCost, 
                                                        lcDuration, 
                                                        "placeholder", 
                                                        lcHotelExposureBonus,
                                                        lcCorporateExposureBonus,
                                                        lcTourismExposureBonus);
                currentAdCampaigns.Add(newLocal);
            
			type.interactable = false;
			break;
		case "2"://regional campaign.
                AdCampaign newRegional = new AdCampaign(Calendar.GetDate(), 
                                                        int.Parse(type.name),
                                                        regionalCampaignCost,
                                                        rcDuration,
                                                        "placeholder",
                                                        rcHotelExposureBonus,
                                                        rcCorporateExposureBonus,
                                                        rcTourismExposureBonus);
                currentAdCampaigns.Add(newRegional);
               
                type.interactable = false;
                break;
		case "3"://statewide campaign.
                AdCampaign newSateWide = new AdCampaign(Calendar.GetDate(), 
                                                       int.Parse(type.name),
                                                       statewideCampaignCost,
                                                       scDuration,
                                                       "placeholder",
                                                       scHotelExposureBonus,
                                                       scCorporateExposureBonus,
                                                       scTourismExposureBonus);
                currentAdCampaigns.Add(newSateWide);
              
                type.interactable = false;
                break;
		case "4"://country wide campaign.
                AdCampaign newCountrywide = new AdCampaign(Calendar.GetDate(), 
                                                      int.Parse(type.name),
                                                      countrywideCampaignCost,
                                                      ccDuration,
                                                      "placeholder",
                                                      ccHotelExposureBonus,
                                                      ccCorporateExposureBonus,
                                                      ccTourismExposureBonus);
                currentAdCampaigns.Add(newCountrywide);
              
                type.interactable = false;
                break;
		case "5"://worldwide campaign.
                AdCampaign newWorldwide = new AdCampaign(Calendar.GetDate(), 
                                                      int.Parse(type.name),
                                                      worldwideCampaignCost,
                                                      wcDuration,
                                                      "placeholder",
                                                      wcHotelExposureBonus,
                                                      wcCorporateExposureBonus,
                                                      wcTourismExposureBonus);
                currentAdCampaigns.Add(newWorldwide);
               
                type.interactable = false;
                break;
		case "6"://later add custom campaigns where player can set the duration, target audiance and would give out a custom cost.
                 
                 type.interactable = false;
                Debug.LogWarning("Currently no lvl 6 campaign type.");
			break;
		}
		//MasterReference.accountsPayable += costOfAdvert;//Add the costs directly to accountsPayable. Should add to a new variable so we can track.
	}
}
