using UnityEngine;
using System.Collections;
using System;

public enum staffType{ DepartmentHead, HotelServices, FoodAndBeverages, FrontDesk, Conference, Others }

public class StaffMember
{
	public staffType Type;
	public float Training;
	//public int Age;
	public float PayRate;
	public float Happiness;
	public float Productivity;
	public float Overtime;
	public float HoursWorked;
	public int Benefits;
	public int Experience=0;

	int baseProductivity = 50;

	public StaffMember (staffType newType,
	                    float newTraining = 1,
	                    float newPayRate = 12,
	                    int newBenefits = 1                                                                                                                                                                                                                                                                                                                                                                                                                             ,
	                    int newExperience = 0,
	                    float newHappiness = 80,
	                    float newProductivity = 50)
	{
		Type = newType;
		Training = newTraining;
		PayRate = newPayRate;
		Benefits = newBenefits;
		Experience = newExperience;
		Happiness = newHappiness;
		Productivity = newProductivity;
	}


	public float CalculateProd ()
	{
		Productivity = (baseProductivity * Training)*Happiness/100;
		return Productivity;
	}
	public void SetPay(payBandEnum payBand)
	{
		switch(Type)
		{
		case staffType.DepartmentHead:
			PayRate = MasterReference.basePayDepartmentHead * (((float)payBand/10)+1);
			break;
		case staffType.HotelServices:
			PayRate = MasterReference.basePayHotelServices * (((float)payBand/10)+1);
			break;
		case staffType.FoodAndBeverages:
			PayRate = MasterReference.basePayFoodAndBeverages * (((float)payBand/10)+1);
			break;
		case staffType.FrontDesk:
			PayRate = MasterReference.basePayFrontDesk * (((float)payBand/10)+1);
			break;
		case staffType.Conference:
			PayRate = MasterReference.basePayConference * (((float)payBand/10)+1);
			break;
		case staffType.Others:
			PayRate = MasterReference.basePayOthers * (((float)payBand/10)+1);
			break;
		}
	}
}