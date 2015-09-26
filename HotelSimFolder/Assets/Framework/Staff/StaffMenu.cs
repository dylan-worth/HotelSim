using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum payBandEnum {nonePay, PayBandOne , PayBandTwo, PayBandThree }

public class StaffingLog{

	public int departmentHeads = 0;
	public payBandEnum departmentHeadsP = payBandEnum.PayBandOne;
	public int hotelService = 0;
	public payBandEnum hotelServiceP = payBandEnum.PayBandOne;
	public int foodAndBev = 0;
	public payBandEnum foodAndBevP = payBandEnum.PayBandOne;
	public int frontDesk = 0;
	public payBandEnum frontDeskP = payBandEnum.PayBandOne;
	public int conferenceAndBanquet = 0;
	public payBandEnum conferenceAndBanquetP = payBandEnum.PayBandOne;
	public int others = 0;
	public payBandEnum othersP = payBandEnum.PayBandOne;


	
	public int month;

	public StaffingLog DeepCopy()
	{
		StaffingLog other = new StaffingLog(); 
		
		other.month = this.month;
		
		
		other.departmentHeads = this.departmentHeads;			
		other.departmentHeadsP = this.departmentHeadsP;				
		other.hotelService= this.hotelService;		
		other.hotelServiceP= this.hotelServiceP;		
		other.foodAndBev= this.foodAndBev;	
		other.foodAndBevP= this.foodAndBevP;	
		other.frontDesk= this.frontDesk;		
		other.frontDeskP= this.frontDeskP;		
		other.conferenceAndBanquet= this.conferenceAndBanquet; 			
		other.conferenceAndBanquetP= this.conferenceAndBanquetP;		
		other.others= this.others;      
		other.othersP= this.othersP;                 

		return other;
	}
}


public class StaffMenu : MonoBehaviour {

	public StaffingLog newStaffingLog = new StaffingLog();
	// Use this for initialization



	void Start () {
		SetStaffing ();
	}
	public void SetStaffing()
	{
		GameObject uimenu = GameObject.FindGameObjectWithTag("UI");
		GameObject staffMenu = uimenu.transform.FindChild("Tabs").transform.FindChild("StaffMenu").gameObject;
		GameObject inputFields = staffMenu.transform.FindChild("Inputs").gameObject;
		GameObject departmentHeads = inputFields.transform.FindChild("DepartmentHeads").gameObject;
		GameObject hotelServices = inputFields.transform.FindChild("HotelServices").gameObject;
		GameObject foodAndBev = inputFields.transform.FindChild("FoodAndBeverages").gameObject;
		GameObject frontDesk = inputFields.transform.FindChild("FrontDeskETC").gameObject;
		GameObject conferences = inputFields.transform.FindChild("ConferenceAndBanqueting").gameObject;
		GameObject others = inputFields.transform.FindChild("Others").gameObject;
		if(departmentHeads.transform.FindChild ("Number").GetComponent<InputField>().text != ""){
			Debug.Log(int.Parse(departmentHeads.transform.FindChild ("Number").GetComponent<InputField>().text));
		newStaffingLog.departmentHeads = int.Parse(departmentHeads.transform.FindChild ("Number").GetComponent<InputField>().text);
			if(newStaffingLog.departmentHeads > Staff.staffDepartmentHead.Count)
			{
				int tempNum = Staff.staffDepartmentHead.Count;
				for(int i=0 ; i < newStaffingLog.departmentHeads-tempNum;i++)
				{
					StaffMember newGuy = new StaffMember(staffType.DepartmentHead);
					Staff.staffDepartmentHead.Add(newGuy);
				}
			}
			else if(newStaffingLog.departmentHeads < Staff.staffDepartmentHead.Count)
			{
				int tempNum = Staff.staffDepartmentHead.Count;
				for(int i=0;i<tempNum-newStaffingLog.departmentHeads;i++)
				{
					Staff.staffDepartmentHead.RemoveAt(Staff.staffDepartmentHead.Count-1);
				}
			}
		}
		if(hotelServices.transform.FindChild ("Number").GetComponent<InputField>().text != ""){
		newStaffingLog.hotelService = int.Parse(hotelServices.transform.FindChild ("Number").GetComponent<InputField>().text);
			if(newStaffingLog.hotelService > Staff.staffHotelServices.Count)
			{
				int tempNum = Staff.staffHotelServices.Count;
				for(int i=0;i<newStaffingLog.hotelService-tempNum;i++)
				{
					StaffMember newGuy = new StaffMember(staffType.HotelServices);
					Staff.staffHotelServices.Add(newGuy);
				}
			}
			else if(newStaffingLog.hotelService < Staff.staffHotelServices.Count)
			{
				int tempNum = Staff.staffHotelServices.Count;
				for(int i=0;i<tempNum-newStaffingLog.hotelService;i++)
				{
					Staff.staffHotelServices.RemoveAt(Staff.staffHotelServices.Count-1);
				}
			}
		}
		if(foodAndBev.transform.FindChild ("Number").GetComponent<InputField>().text != ""){
		newStaffingLog.foodAndBev = int.Parse(foodAndBev.transform.FindChild ("Number").GetComponent<InputField>().text);
			if(newStaffingLog.foodAndBev > Staff.staffFoodAndBeverages.Count)
			{
				int tempNum = Staff.staffFoodAndBeverages.Count;
				for(int i=0;i<newStaffingLog.foodAndBev-tempNum;i++)
				{
					StaffMember newGuy = new StaffMember(staffType.FoodAndBeverages);
					Staff.staffFoodAndBeverages.Add(newGuy);
				}
			}
			else if(newStaffingLog.foodAndBev < Staff.staffFoodAndBeverages.Count)
			{
				int tempNum = Staff.staffFoodAndBeverages.Count;
				for(int i=0;i<tempNum-newStaffingLog.foodAndBev;i++)
				{
					Staff.staffFoodAndBeverages.RemoveAt(Staff.staffFoodAndBeverages.Count-1);
				}
			}
		}
		if(frontDesk.transform.FindChild ("Number").GetComponent<InputField>().text != ""){
		newStaffingLog.frontDesk = int.Parse(frontDesk.transform.FindChild ("Number").GetComponent<InputField>().text);
			if(newStaffingLog.frontDesk > Staff.staffFrontDesk.Count)
			{
				int tempNum = Staff.staffFrontDesk.Count;
				for(int i=0;i<newStaffingLog.frontDesk-tempNum;i++)
				{
					StaffMember newGuy = new StaffMember(staffType.FrontDesk);
					Staff.staffFrontDesk.Add(newGuy);
				}
			}
			else if(newStaffingLog.frontDesk < Staff.staffFrontDesk.Count)
			{
				int tempNum = Staff.staffFrontDesk.Count;
				for(int i=0;i<tempNum-newStaffingLog.frontDesk;i++)
				{
					Staff.staffFrontDesk.RemoveAt(Staff.staffFrontDesk.Count-1);
				}
			}
		}
		if(conferences.transform.FindChild ("Number").GetComponent<InputField>().text != ""){
		newStaffingLog.conferenceAndBanquet = int.Parse(conferences.transform.FindChild ("Number").GetComponent<InputField>().text); 
			if(newStaffingLog.conferenceAndBanquet > Staff.staffConference.Count)
			{
				int tempNum = Staff.staffConference.Count;
				for(int i=0;i<newStaffingLog.conferenceAndBanquet-tempNum;i++)
				{
					StaffMember newGuy = new StaffMember(staffType.Conference);
					Staff.staffConference.Add(newGuy);
				}
			}
			else if(newStaffingLog.conferenceAndBanquet < Staff.staffConference.Count)
			{
				int tempNum = Staff.staffConference.Count;
				for(int i=0;i<tempNum-newStaffingLog.conferenceAndBanquet;i++)
				{
					Staff.staffConference.RemoveAt(Staff.staffConference.Count-1);
				}
			}
		}
		if(others.transform.FindChild ("Number").GetComponent<InputField>().text != ""){
		newStaffingLog.others = int.Parse(others.transform.FindChild ("Number").GetComponent<InputField>().text );
			if(newStaffingLog.others > Staff.staffOthers.Count)
			{
				int tempNum = Staff.staffOthers.Count;
				for(int i=0;i<newStaffingLog.others-tempNum;i++)
				{
					StaffMember newGuy = new StaffMember(staffType.Others);
					Staff.staffOthers.Add(newGuy);
				}
			}
			else if(newStaffingLog.others < Staff.staffOthers.Count)
			{
				int tempNum = Staff.staffOthers.Count;
				for(int i=0;i<tempNum-newStaffingLog.others;i++)
				{
					Staff.staffOthers.RemoveAt(Staff.staffOthers.Count-1);
				}
			}
		}

		/*Debug.Log (newStaffingLog.departmentHeads);
		Debug.Log (newStaffingLog.conferenceAndBanquet);
		Debug.Log (newStaffingLog.foodAndBev);
		Debug.Log (newStaffingLog.frontDesk);
		Debug.Log (newStaffingLog.hotelService);
		Debug.Log (newStaffingLog.others);

		Debug.Log (Staff.staffOthers.Count);
		Debug.Log (Staff.staffHotelServices.Count);
		Debug.Log (Staff.staffFrontDesk.Count);
		Debug.Log (Staff.staffFoodAndBeverages.Count);
		Debug.Log (Staff.staffConference.Count);
		Debug.Log (Staff.staffDepartmentHead.Count);*/

		//need to add the payband lvl dropdown menu. set as a float for now.
		/*
		newStaffingLog.departmentHeadsP = float.Parse(departmentHeads.transform.FindChild ("PayBand").GetComponent<InputField>().text); 
		newStaffingLog.hotelServiceP = float.Parse(hotelServices.transform.FindChild ("PayBand").GetComponent<InputField>().text );
		newStaffingLog.foodAndBevP = float.Parse(foodAndBev.transform.FindChild ("PayBand").GetComponent<InputField>().text );
		newStaffingLog.frontDeskP = float.Parse(frontDesk.transform.FindChild ("PayBand").GetComponent<InputField>().text );
		newStaffingLog.conferenceAndBanquetP = float.Parse(conferences.transform.FindChild ("PayBand").GetComponent<InputField>().text);
		newStaffingLog.othersP = float.Parse(others.transform.FindChild ("PayBand").GetComponent<InputField>().text );
*/
	}
	public float[] ReturnCostPerDay ()
	{

		float[] returnValue = new float[6]{0,0,0,0,0,0};
		foreach (StaffMember aMember in Staff.staffDepartmentHead)
		{
			returnValue[0] += aMember.PayRate*8;
		}
		foreach (StaffMember aMember in Staff.staffHotelServices)
		{
			returnValue[1] += aMember.PayRate*8;
		}
		foreach (StaffMember aMember in Staff.staffFoodAndBeverages)
		{
			returnValue[2] += aMember.PayRate*8;
		}
		foreach (StaffMember aMember in Staff.staffFrontDesk)
		{
			returnValue[3] += aMember.PayRate*8;
		}
		foreach (StaffMember aMember in Staff.staffConference)
		{
			returnValue[4] += aMember.PayRate*8;
		}
		foreach (StaffMember aMember in Staff.staffOthers)
		{
			returnValue[5] += aMember.PayRate*8;
		}

		return returnValue;
	}
	public float returnActualProduction (float ProductionNeeded)
	{
		float actualProd = 0;
		foreach (StaffMember aMember in Staff.staffDepartmentHead)
		{
			actualProd += aMember.CalculateProd()*8;
			aMember.HoursWorked = 8;
			aMember.Experience++;
		}
		foreach (StaffMember aMember in Staff.staffHotelServices)
		{
			actualProd += aMember.CalculateProd()*8;
			aMember.HoursWorked = 8;
			aMember.Experience++;
		}
		foreach (StaffMember aMember in Staff.staffFoodAndBeverages)
		{
			actualProd += aMember.CalculateProd()*8;
			aMember.HoursWorked = 8;
			aMember.Experience++;
		}
		foreach (StaffMember aMember in Staff.staffFrontDesk)
		{
			actualProd += aMember.CalculateProd()*8;
			aMember.HoursWorked = 8;
			aMember.Experience++;
		}
		foreach (StaffMember aMember in Staff.staffConference)
		{
			actualProd += aMember.CalculateProd()*8;
			aMember.HoursWorked = 8;
			aMember.Experience++;
		}
		foreach (StaffMember aMember in Staff.staffOthers)
		{
			actualProd += aMember.CalculateProd()*8;
			aMember.HoursWorked = 8;
			aMember.Experience++;
		}

		return actualProd;
	
	}
	public void SetPay(Slider slider)
	{
		switch(slider.name)
		{
		case "PayBandSlider1":
			newStaffingLog.departmentHeadsP = (payBandEnum)slider.value;
			slider.GetComponentInChildren<Text>().text = "Tier " + slider.value;
			foreach (StaffMember aMember in Staff.staffDepartmentHead)
			{
				aMember.SetPay(newStaffingLog.departmentHeadsP);
			}
			break;
		case "PayBandSlider2":
			newStaffingLog.hotelServiceP = (payBandEnum)slider.value;
			slider.GetComponentInChildren<Text>().text = "Tier " + slider.value;
			foreach (StaffMember aMember in Staff.staffHotelServices)
			{
				aMember.SetPay(newStaffingLog.hotelServiceP );
			}
			break;
		case "PayBandSlider3":
			newStaffingLog.foodAndBevP = (payBandEnum)slider.value;
			slider.GetComponentInChildren<Text>().text = "Tier " + slider.value;
			foreach (StaffMember aMember in Staff.staffFoodAndBeverages)
			{
				aMember.SetPay(newStaffingLog.foodAndBevP);
			}
			break;
		case "PayBandSlider4":
			newStaffingLog.frontDeskP = (payBandEnum)slider.value;
			slider.GetComponentInChildren<Text>().text = "Tier " + slider.value;
			foreach (StaffMember aMember in Staff.staffFrontDesk)
			{
				aMember.SetPay(newStaffingLog.frontDeskP);
			}
			break;
		case "PayBandSlider5":
			newStaffingLog.conferenceAndBanquetP = (payBandEnum)slider.value;
			slider.GetComponentInChildren<Text>().text = "Tier " + slider.value;
			foreach (StaffMember aMember in Staff.staffConference)
			{
				aMember.SetPay(newStaffingLog.conferenceAndBanquetP);
			}
			break;
		case "PayBandSlider6":
			newStaffingLog.othersP = (payBandEnum)slider.value;
			slider.GetComponentInChildren<Text>().text = "Tier " + slider.value;
			foreach (StaffMember aMember in Staff.staffOthers)
			{
				aMember.SetPay(newStaffingLog.othersP);
			}
			break;
		}

	}


}
