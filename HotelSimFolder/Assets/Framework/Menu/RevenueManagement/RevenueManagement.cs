using UnityEngine;
using UnityEngine.UI;

public class RevenueManagement : MonoBehaviour {

	public GameObject revenueMngTab;

	bool isOverbooking = false;
	[SerializeField][Range(0f,50f)][Tooltip("Upper limit of overbooking as a percentage.")]
	int overbookPercent = 10;
	int currentOverbookPercent = 0;

	public float discountOnOccupancy()
	{
		GameObject fields = revenueMngTab.transform.FindChild("InputFields").gameObject;

		float lowOcc = float.Parse(fields.transform.FindChild("occupancyLow").GetComponent<InputField>().text);
		float midOcc = float.Parse(fields.transform.FindChild("occupancyMid").GetComponent<InputField>().text);
		float highOcc = float.Parse(fields.transform.FindChild("occupancyHigh").GetComponent<InputField>().text);
		float currentOcc = CheckOccupancy();
		if(lowOcc > currentOcc)
		{
			return float.Parse(fields.transform.FindChild("occLowRate").GetComponent<InputField>().text);
		}
		else if(midOcc > currentOcc)
		{
			return float.Parse(fields.transform.FindChild("occMidRate").GetComponent<InputField>().text);
		}
		else if(currentOcc > highOcc)
		{
			return float.Parse(fields.transform.FindChild("occHighRate").GetComponent<InputField>().text);
		}
		else
		{
			return 100f;
		}
	}
	float CheckOccupancy()
	{
		int count = 0;
		int bookedCount = 0;
		foreach (RoomStats aRoom in BedroomBehaviour.allBedrooms) 
		{
			count++;
			if(aRoom.daysOccupied !=0)
			{
				bookedCount++;
			}
		}
		float occupancy = bookedCount/count;
		return occupancy;
	}
	public void AllowOverbooking()
	{
		isOverbooking = !isOverbooking;
		if(isOverbooking)
		{
			currentOverbookPercent = overbookPercent;
		}
		else
		{
			currentOverbookPercent = 0;
		}
	}
	//Getter for overbooking boolean. Used inside reception loop to add extra.
	public int GetOverbooking()
	{
		return currentOverbookPercent;
	}
	public bool GetOverbooked()
	{
		return isOverbooking;
	}

}
