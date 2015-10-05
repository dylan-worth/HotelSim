using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

//Holds all information pertaining to reception behaviour's for a single week
//NOTE: these need default values for starting or something to setup first time use
public struct roomCosts{
	public float weekdayRoomCost;				//Cost of renting a room on a weekday (mon - thurs)
	public float weekendRoomCost;				//Cost of a room on weekends (fri/sat/sun)
	public float weekday3NightDiscount;		//Discount for a weekday room if rented for three nights
	public float weekend3NightDiscount;		//Discount for a weekend room if rented for three nights
	public float weekdayNoBreakfastDiscount;	//Discount for a weekday room if no breakfast 
	public float weekendNoBreakfastDiscount;	//Discount for a weekend room if no breakfast
	public float weekdayNoCancelDiscount;		//Discount for a weekday room if there's no cancellation return
	public float weekendNoCancelDiscount;		//Discount for a weekend room if there's no cancellation return
	roomCosts(float wDR, float wER, float wD3, float wE3, float wDB, float wEB, float wDC, float wEC )
	{
		weekdayRoomCost = wDR;				//Cost of renting a room on a weekday (mon - thurs)
		weekendRoomCost = wER;				//Cost of a room on weekends (fri/sat/sun)
		weekday3NightDiscount = wD3;		//Discount for a weekday room if rented for three nights
		weekend3NightDiscount = wE3;		//Discount for a weekend room if rented for three nights
		weekdayNoBreakfastDiscount = wDB;	//Discount for a weekday room if no breakfast 
		weekendNoBreakfastDiscount = wEB;	//Discount for a weekend room if no breakfast
		weekdayNoCancelDiscount = wDC;		//Discount for a weekday room if there's no cancellation return
		weekendNoCancelDiscount = wEC;		//Discount for a weekend room if there's no cancellation return
	}
	public roomCosts DeepCopy()
	{
		roomCosts other = new roomCosts();
		other.weekdayRoomCost = this.weekdayRoomCost;				
		other.weekendRoomCost = this.weekendRoomCost;				
		other.weekday3NightDiscount = this.weekday3NightDiscount;		
		other.weekend3NightDiscount = this.weekend3NightDiscount;		
		other.weekdayNoBreakfastDiscount = this.weekdayNoBreakfastDiscount;	
		other.weekendNoBreakfastDiscount = this.weekendNoBreakfastDiscount;	
		other.weekdayNoCancelDiscount = this.weekdayNoCancelDiscount;		
		other.weekendNoCancelDiscount = this.weekendNoCancelDiscount;	
		return other;
	}
}
public class ReceptionLog
{
	//Variables (Standard)
	public int month;	//Which week this data is for
	//Variables (Costs)
	//standard rooms
	public roomCosts standardRoom = new roomCosts();
	//double rooms
	public roomCosts doubleRoom = new roomCosts();
	//Deluxe rooms
	public roomCosts deluxeRoom = new roomCosts();
	//suite
	public roomCosts suiteRoom = new roomCosts();
	//master suites
	public roomCosts masterSuiteRoom = new roomCosts();


	public float conferenceHeadCost=50; 			//Conference shit, worry about this later
	public float conferenceWeekendDiscount;		//Conference shit, worry about this later
	public float conferenceRoomsDiscount;       //conference variable not yet in use.
	public float groupHeadCost = 50;                 //group variable not used yet.
	public float groupRoomsDiscount;            //groupd variable not yet in use.
	public float eventsCost=50;                    //event booking variable not in use.
	public float conferenceRevenueSplitFood;    //not in use
	public float groupRevenueSplitFood;         //not in use
	public float eventRevenueSplitFood;         //not in use.
	public float conferenceConfRoomsDiscount;   //not in use
	public float eventConfRoomsDiscount; 		//not in use.
	public float eventWeekEndDiscount;          //not in use.

	//Variables (Results)
	public int roomsAvailable;		//How many rooms the hotel had this week
	public int weekdayRoomsBooked;	//How many rooms were booked during the week
	public int weekendRoomsBooked;	//How many rooms were booked during the weekend


	public ReceptionLog DeepCopy()
	{
		ReceptionLog other = new ReceptionLog(); 

		other.month = this.month;
		
		other.standardRoom = this.standardRoom.DeepCopy();
		other.doubleRoom = this.doubleRoom.DeepCopy();
		other.deluxeRoom = this.deluxeRoom.DeepCopy();
		other.suiteRoom = this.suiteRoom.DeepCopy();
		other.masterSuiteRoom = this.masterSuiteRoom.DeepCopy();

		other.conferenceHeadCost= this.conferenceHeadCost; 			
		other.conferenceWeekendDiscount= this.conferenceWeekendDiscount;		
		other.conferenceRoomsDiscount= this.conferenceRoomsDiscount;      
		other.groupHeadCost= this.groupHeadCost;                 
		other.groupRoomsDiscount= this.groupRoomsDiscount;           
		other.eventsCost= this.eventsCost;                  
		other.conferenceRevenueSplitFood= this.conferenceRevenueSplitFood;  
		other.groupRevenueSplitFood= this.groupRevenueSplitFood;        
		other.eventRevenueSplitFood= this.eventRevenueSplitFood;       
		other.conferenceConfRoomsDiscount= this.conferenceConfRoomsDiscount;   
		other.eventConfRoomsDiscount= this.eventConfRoomsDiscount; 		
		other.eventWeekEndDiscount= this.eventWeekEndDiscount;        
		

		other.roomsAvailable= this.roomsAvailable;		
		other.weekdayRoomsBooked= this.weekdayRoomsBooked;	
		other.weekendRoomsBooked= this.weekendRoomsBooked;	
		return other;
	}
}

//This handles all information on renting rooms
public class Reception : MonoBehaviour 
{ 
	//arrays of median costs for room qualities.
	static float[] medianRoomCostWD = new float[5]{50f,75f,100f,150f,250f};
	static float[] medianRoomCostWE = new float[5]{65f,90f,130f,180f,350f};

    StaffMenu staffMenu;//reference to the staffmenu script
    Refurbishment refurbishmentTab;//reference to refurbishmenttab
    BankingReport bankingTab;//reference to bankingtab
    EMSReport emsTab;//reference to emsTab.
    RevenueManagement revenueManagementTab;//refrence to revenue management tab
    RandomEvent randomEvent;//reference to our random event controller.
	//Variables (Utility)
	static float debugDayDelay = 0.1f;

	public GameObject controller;
	public static int Days = 0;
	public static bool isSimulating = false;
	//Variables (Static)
	public static List<ReceptionLog> receptionLogs;	//Holds logs of the recieptions data over every week
	public static Reception singleton;				//Reference to the singleton of this script
	public static List<StaffingLog> staffingLogs;//holds logs of staffing data week by week.
	public static List<RestaurantBook> restaurantBooks;
	public static List<BalanceSheet> balanceSheets;
	public static List<MonthlyReport> monthlyReports;
	bool firstGeneration = true;


	//variables need to run Generation of reports.
	//values are set default at start then updated inside Simulation.
	MonthlyReport newMonthlyReport;
	RestaurantBook newRestaurantBook;
	ReceptionLog currentLog;
	StaffingLog currentLogStaff;

	void Awake()
	{
		refurbishmentTab = controller.transform.FindChild ("RefurbishmentCTR").gameObject.GetComponent<Refurbishment>();
		staffMenu = GameObject.FindGameObjectWithTag ("StaffingController").GetComponent<StaffMenu> ();
		bankingTab = controller.transform.FindChild ("BankingController").gameObject.GetComponent<BankingReport>();
		emsTab = controller.transform.FindChild("EMSController").gameObject.GetComponent<EMSReport>();
		revenueManagementTab = controller.transform.FindChild("RevenueManagerCTR").GetComponent<RevenueManagement>();
        randomEvent = controller.transform.FindChild("EventController").GetComponent<RandomEvent>();

		SingletonCheck();
		if(monthlyReports == null){
			monthlyReports = new List<MonthlyReport>();//create list on first run.
		}
		//Ensure there's a log for this week (special case for 1st time run)
		if (receptionLogs == null) {
			receptionLogs = new List<ReceptionLog> ();
		}
		if(restaurantBooks == null){
			restaurantBooks = new List<RestaurantBook>();//create a new list on first run.
		}
		//Ensure there's a log for this week (special case for 1st time run)
		if (staffingLogs == null) {
			staffingLogs = new List<StaffingLog> ();
		}
		//create the log once.
		if(balanceSheets == null){
			balanceSheets = new List<BalanceSheet>();//create new balance sheet list on first run.
		}
		GenerateNewMonthReports ();
		firstGeneration = true;
	}

	//Ensures a singleton is set
	void SingletonCheck()
	{
		if (singleton == null) {
			singleton = this;
			Staff.staffDepartmentHead = new List<StaffMember>();
			Staff.staffHotelServices = new List<StaffMember>();
			Staff.staffFrontDesk = new List<StaffMember>();
			Staff.staffFoodAndBeverages = new List<StaffMember>();
			Staff.staffConference = new List<StaffMember>();
			Staff.staffOthers = new List<StaffMember>();
		}
	}
	

	//Runs (simulates) the reception desk for a specified numer of weeks
	public static void RunSimulation(int numberOfWeeks)
	{
		singleton.StackedWeeklySimTrigger(numberOfWeeks);
		print ("Call");
	}

	//Wrapper used to trigger IEnumerator (eventually switch to void I guess?)
	public void StackedWeeklySimTrigger(int numberOfWeeks)
	{ 
		Invoke("WeeklySimTrigger",1);
	}

	//Wrapper for launching the co-routine (so it can be invoked with a delay)
	void WeeklySimTrigger()
	{ 
		if (!firstGeneration) {
			GenerateNewMonthReports ();
		} 
		else {firstGeneration = false;}
		StartCoroutine(RunWeeklySimulation()); 
	}

	//create monthly reports
	public void GenerateNewMonthReports()
	{
		newMonthlyReport = new MonthlyReport(Calendar.getDate());
		monthlyReports.Add(newMonthlyReport);

		//-----------------------------------------reception log
		currentLog = new ReceptionLog();
		currentLog = GameObject.FindGameObjectWithTag("RatesInput").GetComponent<RatesSetup>().newReceptionLog.DeepCopy();
		//Adds a new ReceptionLog to the list and Reference to the log we're currently editing.
		receptionLogs.Add (currentLog);
		//ReceptionLog currentLog = receptionLogs [receptionLogs.Count - 1];
		currentLog.month = receptionLogs.Count;
		//--------------------------------------staffing log
		//adds the staffing log to staffing log list.
		currentLogStaff = new StaffingLog();
		currentLogStaff = GameObject.FindGameObjectWithTag("StaffingController").GetComponent<StaffMenu>().newStaffingLog.DeepCopy();
		//add staff log to log list
		staffingLogs.Add(currentLogStaff);
		currentLogStaff.month = staffingLogs.Count;


	}


	//Runs (simulates) the reception desk for one month.
	public IEnumerator RunWeeklySimulation()
	{
		float staffCostMonthlyDP = 0;
		float staffCostMonthlyHS = 0;
		float staffCostMonthlyFB = 0;
		float staffCostMonthlyFD = 0;
		float staffCostMonthlyCF = 0;
		float staffCostMonthlyOT = 0;

		isSimulating = true;
		MasterReference.accountsPayable += bankingTab.GetLoanRepayments();
		MasterReference.accountsPayable += emsTab.GetMonthlyEMSCosts();
		MasterReference.accountsReceivable = 0f;


		//Monthly Report Creator.
		newMonthlyReport.numbDepartmentHead = Staff.staffDepartmentHead.Count;
		newMonthlyReport.numbHotelServicesStaff = Staff.staffHotelServices.Count;
		newMonthlyReport.numbFoodAndBeverageStaff = Staff.staffFoodAndBeverages.Count;
		newMonthlyReport.numbFrontDeskStaff = Staff.staffFrontDesk.Count;
		newMonthlyReport.numbConferenceStaff = Staff.staffConference.Count;
		newMonthlyReport.numbOtherStaff = Staff.staffOthers.Count;
		//-------------------------------------------//
		for (int weeks = 0; weeks < Calendar.getNumberOfWeeksInMonth(); weeks++) 
        {
            //Try and generate a random event. 
            randomEvent.InitiateRandomEvent();
			//once a week degrade our special rooms. 
			refurbishmentTab.DegradeSpecialRooms();
			//-------------------------------------------run day by day simulation---------------------------------------------------------//

            yield return new WaitForSeconds(debugDayDelay);
			//For each of the weekdays (Mon-Thurs inclusive) try to book rooms based on popularity vs cost
			WeekDays dayOfWeek;
			for (dayOfWeek = WeekDays.Monday; dayOfWeek <= WeekDays.Sunday; dayOfWeek++) 
			{
				//set the date.
				Calendar.addDay();
				Days++;
				//Determine how many special groups will book.
				specialBookings specialBooked = SpecialBookingRun();
				//Determine how many rooms we will try to book:


				int roomsToBook = (int)((BedroomBehaviour.allBedrooms.Count-specialBooked.numberOfRooms) * 
				                        (MasterReference.ReturnRegularBookingPop() / 100f));
				//check if enought ppl show up to trigger overbooking.
				if((roomsToBook-specialBooked.numberOfRooms)/(BedroomBehaviour.allBedrooms.Count-specialBooked.numberOfRooms) > 0.9 && revenueManagementTab.GetOverbooked())
				{
					Debug.LogWarning("overbooking");
					roomsToBook += (roomsToBook/revenueManagementTab.GetOverbooking());

					int overbookedCustomer = ((roomsToBook+specialBooked.numberOfRooms) - BedroomBehaviour.allBedrooms.Count);
					if(overbookedCustomer > 0)
					{
						for(int i = 0; i < overbookedCustomer; i++)
						{
							Debug.LogWarning("Customer overbooked! Cost to relocate: $300. Lost reputation!");
							MasterReference.accountsPayable += 300f;
							MasterReference.hotelExposure--;
						}
					}
				}
			
				//this is only for display. Will remove in later versions.
				if (dayOfWeek <= WeekDays.Thursday)
					Debug.Log ("Day "+Days+" Booking " + roomsToBook + " on weekday " + dayOfWeek);
				else 
					Debug.Log ("Day "+Days+" Booking " + roomsToBook + " on weekend day " + dayOfWeek);
				//*****************************************************//

				//Try to book specialgroups Rooms.---------------
				BedroomBehaviour.GetNumberOfRoomsAvailable();
				if(BedroomBehaviour.roomsAvailable > specialBooked.numberOfRooms && specialBooked.numberOfRooms != 0)
				{
					//check for price ratio here.
					if(CheckGroupCost(specialBooked)){
					Debug.LogError("Special Group Booked for "+ specialBooked.numberOfDays + " Days for "+ specialBooked.numberOfRooms + "rooms for a "+ specialBooked.type);
					for(int i = 0; i < specialBooked.numberOfRooms; i++)
					BookRoomSpecial(dayOfWeek, specialBooked);
					}
					else
					{
						print ("Prices too expensive, " + (specialBooked.numberOfRooms) + "Customers Lost");
					}
				}
				else {
					print ("Not enought rooms available, " + (specialBooked.numberOfRooms) + "Customers Lost");
					//break;
				}
				//-----------------------------------------------
				float occupancyDiscount =  controller.transform.FindChild ("RevenueManagerCTR").gameObject.GetComponent<RevenueManagement>().discountOnOccupancy();
			
				//Try to book that many rooms:
				for (int j = 0; j < roomsToBook; j++) {
					BedroomBehaviour.GetNumberOfRoomsAvailable ();
					if (BedroomBehaviour.roomsAvailable > 0) {
						BookRoom (dayOfWeek, occupancyDiscount);
					} else {
						print ("No Rooms Available, " + (roomsToBook - j) + "Customers Lost");
						//break;
					}
				}
				//set the price for current occupancy
				BedroomBehaviour.occupancyDiscount = occupancyDiscount/100f;
				//Step the day counter for all rooms
				BedroomBehaviour.StepDay (dayOfWeek);
				float ProdReq = BedroomBehaviour.ProdRequired;
				float StaffProduction = staffMenu.returnActualProduction(ProdReq);
				BedroomBehaviour.CleanRooms (StaffProduction);
				print (ProdReq + ", " +StaffProduction);

				float[] dailyCost = staffMenu.ReturnCostPerDay();
				staffCostMonthlyDP += dailyCost[0];
				staffCostMonthlyHS += dailyCost[1];
				staffCostMonthlyFB += dailyCost[2];
				staffCostMonthlyFD += dailyCost[3];
				staffCostMonthlyCF += dailyCost[4];
				staffCostMonthlyOT += dailyCost[5];


				//adds all 6 staff list's cost daily.
				MasterReference.accountsPayable += (dailyCost[0]+dailyCost[1]+dailyCost[2]
				                                    +dailyCost[3]+dailyCost[4]+dailyCost[5]);
			
				yield return new WaitForSeconds (debugDayDelay);
			}
			
		}
		//-----------------------------------SIMULATE THE RESTAURANT-----------------------------------------------//
		//create a new restaurant book with simulated data. 1 month at the time.
		newRestaurantBook = 
			GameObject.FindGameObjectWithTag("Restaurant").GetComponent<Restaurant>().SimulateMonth (newMonthlyReport.totalBookings()
			                                                                                        , 7*Calendar.getNumberOfWeeksInMonth());
		restaurantBooks.Add (newRestaurantBook);//creates a new log for the restaurant numbers. stored inside a list.
		for(int i = 0; i < restaurantBooks.Count; i++){
			Debug.Log(restaurantBooks[i].startDate.day);}
		//--------------------------------ASSIGN VALUE OF NEW BALANCE SHEET----------------------------------------//
		BalanceSheet newBalanceSheet = 
			new BalanceSheet(Calendar.getDate(), MasterReference.cashAtBank, MasterReference.accountsReceivable,
			                 MasterReference.inventories, MasterReference.totalCurrentAssets(), MasterReference.propretyAndEquipment,
			                 MasterReference.totalAssets(), MasterReference.accountsPayable, MasterReference.carbonOffsetReceipts,
			                 MasterReference.incomeTaxPayable, MasterReference.dividendOwed, MasterReference.currentMaturityofLongtermDebt,
			                 MasterReference.totalCurrentLiabilities(), MasterReference.longTermDebt, MasterReference.shareCapital,
			                 MasterReference.retainedEarnings, MasterReference.ownersEquity(), MasterReference.totalLiabilitiesAndOwnersEquity);
        //add date reference for nicer serialization
        date currentDate = Calendar.getDate();
        newBalanceSheet.dayOfTheMonth = currentDate.dayOfTheMonth;
        newBalanceSheet.month = currentDate.month;
        newBalanceSheet.year = currentDate.year;
        newBalanceSheet.day = currentDate.day;
        newBalanceSheet.numberOfWeeks = Calendar.getNumberOfWeeksInMonth(balanceSheets.Count%12);
		balanceSheets.Add(newBalanceSheet);
		//-------------------------------ADD DATA TO MONTHLY REPORT------------------------------------------------//
		newMonthlyReport.restaurantTake = (newRestaurantBook.totalBeverageSales + newRestaurantBook.totalFoodSales);

		refurbishmentTab.refreshTabs ();
		newMonthlyReport.numbRoomLvl1 = MasterReference.standardRooms;
		newMonthlyReport.numbRoomLvl2 = MasterReference.doubleRooms;
		newMonthlyReport.numbRoomLvl3 = MasterReference.deluxeRooms;
		newMonthlyReport.numbRoomLvl4 = MasterReference.suites;
		newMonthlyReport.numbRoomLvl5 = MasterReference.masterSuites;

		newMonthlyReport.expenseDepartmentHeadStaff = staffCostMonthlyDP;
		newMonthlyReport.expenseHotelServicesStaff = staffCostMonthlyHS;
		newMonthlyReport.expenseFoodAndBeverageStaff = staffCostMonthlyFB;
		newMonthlyReport.expenseFrontDeskStaff = staffCostMonthlyFD;
		newMonthlyReport.expenseConferenceStaff = staffCostMonthlyCF;
		newMonthlyReport.expenseOtherStaff = staffCostMonthlyOT;
		//-----------------------------------ADD AMOUNTS FOR END OF MONTH DATA-------------------------------------//
		MasterReference.accountsReceivable += (newRestaurantBook.totalBeverageSales + newRestaurantBook.totalFoodSales);
		MasterReference.accountsPayable += MasterReference.guessComfortMonthlySpending + MasterReference.upgradeCost;//cost of guest comfort amunities.
		//-----------------------------------ADD Account Payable and Receivable at END OF MONTH--------------------//
		MasterReference.cashAtBank -= MasterReference.accountsPayable;
		MasterReference.cashAtBank += MasterReference.accountsReceivable;

		newBalanceSheet.cashAtBank = MasterReference.cashAtBank;


		//---------------------------------------------------------------------------------------------------------//
		bankingTab.GetComponent<BankingReport>().EndMonth();//tick all loan duration down once.
		MasterReference.currentMonthInt++;
		isSimulating = false;
		MasterReference.accountsPayable = 0f;
		//---------------------------------------------SAVE DATA---------------------------------------------------//
		//currently adding everytime you save creating HUGE files.
		//var newBSheetTest = Data_Save_CTNBalanceSheet.Load(Path.Combine(Application.persistentDataPath, "Data_Save_CTNBalanceSheets.xml"));
		//newBSheetTest.
		//newBSheetTest.Save(Path.Combine(Application.persistentDataPath, "Data_Save_CTNBalanceSheets.xml"));
		//-------------------------------------END OF SIMULATION LOOP---------------------------------------------//
		//-------------------------------------RESET SOME DATA----------------------------------------------------//
		MasterReference.upgradeCost = 0f;
	}
	//Used to book group rooms
	public static void BookRoomSpecial(WeekDays dayOfWeek, specialBookings special)
	{
		RoomStats aRoom = BedroomBehaviour.GetNextAvailableRoom();
		aRoom.Book (special.numberOfDays);
		Debug.Log("Room booked for " + special.numberOfDays + " days for special event!");
	}
	//Used to try to book a room at this hotel
	public static void BookRoom(WeekDays dayOfWeek, float occupancyDiscount)
	{
		int numberOfDays = Random.Range(1, 5);

		//Determine if there will be a discount for 3+ days booking:
		bool discount3Days = true;
		if(numberOfDays < 3)
			discount3Days = false;

		RoomStats aRoom = BedroomBehaviour.GetNextAvailableRoom();
		if( aRoom == null)
		{
			Debug.Log("All Rooms are unsuable");
		}

		//check the price to remove cancel booking.
		if (CheckRoomCost (aRoom, dayOfWeek,occupancyDiscount)) {
			aRoom.Book (numberOfDays);
			aRoom.discount3Days = discount3Days;
			aRoom.discountNoBreakfast = BoolGen (40);
			aRoom.discountNoCancel = BoolGen (30);
			Debug.Log ("Room booked for " + numberOfDays + " days!");
		} 
		else 
		{
			//add to monthly report the number of customer lost here.
			Debug.Log ("Room too expensive, customer lost.");
		}
	}
	//Random bool generator. Receives the probability of true outcome. 50/50 default.
	public static bool BoolGen(int trueProb = 50)
	{
		return(Random.Range(1,101) <= trueProb);
	}

	public specialBookings SpecialBookingRun()
	{
		int chanceToBook = (int)(MasterReference.ReturnGroupBookingPop ()*0.2f);
		if (BoolGen (chanceToBook)) {
			specialBookings special = GroupBookings.bookSpecial ();
			return special;
		} 
		else 
		{
			specialBookings special = new specialBookings(0 ,0 , groupType.noneBooked);
			return special;
		}
	}
	//-----checks the median costs and discount based on occupancy.Return a booking or not as a bool----//
	static bool CheckRoomCost(RoomStats aRoom, WeekDays day, float occupancyDiscount)
	{
		int roomQuality = aRoom.roomQuality;
		int percentChance;
		float currentCost = 0f;
		bool WD = true;



		if (day <= WeekDays.Wednesday) 
		{
			WD = true;
		} 
		else 
		{
			WD = false;
		}
		
		switch (roomQuality) 
		{
		case 1:
			if(WD){currentCost = receptionLogs[receptionLogs.Count-1].standardRoom.weekdayRoomCost;}
			else{currentCost = receptionLogs[receptionLogs.Count-1].standardRoom.weekdayRoomCost;}
			break;
		case 2:
			if(WD){currentCost = receptionLogs[receptionLogs.Count-1].doubleRoom.weekdayRoomCost;}
			else{currentCost = receptionLogs[receptionLogs.Count-1].doubleRoom.weekdayRoomCost;}
			break;
			
		case 3:
			if(WD){currentCost = receptionLogs[receptionLogs.Count-1].deluxeRoom.weekdayRoomCost;}
			else{currentCost = receptionLogs[receptionLogs.Count-1].deluxeRoom.weekdayRoomCost;}
			break;
			
		case 4:
			if(WD){currentCost = receptionLogs[receptionLogs.Count-1].suiteRoom.weekdayRoomCost;}
			else{currentCost = receptionLogs[receptionLogs.Count-1].suiteRoom.weekdayRoomCost;}
			break;
			
		case 5:
			if(WD){currentCost = receptionLogs[receptionLogs.Count-1].masterSuiteRoom.weekdayRoomCost;}
			else{currentCost = receptionLogs[receptionLogs.Count-1].masterSuiteRoom.weekdayRoomCost;}
			break;
		}
		if (WD) 
		{
			percentChance = (int)((medianRoomCostWD[roomQuality-1]/(currentCost*occupancyDiscount/100f))*0.75f*100f);
		}
		else 
		{
			percentChance =  (int)((medianRoomCostWE[roomQuality-1]/(currentCost*occupancyDiscount/100f))*0.75f*100f);
		}
		return BoolGen (percentChance);
	}
	static bool CheckGroupCost(specialBookings group)
	{
		int ratioPrice = 0;
		int count = 0;
		int qualityTotal=0;
		float averageQuality=0f;
		foreach (RoomStats aRoom in BedroomBehaviour.allBedrooms) {
			count++;
			qualityTotal += aRoom.roomQuality;
		}
		averageQuality = qualityTotal / count;
	
		switch(group.type)
		{
		case groupType.conference:
			ratioPrice = (int)(averageQuality*50f/receptionLogs[receptionLogs.Count-1].conferenceHeadCost*100f);
			break;
		case groupType.group:
			ratioPrice = (int)(averageQuality*50f/receptionLogs[receptionLogs.Count-1].groupHeadCost*100f);
			break;
		case groupType.specialEvent:
			ratioPrice = (int)(averageQuality*50f/receptionLogs[receptionLogs.Count-1].eventsCost*100f);
			break;
		}
	
		return BoolGen (ratioPrice);

	
	}
}
