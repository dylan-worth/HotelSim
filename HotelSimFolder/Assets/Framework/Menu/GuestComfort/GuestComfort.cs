using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuestComfort : MonoBehaviour {

	public ToggleGroup comp;
	public ToggleGroup entertainment;
	public InputField costs;
	public InputField popularity;

	public float monthlyCosts = 0;
	int compLevel=0;
	int entLevel=0;
	void OnEnable()
	{
		costs.text = "$" + monthlyCosts.ToString ();
		popularity.text = MasterReference.hotelPopularity.ToString ();
	}

	public void RefreshValue(Toggle selected)
	{

		int choice = int.Parse(selected.name);
		if (choice <= 4)
		{
			int diff = choice - compLevel;
			MasterReference.hotelPopularity += diff;
			monthlyCosts += (float)(diff*50);
			compLevel = choice;
		} 
		else 
		{
			choice -=4;
			int diff = choice - entLevel;
			MasterReference.hotelPopularity += diff;
			monthlyCosts += (float)(diff*50);
			entLevel = choice;
		}
		Debug.Log (MasterReference.hotelPopularity);
		costs.text = "$" + monthlyCosts.ToString ();
		popularity.text = MasterReference.hotelPopularity.ToString ();
	}

	public void SetNewOption(Toggle newSelection)
	{

		switch(newSelection.name)
		{
		case "Concierge":
			if(newSelection.isOn)
			{
				monthlyCosts += 500f;
				MasterReference.hotelPopularity += 10f;
			}
			else
			{
				MasterReference.hotelPopularity += -10f;
				monthlyCosts += -500f;
			}
			break;
		case "RoomService":
			if(newSelection.isOn)
			{
				monthlyCosts += 50f;
				MasterReference.hotelPopularity += 5f;
			}
			else
			{
				monthlyCosts += -50f;
				MasterReference.hotelPopularity += -5f;
			}
			break;
		case "MiniBar":
			if(newSelection.isOn)
			{
				monthlyCosts += 50f;
				MasterReference.hotelPopularity += 3f;
			}
			else
			{
				monthlyCosts += -50f;
				MasterReference.hotelPopularity += -3f;
			}
			break;
		case "Wifi":
			if(newSelection.isOn)
			{
				monthlyCosts += 200f;
				MasterReference.hotelPopularity += 4f;
			}
			else
			{
				monthlyCosts += -200f;
				MasterReference.hotelPopularity += -4f;
			}
			break;
		case "QuickCheckOut":
			if(newSelection.isOn)
			{
				monthlyCosts += 75f;
				MasterReference.hotelPopularity += 2f;
			}
			else
			{
				monthlyCosts += -75f;
				MasterReference.hotelPopularity += -2f;
			}
			break;
		case "BusinessService":
			if(newSelection.isOn)
			{
				monthlyCosts += 100f;
				MasterReference.hotelPopularity += 3f;
			}
			else
			{
				monthlyCosts += -100f;
				MasterReference.hotelPopularity += -3f;
			}
			break;
		case "ExtraCarParkingArea":if(newSelection.isOn)
			{
				monthlyCosts += 100f;
				MasterReference.hotelPopularity += 3f;
			}
			else
			{
				monthlyCosts += -100f;
				MasterReference.hotelPopularity += -3f;
			}
			break;
		case "HotelShop":
			if(newSelection.isOn)
			{
				monthlyCosts += 100f;
				MasterReference.hotelPopularity += 2f;
			}
			else
			{
				monthlyCosts += -100f;
				MasterReference.hotelPopularity += -2f;
			}
			break;
		case "RoomExtension":
			if(newSelection.isOn)
			{
				monthlyCosts += 100f;
				MasterReference.hotelPopularity += 4f;
			}
			else
			{
				monthlyCosts += -100f;
				MasterReference.hotelPopularity += -4f;
			}
			break;
		case "LeisureClub":
			if(newSelection.isOn)
			{
				monthlyCosts += 500f;
				MasterReference.hotelPopularity += 3f;
			}
			else
			{
				monthlyCosts += -500f;
				MasterReference.hotelPopularity += -3f;
			}
			break;
		case "ConferenceFunctionRms":
			if(newSelection.isOn)
			{
				monthlyCosts += 100f;
				MasterReference.hotelPopularity += 3f;
			}
			else
			{
				monthlyCosts += -100f;
				MasterReference.hotelPopularity += -3f;
			}
			break;
		case "SecondBar":
			if(newSelection.isOn)
			{
				monthlyCosts += 50f;
				MasterReference.hotelPopularity += 4f;
			}
			else
			{
				monthlyCosts += -50f;
				MasterReference.hotelPopularity += -4f;
			}
			break;

		}
		popularity.text = MasterReference.hotelPopularity.ToString ();
		costs.text = "$" + monthlyCosts.ToString ();
		MasterReference.guessComfortMonthlySpending = monthlyCosts;

	}
}

