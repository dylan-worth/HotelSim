//simple class to hold all the data of running ad campaigns.
[System.Serializable]
public class AdCampaign {

    public Date startDate;//starting date of the campaign.
    public float cost;
    public int duration;//duration in months.
    public int remainingDuration;

    public string reason;//Lets the player explain why they ran this campaign at this time and why they spent that money.

    public float hotelExposureMonthlyBonus;
    public float hotelCorporateExposureMonthlyBonus;
    public float hotelTourismExposureMonthlyBonus;

    public AdCampaign() { }//Default constructure.
    public AdCampaign(Date current, float price, int lenght, string comments, float hExposure, float cExposure, float tExposure)
    {
        startDate = current;
        cost = price;
        duration = lenght;
        reason = comments;
        hotelExposureMonthlyBonus = hExposure;
        hotelCorporateExposureMonthlyBonus = cExposure;
        hotelTourismExposureMonthlyBonus = tExposure;
    }

}
