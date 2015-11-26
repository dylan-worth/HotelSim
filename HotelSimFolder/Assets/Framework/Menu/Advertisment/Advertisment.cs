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



    List<AdCampaign> currentAdCampaigns = new List<AdCampaign>();
    List<AdCampaign> archivedAdCampaigns = new List<AdCampaign>();


    public void Tick()//ticks all campaing by one month/period, decreases exposure due to lost of interest and increases exposure due to campaigns currently running.
    {
        float monthlyCost = 0f;
        for (int i = 0; i < currentAdCampaigns.Count; i++)
        {
            monthlyCost += currentAdCampaigns[i].cost;
            currentAdCampaigns[i].remainingDuration--;
            if(currentAdCampaigns[i].remainingDuration <= 0)
            { }
        }
    }

   // float costOfAdvert = 0f;
	public void RunAdvert(Button type)//Based on the button pressed.
	{
		//costOfAdvert = 0f;
		switch(type.name)
		{
		case "1"://local campaign.
                AdCampaign newLocal = new AdCampaign(Calendar.GetDate(),
                                                        localCampaignCost, 
                                                        lcDuration, 
                                                        "placeholder", 
                                                        lcHotelExposureBonus,
                                                        lcCorporateExposureBonus,
                                                        lcTourismExposureBonus);
                currentAdCampaigns.Add(newLocal);
                /*
			costOfAdvert = 500f;
			MasterReference.hotelExposure += 5f;
			MasterReference.hotelCorporateExposure += 5f;
			type.interactable = false;*/
			break;
		case "2"://regional campaign.
                AdCampaign newRegional = new AdCampaign(Calendar.GetDate(),
                                                        regionalCampaignCost,
                                                        rcDuration,
                                                        "placeholder",
                                                        rcHotelExposureBonus,
                                                        rcCorporateExposureBonus,
                                                        rcTourismExposureBonus);
                currentAdCampaigns.Add(newRegional);
                /*MasterReference.hotelExposure += 10f;
                MasterReference.hotelCorporateExposure += 5f;
                costOfAdvert = 1500f;
                type.interactable = false;*/
                break;
		case "3"://statewide campaign.
                AdCampaign newSateWide = new AdCampaign(Calendar.GetDate(),
                                                       statewideCampaignCost,
                                                       scDuration,
                                                       "placeholder",
                                                       scHotelExposureBonus,
                                                       scCorporateExposureBonus,
                                                       scTourismExposureBonus);
                currentAdCampaigns.Add(newSateWide);
                /*MasterReference.hotelExposure += 10f;
                MasterReference.hotelCorporateExposure += 10f;
                costOfAdvert = 5000f;
                type.interactable = false;*/
                break;
		case "4"://country wide campaign.
                AdCampaign newCountrywide = new AdCampaign(Calendar.GetDate(),
                                                      countrywideCampaignCost,
                                                      ccDuration,
                                                      "placeholder",
                                                      ccHotelExposureBonus,
                                                      ccCorporateExposureBonus,
                                                      ccTourismExposureBonus);
                currentAdCampaigns.Add(newCountrywide);
                /*MasterReference.hotelExposure += 25f;
                MasterReference.hotelCorporateExposure += 10f;
                MasterReference.hotelTourismExposure += 5f;
                costOfAdvert = 50000f;
                type.interactable = false;*/
                break;
		case "5"://worldwide campaign.
                AdCampaign newWorldwide = new AdCampaign(Calendar.GetDate(),
                                                      worldwideCampaignCost,
                                                      wcDuration,
                                                      "placeholder",
                                                      wcHotelExposureBonus,
                                                      wcCorporateExposureBonus,
                                                      wcTourismExposureBonus);
                currentAdCampaigns.Add(newWorldwide);
                /*MasterReference.hotelExposure += 25f;
                MasterReference.hotelCorporateExposure += 25f;
                MasterReference.hotelTourismExposure += 10f;
                costOfAdvert = 250000f;
                type.interactable = false;*/
                break;
		case "6"://later add custom campaigns where player can set the duration, target audiance and would give out a custom cost.
                 /*MasterReference.hotelExposure += 25f;
                 MasterReference.hotelCorporateExposure += 25f;
                 MasterReference.hotelTourismExposure += 25f;
                 costOfAdvert = 2000000f;
                 type.interactable = false;*/
                Debug.LogWarning("Currently no lvl 6 campaign type.");
			break;
		}
		//MasterReference.accountsPayable += costOfAdvert;//Add the costs directly to accountsPayable. Should add to a new variable so we can track.
	}
}
