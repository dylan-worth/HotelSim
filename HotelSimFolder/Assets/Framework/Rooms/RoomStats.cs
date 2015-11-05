using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class RoomStats : RoomBehaviour
{
	public int roomQuality = 1;			//Room quality (based on a tier system out of 5)
	public int daysSinceLastMaintained = 0;
	public int daysOccupied = 0;			//How many more days this bedroom is occupied for
	public int daysUnderConstruction = 0;	//Used for renovations
	public float roomCleanliness = 100; // Cleanliness of Room < 80 need minor cleaning < 50 Unusable
	public float roomCondition = 100;	//Condition (%) of the room (deteriorates over time)
	public float discountedPrice = 0;
	public bool inUse = false;
	public bool discount3Days = false;
	public bool discountNoBreakfast = false;
	public bool discountNoCancel = false;
	public groupType typeRented = groupType.noneBooked;
	static GameObject BedroomMenu;
	static ImageHolder IM;
	static Text TxtDaysOccupied;
	static Text TxtCondition;
	static Text TxtTitle;
	static Button btnRepair;
	static Button btnUpgrade2;
	static Button btnUpgrade3;
	static Button btnUpgrade4;
	static Button btnUpgrade5;

	private Vector3 lastestPOS = new Vector3(0f,0f,0f);

	Refurbishment refurbCTR;

	public void Awake ()
	{
		if (TxtDaysOccupied == null)
		{
			print ("Seting Text Box");
			GameObject UImenu = GameObject.FindGameObjectWithTag("UI");
			IM = UImenu.transform.GetComponent("ImageHolder") as ImageHolder;
			BedroomMenu =  UImenu.transform.FindChild("Popups").transform.FindChild("BedroomMenu").gameObject;
			TxtTitle = BedroomMenu.transform.FindChild("txtTitle").GetComponent<Text>();
			TxtDaysOccupied = BedroomMenu.transform.FindChild("txtDaysOccupied").GetComponent<Text>();
			TxtCondition = BedroomMenu.transform.FindChild("txtCondition").GetComponent<Text>();
			btnRepair  = BedroomMenu.transform.FindChild("btn_Repair").GetComponent<Button>();
			btnUpgrade2  = BedroomMenu.transform.FindChild("btn_Upgrade2").GetComponent<Button>();
			btnUpgrade3  = BedroomMenu.transform.FindChild("btn_Upgrade3").GetComponent<Button>();
			btnUpgrade4  = BedroomMenu.transform.FindChild("btn_Upgrade4").GetComponent<Button>();
			btnUpgrade5  = BedroomMenu.transform.FindChild("btn_Upgrade5").GetComponent<Button>();

		}
		BedroomBehaviour.AddToRoomList(this);

		refurbCTR = GameObject.Find("RefurbishmentCTR").GetComponent<Refurbishment>();
	}

	//Handles filling in the menu
	public void HandleMenu(Vector3 position)
	{
		lastestPOS = position;
		if (!BedroomMenu.activeSelf)
			BedroomMenu.SetActive(true);
		BedroomMenu.transform.position = new Vector3(Mathf.Clamp(position.x, 300f, Screen.width-300f),
		                                             Mathf.Clamp(position.y, 100f, Screen.height-100f), position.z);
		btnRepair.onClick.RemoveAllListeners();
		btnRepair.onClick.AddListener(Repair);
		btnRepair.GetComponentInChildren<Text>().text = "Repair: $" + Mathf.Round((150 + 1000*((100-roomCondition))/100));

		btnUpgrade2.onClick.RemoveAllListeners();
		btnUpgrade2.onClick.AddListener(delegate{Upgrade(2,1000);});
		btnUpgrade2.GetComponentInChildren<Text>().text = "Upgrade to Double: $" + 1000;

		btnUpgrade3.onClick.RemoveAllListeners();
		btnUpgrade3.onClick.AddListener(delegate{Upgrade(3,5000);});
		btnUpgrade3.GetComponentInChildren<Text>().text = "Upgrade to Deluxe: $" + 5000;

		btnUpgrade4.onClick.RemoveAllListeners();
		btnUpgrade4.onClick.AddListener(delegate{Upgrade(4,25000);});
		btnUpgrade4.GetComponentInChildren<Text>().text = "Upgrade to Suite: $" + 25000;

		btnUpgrade5.onClick.RemoveAllListeners();
		btnUpgrade5.onClick.AddListener(delegate{Upgrade(5,100000);});
		btnUpgrade5.GetComponentInChildren<Text>().text = "Upgrade to Master Suite: $" + 100000;
		//check quality of the room and sets the options base on that.
		switch(roomQuality){
		case 1: 
			SetPopUpStatus("Standard Room:", true, true, true, true);
				break;
		case 2:  
			SetPopUpStatus("Double Room:", false, true, true, true);
			break;
		case 3:  
			SetPopUpStatus("Deluxe Room:", false, false, true, true);
			break;
		case 4:  
			SetPopUpStatus("Suite:", false, false, false, true);
			break;
		case 5:  
			SetPopUpStatus("Master Suite:", false, false, false, false);
			break;
		}
		TxtDaysOccupied.text = daysOccupied.ToString();
		TxtCondition.text = roomCondition.ToString("F0") + "%";
		IM.setupStars((int)((roomCleanliness/100) * 6) );
	}
	//set the options for the room level and name for the pop up.
	void SetPopUpStatus(string name, bool upg2, bool upg3, bool upg4, bool upg5)
	{
		TxtTitle.text = name;
		btnUpgrade2.interactable = upg2;
		btnUpgrade3.interactable = upg3;
		btnUpgrade4.interactable = upg4;
		btnUpgrade5.interactable = upg5;
	}
	public void RemoveIcon(string icon)
	{
		switch (icon)
		{
		case "RepairUpgrade":
			if(transform.FindChild("Icon_Repair(Clone)") != null)
				Destroy(transform.FindChild("Icon_Repair(Clone)").gameObject);
			if(transform.FindChild("Icon_Upgrade(Clone)") != null)
				Destroy(transform.FindChild("Icon_Upgrade(Clone)").gameObject);

			break;
		}
	}

	public void DegradeRoom ()
	{
		if (inUse)
		{
			roomCleanliness = 0; 
			roomCondition -= 0.365f;
		}
		else if (roomCondition > 0)
		{
			if (roomCleanliness > 5)
				roomCleanliness -= 5f;
			roomCondition -= (0.1f); // Not in Use normal Decay
		}
		
		if (roomCondition <= 10 && GetComponent<Renderer>().material.color != Color.black)
		{
			GetComponent<Renderer>().material.color = Color.black; // Color Room Black when Degraded beyond normal use ****Add Player Warning****
		}
		daysSinceLastMaintained++;
		
		if (roomCleanliness < 80) 
		{															// If roomCleanliness is below %80 clean, add to production and get it cleaned
			BedroomBehaviour.ProdRequired += (100 - roomCleanliness); 
			BedroomBehaviour.uncleanRooms.Add (this);								// Adds itself to needs cleaning list, sorted by InUse (Priority), 
			// then most clean to least clean inorder to clean the most number of rooms (Also People are lazy)
		}
	}
	//repair back to 100%
	void Repair()
	{
		MasterReference.accountsPayable += Mathf.Round((150 + 1000*((100-roomCondition))/100));
		Reception.monthlyReports[Reception.monthlyReports.Count-1].expenseRepairCost += Mathf.Round((150 + 1000*((100-roomCondition))/100));

		this.GetComponent<Renderer>().material.color = Color.white;

		daysUnderConstruction += Mathf.CeilToInt(refurbCTR.maxRepairTime[roomQuality-1]*((100f-roomCondition)/100f));
		if(daysUnderConstruction == 0)//minimum repair time 1 day.
		{
			daysUnderConstruction = 1;
		}
		Debug.Log(roomCondition);
		Debug.Log(daysUnderConstruction);
		roomCondition = 100;
		roomCleanliness = 100;
		GameObject repairIcon = Instantiate(refurbCTR.repairIcon, gameObject.transform.parent.position, Quaternion.identity) as GameObject;
		repairIcon.transform.SetParent(transform);

		HandleMenu(lastestPOS);
	}
	//upgrade room quality
	void Upgrade(int level,int cost)
	{
		roomQuality = level;
		roomCondition = 100;
		roomCleanliness = 100;
		this.GetComponent<Renderer>().material.color = Color.white;
		MasterReference.accountsPayable += cost;
		Reception.monthlyReports[Reception.monthlyReports.Count-1].expenseUpgradeCost += cost;
		if(level > 1)
		{
			//GameObject upgradeEffect = GameObject.Find("/GameController/PrefabHolder").GetComponent<MainHolderScript>().UpgradeSmokePrefab;
			for(int i = 0; i < refurbCTR.upgradeList[level-2].Length; i++)
			{
				GameObject addOn = Instantiate(refurbCTR.upgradeList[level-2][i], transform.parent.position, Quaternion.identity) as GameObject;
				addOn.transform.SetParent(transform);
				addOn.transform.localScale = transform.parent.localScale;
			}
			//Instantiate(upgradeEffect, bedToAdd.transform.position, Quaternion.identity);
		}

		GameObject upgradeIcon = Instantiate(refurbCTR.upgradeIcon, gameObject.transform.parent.position, Quaternion.identity) as GameObject;
		upgradeIcon.transform.SetParent(gameObject.transform);
		daysUnderConstruction += refurbCTR.roomUpgradeTime[level-2];
		HandleMenu(lastestPOS);
	}


	//Books this room
	public bool Book(int daysToBook)
	{

		//Booking insurance check:
		if(daysToBook < 1)
			Debug.LogWarning("Warning: You're trying to book a room for less than a day.");
		
		//Temporarily change room color so we know it's taken
		this.GetComponent<Renderer>().material.color = Color.red;
		
		//Book the room:
		daysOccupied = daysToBook;
		inUse = true;
		return true;
	}
}
