using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RatesSetup : MonoBehaviour {
	

	public ReceptionLog newReceptionLog = new ReceptionLog();

	public GameObject tabStandard;
	public GameObject tabDouble;
	public GameObject tabDeluxe;
	public GameObject tabSuite;
	public GameObject tabMasterSuite;


	GameObject receptionMenuRef;
	void Awake(){
		SetRates();
		receptionMenuRef.SetActive(false);
	}

	public void SetRates()
	{
		GameObject uimenu = GameObject.FindGameObjectWithTag("UI");
		GameObject receptionMenu = uimenu.transform.FindChild("Tabs").transform.FindChild("ReceptionMenu").gameObject;
		GameObject ratesFieldInputs = receptionMenu.transform.FindChild("RatesFieldInputs").gameObject;
		GameObject weekDayRoom = tabStandard.transform.FindChild("WeekDayRoom").gameObject;
		GameObject weekEndRoom = tabStandard.transform.FindChild("WeekEndRoom").gameObject;

		GameObject weekDayRoomDbl = tabDouble.transform.FindChild("WeekDayRoomD").gameObject;
		GameObject weekEndRoomDbl = tabDouble.transform.FindChild("WeekEndRoomD").gameObject;

		GameObject weekDayRoomDeluxe = tabDeluxe.transform.FindChild("WeekDayRoomDL").gameObject;
		GameObject weekEndRoomDeluxe = tabDeluxe.transform.FindChild("WeekEndRoomDL").gameObject;

		GameObject weekDayRoomSuite = tabSuite.transform.FindChild("WeekDayRoomS").gameObject;
		GameObject weekEndRoomSuite = tabSuite.transform.FindChild("WeekEndRoomS").gameObject;

		GameObject weekDayRoomMaster = tabMasterSuite.transform.FindChild("WeekDayRoomMS").gameObject;
		GameObject weekEndRoomMaster = tabMasterSuite.transform.FindChild("WeekEndRoomMS").gameObject;

		GameObject conferences = ratesFieldInputs.transform.FindChild("Conferences").gameObject;
		GameObject groups = ratesFieldInputs.transform.FindChild("Groups").gameObject;
		GameObject events = ratesFieldInputs.transform.FindChild("Events").gameObject;

		receptionMenuRef = receptionMenu;
		//standard
			//WeekDays
			if(weekDayRoom.transform.FindChild ("Rate").GetComponent<InputField>().text != "")
				newReceptionLog.standardRoom.weekdayRoomCost = float.Parse(weekDayRoom.transform.FindChild ("Rate").GetComponent<InputField>().text);

			if(weekDayRoom.transform.FindChild ("Discount3Night").GetComponent<InputField>().text != "")
			newReceptionLog.standardRoom.weekday3NightDiscount = float.Parse(weekDayRoom.transform.FindChild ("Discount3Night").GetComponent<InputField>().text);

			if( weekDayRoom.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text != "")
				newReceptionLog.standardRoom.weekdayNoBreakfastDiscount = float.Parse(weekDayRoom.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text);

			if(weekDayRoom.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text != "")
				newReceptionLog.standardRoom.weekdayNoCancelDiscount = float.Parse(weekDayRoom.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text);

			//WeekEnds
			if(weekEndRoom.transform.FindChild ("Rate").GetComponent<InputField>().text != "")
				newReceptionLog.standardRoom.weekendRoomCost = float.Parse(weekEndRoom.transform.FindChild ("Rate").GetComponent<InputField>().text);

			if(weekEndRoom.transform.FindChild ("Discount3Night").GetComponent<InputField>().text != "")
				newReceptionLog.standardRoom.weekend3NightDiscount = float.Parse(weekEndRoom.transform.FindChild ("Discount3Night").GetComponent<InputField>().text);
				
			if( weekEndRoom.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text != "")
				newReceptionLog.standardRoom.weekendNoBreakfastDiscount = float.Parse(weekEndRoom.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text);

			if(weekEndRoom.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text != "")
				newReceptionLog.standardRoom.weekendNoCancelDiscount = float.Parse(weekEndRoom.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text);
		//double
		//WeekDays
		if(weekDayRoomDbl.transform.FindChild ("Rate").GetComponent<InputField>().text != "")
			newReceptionLog.doubleRoom.weekdayRoomCost = float.Parse(weekDayRoomDbl.transform.FindChild ("Rate").GetComponent<InputField>().text);
			
		if(weekDayRoomDbl.transform.FindChild ("Discount3Night").GetComponent<InputField>().text != "")
			newReceptionLog.doubleRoom.weekday3NightDiscount = float.Parse(weekDayRoomDbl.transform.FindChild ("Discount3Night").GetComponent<InputField>().text);

		if( weekDayRoomDbl.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text != "")
			newReceptionLog.doubleRoom.weekdayNoBreakfastDiscount = float.Parse(weekDayRoomDbl.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text);
			
		if(weekDayRoomDbl.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text != "")
			newReceptionLog.doubleRoom.weekdayNoCancelDiscount = float.Parse(weekDayRoomDbl.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text);
			
			//WeekEnds
		if(weekEndRoomDbl.transform.FindChild ("Rate").GetComponent<InputField>().text != "")
			newReceptionLog.doubleRoom.weekendRoomCost = float.Parse(weekEndRoomDbl.transform.FindChild ("Rate").GetComponent<InputField>().text);
			
		if(weekEndRoomDbl.transform.FindChild ("Discount3Night").GetComponent<InputField>().text != "")
			newReceptionLog.doubleRoom.weekend3NightDiscount = float.Parse(weekEndRoomDbl.transform.FindChild ("Discount3Night").GetComponent<InputField>().text);
			
		if( weekEndRoomDbl.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text != "")
			newReceptionLog.doubleRoom.weekendNoBreakfastDiscount = float.Parse(weekEndRoomDbl.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text);
			
		if(weekEndRoomDbl.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text != "")
			newReceptionLog.doubleRoom.weekendNoCancelDiscount = float.Parse(weekEndRoomDbl.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text);
		//deluxe
			//WeekDays
		if(weekDayRoomDeluxe.transform.FindChild ("Rate").GetComponent<InputField>().text != "")
			newReceptionLog.deluxeRoom.weekdayRoomCost = float.Parse(weekDayRoomDeluxe.transform.FindChild ("Rate").GetComponent<InputField>().text);
	
		if( weekDayRoomDeluxe.transform.FindChild ("Discount3Night").GetComponent<InputField>().text != "")
			newReceptionLog.deluxeRoom.weekday3NightDiscount = float.Parse(weekDayRoomDeluxe.transform.FindChild ("Discount3Night").GetComponent<InputField>().text);	
			
		if( weekDayRoomDeluxe.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text != "")
			newReceptionLog.deluxeRoom.weekdayNoBreakfastDiscount = float.Parse(weekDayRoomDeluxe.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text);
			
		if(weekDayRoomDeluxe.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text != "")
			newReceptionLog.deluxeRoom.weekdayNoCancelDiscount = float.Parse(weekDayRoomDeluxe.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text);
			
			//WeekEnds
		if(weekEndRoomDeluxe.transform.FindChild ("Rate").GetComponent<InputField>().text != "")
			newReceptionLog.deluxeRoom.weekendRoomCost = float.Parse(weekEndRoomDeluxe.transform.FindChild ("Rate").GetComponent<InputField>().text);
			
		if(weekEndRoomDeluxe.transform.FindChild ("Discount3Night").GetComponent<InputField>().text != "")
			newReceptionLog.deluxeRoom.weekend3NightDiscount = float.Parse(weekEndRoomDeluxe.transform.FindChild ("Discount3Night").GetComponent<InputField>().text);
			
		if( weekEndRoomDeluxe.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text != "")
			newReceptionLog.deluxeRoom.weekendNoBreakfastDiscount = float.Parse(weekEndRoomDeluxe.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text);
			
		if(weekEndRoomDeluxe.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text != "")
			newReceptionLog.deluxeRoom.weekendNoCancelDiscount = float.Parse(weekEndRoomDeluxe.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text);
		//suite
			//WeekDays
		if(weekDayRoomSuite.transform.FindChild ("Rate").GetComponent<InputField>().text != "")
			newReceptionLog.suiteRoom.weekdayRoomCost = float.Parse(weekDayRoomSuite.transform.FindChild ("Rate").GetComponent<InputField>().text);
			
		if( weekDayRoomSuite.transform.FindChild ("Discount3Night").GetComponent<InputField>().text != "")
			newReceptionLog.suiteRoom.weekday3NightDiscount = float.Parse(weekDayRoomSuite.transform.FindChild ("Discount3Night").GetComponent<InputField>().text);	
			
		if( weekDayRoomSuite.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text != "")
			newReceptionLog.suiteRoom.weekdayNoBreakfastDiscount = float.Parse(weekDayRoomSuite.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text);
			
		if(weekDayRoomSuite.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text != "")
			newReceptionLog.suiteRoom.weekdayNoCancelDiscount = float.Parse(weekDayRoomSuite.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text);
			
			//WeekEnds
		if(weekEndRoomSuite.transform.FindChild ("Rate").GetComponent<InputField>().text != "")
			newReceptionLog.suiteRoom.weekendRoomCost = float.Parse(weekEndRoomSuite.transform.FindChild ("Rate").GetComponent<InputField>().text);
			
		if(weekEndRoomSuite.transform.FindChild ("Discount3Night").GetComponent<InputField>().text != "")
			newReceptionLog.suiteRoom.weekend3NightDiscount = float.Parse(weekEndRoomSuite.transform.FindChild ("Discount3Night").GetComponent<InputField>().text);
			
		if( weekEndRoomSuite.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text != "")
			newReceptionLog.suiteRoom.weekendNoBreakfastDiscount = float.Parse(weekEndRoomSuite.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text);
			
		if(weekEndRoomSuite.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text != "")
			newReceptionLog.suiteRoom.weekendNoCancelDiscount = float.Parse(weekEndRoomSuite.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text);
		//master Suite
			//WeekDays
		if(weekDayRoomMaster.transform.FindChild ("Rate").GetComponent<InputField>().text != "")
			newReceptionLog.masterSuiteRoom.weekdayRoomCost = float.Parse(weekDayRoomMaster.transform.FindChild ("Rate").GetComponent<InputField>().text);
			
		if( weekDayRoomMaster.transform.FindChild ("Discount3Night").GetComponent<InputField>().text != "")
			newReceptionLog.masterSuiteRoom.weekday3NightDiscount = float.Parse(weekDayRoomMaster.transform.FindChild ("Discount3Night").GetComponent<InputField>().text);	
			
		if( weekDayRoomMaster.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text != "")
			newReceptionLog.masterSuiteRoom.weekdayNoBreakfastDiscount = float.Parse(weekDayRoomMaster.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text);
			
		if(weekDayRoomMaster.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text != "")
			newReceptionLog.masterSuiteRoom.weekdayNoCancelDiscount = float.Parse(weekDayRoomMaster.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text);
			
			//WeekEnds
		if(weekEndRoomMaster.transform.FindChild ("Rate").GetComponent<InputField>().text != "")
			newReceptionLog.masterSuiteRoom.weekendRoomCost = float.Parse(weekEndRoomMaster.transform.FindChild ("Rate").GetComponent<InputField>().text);
			
		if(weekEndRoomMaster.transform.FindChild ("Discount3Night").GetComponent<InputField>().text != "")
			newReceptionLog.masterSuiteRoom.weekend3NightDiscount = float.Parse(weekEndRoomMaster.transform.FindChild ("Discount3Night").GetComponent<InputField>().text);
			
		if( weekEndRoomMaster.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text != "")
			newReceptionLog.masterSuiteRoom.weekendNoBreakfastDiscount = float.Parse(weekEndRoomMaster.transform.FindChild ("DiscountNoBreakfast").GetComponent<InputField>().text);
			
		if(weekEndRoomMaster.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text != "")
			newReceptionLog.masterSuiteRoom.weekendNoCancelDiscount = float.Parse(weekEndRoomMaster.transform.FindChild ("DiscountNoCancel").GetComponent<InputField>().text);

		//Conferences
		if(conferences.transform.FindChild ("Rate").GetComponent<InputField>().text != "")
			newReceptionLog.conferenceHeadCost = float.Parse(conferences.transform.FindChild ("Rate").GetComponent<InputField>().text);
	
		if(conferences.transform.FindChild ("DiscountWeekEnd").GetComponent<InputField>().text != "")
			newReceptionLog.conferenceWeekendDiscount = float.Parse(conferences.transform.FindChild ("DiscountWeekEnd").GetComponent<InputField>().text);
			
		if(conferences.transform.FindChild ("Rooms").GetComponent<InputField>().text != "")
			newReceptionLog.conferenceRoomsDiscount = float.Parse(conferences.transform.FindChild ("Rooms").GetComponent<InputField>().text);
			
		if(conferences.transform.FindChild ("RevenueSplitFood").GetComponent<InputField>().text != "")
			newReceptionLog.conferenceRevenueSplitFood = float.Parse(conferences.transform.FindChild ("RevenueSplitFood").GetComponent<InputField>().text); 

		if(conferences.transform.FindChild ("ConferenceRooms").GetComponent<InputField>().text != "")
			newReceptionLog.conferenceConfRoomsDiscount = float.Parse(conferences.transform.FindChild ("ConferenceRooms").GetComponent<InputField>().text);

		//Groups
		if(groups.transform.FindChild ("Rate").GetComponent<InputField>().text != "")
			newReceptionLog.groupHeadCost = float.Parse(groups.transform.FindChild ("Rate").GetComponent<InputField>().text);
	
		if(groups.transform.FindChild ("RevenueSplitFood").GetComponent<InputField>().text != "")
			newReceptionLog.groupRevenueSplitFood = float.Parse(groups.transform.FindChild ("RevenueSplitFood").GetComponent<InputField>().text);
			
		if(groups.transform.FindChild ("Rooms").GetComponent<InputField>().text != "")
			newReceptionLog.groupRoomsDiscount = float.Parse(groups.transform.FindChild ("Rooms").GetComponent<InputField>().text);
			
		//Events
		if(events.transform.FindChild ("Rate").GetComponent<InputField>().text != "")
			newReceptionLog.eventsCost = float.Parse(events.transform.FindChild ("Rate").GetComponent<InputField>().text);

		if(events.transform.FindChild ("ConferenceRooms").GetComponent<InputField>().text != "")
			newReceptionLog.eventConfRoomsDiscount = float.Parse(events.transform.FindChild ("ConferenceRooms").GetComponent<InputField>().text);

		if(events.transform.FindChild ("RevenueSplitFood").GetComponent<InputField>().text != "")
			newReceptionLog.eventRevenueSplitFood = float.Parse(events.transform.FindChild ("RevenueSplitFood").GetComponent<InputField>().text);
			
		if(events.transform.FindChild ("DiscountWeekEnd").GetComponent<InputField>().text != "")
			newReceptionLog.eventWeekEndDiscount = float.Parse(events.transform.FindChild ("DiscountWeekEnd").GetComponent<InputField>().text);
			

	

		Debug.Log (newReceptionLog.standardRoom.weekdayRoomCost);
		Debug.Log (newReceptionLog.deluxeRoom.weekdayRoomCost);
		Debug.Log (newReceptionLog.doubleRoom.weekdayRoomCost);
		Debug.Log (newReceptionLog.suiteRoom.weekdayRoomCost);


	}
	public void HighlightTab(int tab)
	{
		GameObject uimenu = GameObject.FindGameObjectWithTag("UI");
		GameObject receptionMenu = uimenu.transform.FindChild("Tabs").transform.FindChild("ReceptionMenu").gameObject;
		switch(tab)
		{
		case 1:
			receptionMenu.transform.FindChild("btnTabStandard").GetComponent<Button>().image.color = new Color(1f,0f,0f);
			receptionMenu.transform.FindChild("btnTabDouble").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			receptionMenu.transform.FindChild("btnTabDeluxe").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			receptionMenu.transform.FindChild("btnTabSuite").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			receptionMenu.transform.FindChild("btnTabMasterSuite").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			break;
			
		case 2:
			receptionMenu.transform.FindChild("btnTabStandard").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			receptionMenu.transform.FindChild("btnTabDouble").GetComponent<Button>().image.color = new Color(1f,0f,0f);
			receptionMenu.transform.FindChild("btnTabDeluxe").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			receptionMenu.transform.FindChild("btnTabSuite").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			receptionMenu.transform.FindChild("btnTabMasterSuite").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			break;
			
		case 3:
			receptionMenu.transform.FindChild("btnTabStandard").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			receptionMenu.transform.FindChild("btnTabDouble").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			receptionMenu.transform.FindChild("btnTabDeluxe").GetComponent<Button>().image.color = new Color(1f,0f,0f);
			receptionMenu.transform.FindChild("btnTabSuite").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			receptionMenu.transform.FindChild("btnTabMasterSuite").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			break;
			
		case 4:
			receptionMenu.transform.FindChild("btnTabStandard").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			receptionMenu.transform.FindChild("btnTabDouble").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			receptionMenu.transform.FindChild("btnTabDeluxe").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			receptionMenu.transform.FindChild("btnTabSuite").GetComponent<Button>().image.color = new Color(1f,0f,0f);
			receptionMenu.transform.FindChild("btnTabMasterSuite").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			break;
			
		case 5:
			receptionMenu.transform.FindChild("btnTabStandard").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			receptionMenu.transform.FindChild("btnTabDouble").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			receptionMenu.transform.FindChild("btnTabDeluxe").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			receptionMenu.transform.FindChild("btnTabSuite").GetComponent<Button>().image.color = new Color(1f,1f,1f);
			receptionMenu.transform.FindChild("btnTabMasterSuite").GetComponent<Button>().image.color = new Color(1f,0f,0f);
			break;
		}
	}

}
