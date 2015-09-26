using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Advertisment : MonoBehaviour {




	float costOfAdvert = 0f;
	public void RunAdvert(Button type)
	{
		costOfAdvert = 0f;
		switch(type.name)
		{
		case "1":
			costOfAdvert = 500f;
			MasterReference.hotelExposure += 5f;
			MasterReference.hotelCorporateExposure += 5f;
			type.interactable = false;
			break;
		case "2":
			MasterReference.hotelExposure += 10f;
			MasterReference.hotelCorporateExposure += 5f;
			costOfAdvert = 1500f;
			type.interactable = false;
			break;
		case "3":
			MasterReference.hotelExposure += 10f;
			MasterReference.hotelCorporateExposure += 10f;
			costOfAdvert = 5000f;
			type.interactable = false;
			break;
		case "4":
			MasterReference.hotelExposure += 25f;
			MasterReference.hotelCorporateExposure += 10f;
			MasterReference.hotelTourismExposure += 5f;
			costOfAdvert = 50000f;
			type.interactable = false;
			break;
		case "5":
			MasterReference.hotelExposure += 25f;
			MasterReference.hotelCorporateExposure += 25f;
			MasterReference.hotelTourismExposure += 10f;
			costOfAdvert = 250000f;
			type.interactable = false;
			break;
		case "6":
			MasterReference.hotelExposure += 25f;
			MasterReference.hotelCorporateExposure += 25f;
			MasterReference.hotelTourismExposure += 25f;
			costOfAdvert = 2000000f;
			type.interactable = false;
			break;
		}
		MasterReference.accountsPayable += costOfAdvert;
	}
}
