//simple class to hold all the data of running ad campaigns.
[System.Serializable]
public class AdCampaign {

    public Date startDate;//starting date of the campaign.
    public int typeOfCampaign;//what type of campaign(1 = Local; 2 = Regional; 3 = Statewide; 4 = CountryWide; 5 = WorldWide; 6 = Custom)
    public float cost;
    public int duration;//duration in months.
    public int remainingDuration;

    public string reason;//Lets the player explain why they ran this campaign at this time and why they spent that money.

    public float hotelExposureMonthlyBonus;
    public float hotelCorporateExposureMonthlyBonus;
    public float hotelTourismExposureMonthlyBonus;

    public AdCampaign() { }//Default constructure.
    public AdCampaign(Date current, int type, float price, int lenght, string comments, float hExposure, float cExposure, float tExposure)
    {
        startDate = current;
        typeOfCampaign = type;
        cost = price;
        duration = lenght;
        reason = comments;
        hotelExposureMonthlyBonus = hExposure;
        hotelCorporateExposureMonthlyBonus = cExposure;
        hotelTourismExposureMonthlyBonus = tExposure;
        
    }

}
