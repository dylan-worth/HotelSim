using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Refurbishment : MonoBehaviour {
	[SerializeField][Tooltip("Cost of each individual upgrade to the restaurant.")]
	float[] restaurantUpgrades = new float[4];
	[SerializeField][Tooltip("Cost of each individual upgrade to the bar.")]
	float[] barUpgrades = new float[4];
	[SerializeField][Tooltip("Cost of each individual upgrade to the front desk/reception.")]
	float[] frontDeskUpgrades = new float[4];

	GameObject refurbishTab;

	GameObject inputs;
	GameObject standardRooms;
	GameObject doubleRooms;
	GameObject deluxeRooms;
	GameObject suites;
	GameObject masterSuites;
	GameObject costHeader;
	GameObject restaurantRefurb;
	GameObject barRefurb;
	GameObject frontDeskRefurb;
	GameObject[] roomTypes = new GameObject[5];



	List<RoomStats> needOfRepairSTD = new List<RoomStats>();
	List<RoomStats> needOfRepairDBL = new List<RoomStats>();
	List<RoomStats> needOfRepairDLX = new List<RoomStats>();
	List<RoomStats> needOfRepairSuite = new List<RoomStats>();
	List<RoomStats> needOfRepairMaster = new List<RoomStats>();
	List<RoomStats>[] masterList = new List<RoomStats>[5];

	void Awake(){
		refurbishTab = GameObject.FindGameObjectWithTag ("UI").transform.FindChild ("Tabs").transform.FindChild ("RefurbishTab").gameObject;

		//references for all tabs
		inputs = refurbishTab.transform.FindChild ("Inputs").gameObject;
		standardRooms = inputs.transform.FindChild ("StandardRefurb").gameObject;
		doubleRooms = inputs.transform.FindChild ("DoubleRefurb").gameObject;
		deluxeRooms = inputs.transform.FindChild ("DeluxeRefurb").gameObject;
		suites = inputs.transform.FindChild ("SuitesRefurb").gameObject;
		masterSuites = inputs.transform.FindChild ("MasterSuitesRefurb").gameObject;
		restaurantRefurb = inputs.transform.FindChild ("RestaurantRefurb").gameObject;
		barRefurb = inputs.transform.FindChild ("BarRefurb").gameObject;
		frontDeskRefurb = inputs.transform.FindChild ("FrontDeskRefurb").gameObject;

		//costHeader = inputs.transform.FindChild ("CostHeader").gameObject;
		roomTypes [0] = standardRooms;
		roomTypes [1] = doubleRooms;
		roomTypes [2] = deluxeRooms;
		roomTypes [3] = suites;
		roomTypes [4] = masterSuites;
		refreshTabs ();

	}

	public void OpenTab()
	{
		CalculateCost();
	}
	//refresh the display with updated values.
	public void refreshTabs()
	{

		int standardRooms = 0;
		int standardNeedRepair = 0;
		int doubleRooms = 0;
		int doubleNeedRepair = 0;
		int deluxeRooms = 0;
		int deluxeNeedRepair = 0;
		int suites = 0;
		int suitesNeedRepair = 0;
		int masterSuites = 0;
		int masterNeedRepair = 0;
		int[] roomcounts = new int[5];
		int[] roomNeedRepair = new int[5];
		needOfRepairSTD.Clear ();
		needOfRepairDBL.Clear ();
		needOfRepairDLX.Clear ();
		needOfRepairSuite.Clear ();
		needOfRepairMaster.Clear ();
		//cycle through the list of rooms and assign any rooms below 50% to a list of disrepaired roomstats.
		foreach (RoomStats aBedroom in BedroomBehaviour.allBedrooms) 
		{
			switch(aBedroom.roomQuality)
			{
			case 1:
				standardRooms++;
				if(aBedroom.roomCondition < 50){
					standardNeedRepair++;
					needOfRepairSTD.Add(aBedroom);
				}
				break;
			case 2:
				doubleRooms++;
				if(aBedroom.roomCondition < 50){
					doubleNeedRepair++;
					needOfRepairDBL.Add(aBedroom);
				}
				break;
			case 3:
				deluxeRooms++;
				if(aBedroom.roomCondition < 50){
					deluxeNeedRepair++;
					needOfRepairDLX.Add(aBedroom);
				}
				break;
			case 4:
				suites++;
				if(aBedroom.roomCondition < 50){
					suitesNeedRepair++;
					needOfRepairSuite.Add(aBedroom);
				}
				break;
			case 5:
				masterSuites++;
				if(aBedroom.roomCondition < 50){
					masterNeedRepair++;
					needOfRepairMaster.Add(aBedroom);
				}
				break;
			}
		}
		masterList [0] = needOfRepairSTD;
		masterList [1] = needOfRepairDBL;
		masterList [2] = needOfRepairDLX;
		masterList [3] = needOfRepairSuite;
		masterList [4] = needOfRepairMaster;
		roomcounts [0] = standardRooms;
		roomcounts [1] = doubleRooms;
		roomcounts [2] = deluxeRooms;
		roomcounts [3] = suites;
		roomcounts [4] = masterSuites;
		roomNeedRepair [0] = standardNeedRepair;
		roomNeedRepair [1] = doubleNeedRepair;
		roomNeedRepair [2] = deluxeNeedRepair;
		roomNeedRepair [3] = suitesNeedRepair;
		roomNeedRepair [4] = masterNeedRepair;

		for (int i = 0; i<roomTypes.Length; i++) 
		{
			roomTypes[i].transform.FindChild("Number").GetComponent<InputField>().text = roomcounts[i].ToString();
			roomTypes[i].transform.FindChild("NumberBelow50").GetComponent<InputField>().text = roomNeedRepair[i].ToString();
		}

		MasterReference.standardRooms = standardRooms;
		MasterReference.doubleRooms = doubleRooms;
		MasterReference.deluxeRooms = deluxeRooms;
		MasterReference.suites = suites;
		MasterReference.masterSuites = masterSuites;
		//Set the different levels and condition of specials room.
		restaurantRefurb.transform.FindChild("Level").GetComponent<InputField>().text = MasterReference.restaurantLevel.ToString();
		barRefurb.transform.FindChild("Level").GetComponent<InputField>().text = MasterReference.barLevel.ToString();
		frontDeskRefurb.transform.FindChild("Level").GetComponent<InputField>().text = MasterReference.frontDeskLevel.ToString();
		//Condition
		restaurantRefurb.transform.FindChild("Condition").GetComponent<InputField>().text = MasterReference.restaurantConditionRepair.ToString();
		barRefurb.transform.FindChild("Condition").GetComponent<InputField>().text = MasterReference.barCondition.ToString();;
		frontDeskRefurb.transform.FindChild("Condition").GetComponent<InputField>().text = MasterReference.frontDeskCondition.ToString();
		//Repair cost
		restaurantRefurb.transform.FindChild("Cost").GetComponent<InputField>().text = GetCostOfRepairSpecialRooms(1).ToString();
		barRefurb.transform.FindChild("Cost").GetComponent<InputField>().text = GetCostOfRepairSpecialRooms(2).ToString();;
		frontDeskRefurb.transform.FindChild("Cost").GetComponent<InputField>().text = GetCostOfRepairSpecialRooms(3).ToString();
		//Upgrade cost displayed on the upgrade button.
		if(MasterReference.restaurantLevel != 5)
		{
			restaurantRefurb.transform.FindChild("btn_Upgrade").transform.FindChild("Text").GetComponent<Text>().text = 
				"Upgrade" + "\n" + "Cost: $" + restaurantUpgrades[MasterReference.restaurantLevel-1].ToString();
		}
		else
		{
			restaurantRefurb.transform.FindChild("btn_Upgrade").transform.FindChild("Text").GetComponent<Text>().text = 
				"No More Upgrades"+"\n"+"available";
			restaurantRefurb.transform.FindChild("btn_Upgrade").GetComponent<Button>().interactable = false;
		}
		if(MasterReference.barLevel != 5)
		{
			barRefurb.transform.FindChild("btn_Upgrade").transform.FindChild("Text").GetComponent<Text>().text = 
				"Upgrade" + "\n" + "Cost: $" + barUpgrades[MasterReference.barLevel-1].ToString();
		}
		else
		{
			barRefurb.transform.FindChild("btn_Upgrade").transform.FindChild("Text").GetComponent<Text>().text = 
				"No More Upgrades"+"\n"+"available";
			barRefurb.transform.FindChild("btn_Upgrade").GetComponent<Button>().interactable = false;
		}
		if(MasterReference.frontDeskLevel != 5)
		{
			frontDeskRefurb.transform.FindChild("btn_Upgrade").transform.FindChild("Text").GetComponent<Text>().text = 
				"Upgrade" + "\n" + "Cost: $" + frontDeskUpgrades[MasterReference.frontDeskLevel-1].ToString();
		}
		else
		{
			frontDeskRefurb.transform.FindChild("btn_Upgrade").transform.FindChild("Text").GetComponent<Text>().text = 
				"No More Upgrades"+"\n"+"available";
			frontDeskRefurb.transform.FindChild("btn_Upgrade").GetComponent<Button>().interactable = false;
		}

	}
	//returns the cost to repair a special room. Takes in an int for the type being repaired.
	float GetCostOfRepairSpecialRooms(int type)
	{
		float costOfRepairSpecialRooms = 0f;
		switch(type)
		{
		case 1://restaurant
			costOfRepairSpecialRooms = (((100f-MasterReference.restaurantConditionRepair) * 25000f)/100f)*MasterReference.restaurantLevel;
			break;
		case 2://bar
			costOfRepairSpecialRooms = (((100f-MasterReference.barCondition) * 5000f)/100f)*MasterReference.barLevel;
			break;
		case 3://front desk
			costOfRepairSpecialRooms = (((100f-MasterReference.frontDeskCondition) * 500f)/100f)*MasterReference.frontDeskLevel;
			break;
		}
		return costOfRepairSpecialRooms;
	}
	//displays the cost to repair each types of rooms.
	public void CalculateCost()
	{
		refreshTabs ();

		for(int i =1; i <= 5; i++)
		{
			roomTypes[i-1].transform.FindChild("Cost").GetComponent<InputField>().text = RepairRooms(i).ToString();
		}
	}
	//Linked to the repair buttons for special rooms. Runs a repair method.
	public void BTNRepairSpecial(int repairedType)
	{
		MasterReference.repairCosts += GetCostOfRepairSpecialRooms(repairedType);
		switch(repairedType)
		{
		case 1://restaurant
			MasterReference.restaurantConditionRepair = 100f;
			break;
		case 2://bar
			MasterReference.barCondition = 100f;
			break;
		case 3://front desk
			MasterReference.frontDeskCondition = 100f;
			break;
		}
		refreshTabs();
	}
	//Linked to the repair buttons for each type of room. Runs a repair method.
	public void BTNRepair(int repairedType)
	{
		MasterReference.repairCosts += RepairRooms(repairedType);
		switch(repairedType)
		{
			//cost of repairs for a upgraded room is higher due to more expensive materials/fixtures/furniture.
		case 1://standard rooms
			foreach (RoomStats aBedroom in masterList[0])
			{
				aBedroom.roomCondition = 100;
				aBedroom.GetComponent<Renderer>().material.color = Color.white;

			}
			break;
		case 2://double rooms
			foreach (RoomStats aBedroom in masterList[1])
			{
				aBedroom.roomCondition = 100;
				aBedroom.GetComponent<Renderer>().material.color = Color.white;
			}
			break;
		case 3://deluxe rooms
			foreach (RoomStats aBedroom in masterList[2])
			{
				aBedroom.roomCondition = 100;
				aBedroom.GetComponent<Renderer>().material.color = Color.white;
			}
			break;
		case 4://suites
			foreach (RoomStats aBedroom in masterList[3])
			{
				aBedroom.roomCondition = 100;
				aBedroom.GetComponent<Renderer>().material.color = Color.white;
			}
			break;
		case 5://master suites
			foreach (RoomStats aBedroom in masterList[4])
			{
				aBedroom.roomCondition = 100;
				aBedroom.GetComponent<Renderer>().material.color = Color.white;
			}
			break;
		}
		refreshTabs();
	}
	//This methods takes in an int of the room type being repaired or estmated from 1-5. It returns the amount as a float.
	//need to call only after the lists have been updated.
	public float RepairRooms(int typeOfRoom)
	{
		float repairCosts = 0f;
		switch(typeOfRoom)
		{
			//cost of repairs for a upgraded room is higher due to more expensive materials/fixtures/furniture.
		case 1://standard rooms
			foreach (RoomStats aBedroom in masterList[0])
			{
				//basic repair cost when repairing all is 50 instread of 150 to give incentive to repairing multiple at a time.
				repairCosts += Mathf.Round((50f + 1000f*((100f-aBedroom.roomCondition))/100f));
			}
			break;
		case 2://double rooms
			foreach (RoomStats aBedroom in masterList[1])
			{
				//basic repair cost when repairing all is 50 instread of 150 to give incentive to repairing multiple at a time.
				repairCosts += Mathf.Round((50f + 1000f*((100f-aBedroom.roomCondition))/100f));
			}
			break;
		case 3://deluxe rooms
			foreach (RoomStats aBedroom in masterList[2])
			{
				//basic repair cost when repairing all is 50 instread of 150 to give incentive to repairing multiple at a time.
				repairCosts += Mathf.Round((50f + 1500f*((100f-aBedroom.roomCondition))/100f));
			}
			break;
		case 4://suites
			foreach (RoomStats aBedroom in masterList[3])
			{
				//basic repair cost when repairing all is 50 instread of 150 to give incentive to repairing multiple at a time.
				repairCosts += Mathf.Round((50f + 3000f*((100f-aBedroom.roomCondition))/100f));
			}
			break;
		case 5://master suites
			foreach (RoomStats aBedroom in masterList[4])
			{
				//basic repair cost when repairing all is 50 instread of 150 to give incentive to repairing multiple at a time.
				repairCosts += Mathf.Round((50f + 10000f*((100f-aBedroom.roomCondition))/100f));
			}
			break;
		}
		return repairCosts;

	}
	//degrade function for restaurant bar and front desk. Need to add conference room to this list.
	public void DegradeSpecialRooms()
	{
		//currently not based on occupancy.
		MasterReference.restaurantConditionRepair--;
		MasterReference.barCondition--;
		MasterReference.frontDeskCondition--;
	}
	//Link to the upgrade btns. Upgrade the special rooms.
	public void UpgradeSpecialRoom(int type)
	{
		switch(type)
		{
		case 1://restaurant
			MasterReference.upgradeCost += restaurantUpgrades[MasterReference.restaurantLevel-1];
			MasterReference.restaurantLevel++;
			break;
		case 2://bar
			MasterReference.upgradeCost += barUpgrades[MasterReference.barLevel-1];
			MasterReference.barLevel++;
			break;
		case 3://front desk
			MasterReference.upgradeCost += frontDeskUpgrades[MasterReference.frontDeskLevel-1];
			MasterReference.frontDeskLevel++;
			break;
		}
		refreshTabs();
	}
}


