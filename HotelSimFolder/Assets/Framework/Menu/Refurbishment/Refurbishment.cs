using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Refurbishment : MonoBehaviour {

	GameObject refurbishTab;

	GameObject inputs;
	GameObject standardRooms;
	GameObject doubleRooms;
	GameObject deluxeRooms;
	GameObject suites;
	GameObject masterSuites;
	GameObject costHeader;
	GameObject[] roomTypes = new GameObject[5];

	public InputField totalInput; 

	List<RoomStats> needOfRepairSTD = new List<RoomStats>();
	List<RoomStats> needOfRepairDBL = new List<RoomStats>();
	List<RoomStats> needOfRepairDLX = new List<RoomStats>();
	List<RoomStats> needOfRepairSuite = new List<RoomStats>();
	List<RoomStats> needOfRepairMaster = new List<RoomStats>();
	List<RoomStats>[] masterList = new List<RoomStats>[5];

	void Awake(){
		refurbishTab = GameObject.FindGameObjectWithTag ("UI").transform.FindChild ("Tabs").transform.FindChild ("RefurbishTab").gameObject;
		//refurbishTab.SetActive (true);
		//references for all tabs
		inputs = refurbishTab.transform.FindChild ("Inputs").gameObject;
		standardRooms = inputs.transform.FindChild ("StandardRefurb").gameObject;
		doubleRooms = inputs.transform.FindChild ("DoubleRefurb").gameObject;
		deluxeRooms = inputs.transform.FindChild ("DeluxeRefurb").gameObject;
		suites = inputs.transform.FindChild ("SuitesRefurb").gameObject;
		masterSuites = inputs.transform.FindChild ("MasterSuitesRefurb").gameObject;
		//costHeader = inputs.transform.FindChild ("CostHeader").gameObject;
		roomTypes [0] = standardRooms;
		roomTypes [1] = doubleRooms;
		roomTypes [2] = deluxeRooms;
		roomTypes [3] = suites;
		roomTypes [4] = masterSuites;
		refreshTabs ();
		refurbishTab.SetActive (false);
	}

	void OnEnable() 
	{
		refreshTabs ();
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
	}

	float totalCosts;
	bool repairing = false;
	public void CalculateCost()
	{
		refreshTabs ();
		totalCosts = 0f;
		for(int i = 0; i< roomTypes.Length; i++)
		{
			float costForType = 0;

			float workCount;

			if(roomTypes[i].transform.FindChild("WorkAmount").GetComponent<InputField>().text != "")
				workCount = float.Parse(roomTypes[i].transform.FindChild("WorkAmount").GetComponent<InputField>().text);
			else
				workCount = 0;

			//set workamount to max amount below 50%
			if(workCount > float.Parse(roomTypes[i].transform.FindChild("NumberBelow50").GetComponent<InputField>().text))
				workCount = float.Parse(roomTypes[i].transform.FindChild("NumberBelow50").GetComponent<InputField>().text);



			for(int j = 0; j < workCount; j++){
				RoomStats aBedroom = masterList[i][j];
				costForType += Mathf.Round((150 + 1000*((100-aBedroom.roomCondition))/100));
				//if we want to repair set condition back to 100
				if(repairing){aBedroom.roomCondition = 100;}
			}
			roomTypes[i].transform.FindChild("Cost").GetComponent<InputField>().text = costForType.ToString();
			totalCosts += costForType;
		}
		totalInput.text = totalCosts.ToString ();
		repairing = false;
	}
	public void ExecuteWork()
	{
		repairing = true;
		CalculateCost ();
	
		MasterReference.accountsPayable += totalCosts;
		Reception.monthlyReports[Reception.monthlyReports.Count-1].expenseRepairCost += totalCosts;
		//run twice to refresh the screen
		CalculateCost ();
	}
}


