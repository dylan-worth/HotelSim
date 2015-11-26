using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    ToolbarOptions toolbar;//overworld display script.
    

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
	RatesSetup rateSetup;
    AssetSwapper assetSwapper;
	GroupBookController groupController;
	CalendarController calendarController;
	FeedbackController feedbackController;
    Serializer_Deserializer dataProcessor;
    Advertisment adController;

	void Awake()
	{
		refurbishmentTab = controller.transform.FindChild ("RefurbishmentCTR").gameObject.GetComponent<Refurbishment>();
		staffMenu = GameObject.FindGameObjectWithTag ("StaffingController").GetComponent<StaffMenu> ();
		bankingTab = controller.transform.FindChild ("BankingController").gameObject.GetComponent<BankingReport>();
		emsTab = controller.transform.FindChild("EMSController").gameObject.GetComponent<EMSReport>();
		revenueManagementTab = controller.transform.FindChild("RevenueManagerCTR").GetComponent<RevenueManagement>();
        randomEvent = controller.transform.FindChild("EventController").GetComponent<RandomEvent>();
		rateSetup = controller.transform.FindChild("RatesSetUp").GetComponent<RatesSetup>();
		groupController = controller.transform.FindChild("GroupBooking").GetComponent<GroupBookController>();
		calendarController = controller.transform.FindChild("CalendarController").GetComponent<CalendarController>();
		feedbackController = controller.transform.FindChild("FeedBackController").GetComponent<FeedbackController>();
        dataProcessor = GameObject.Find("DataCollection").GetComponent<Serializer_Deserializer>();
        assetSwapper = controller.transform.FindChild("AssetController").GetComponent<AssetSwapper>();
        toolbar = GameObject.FindGameObjectWithTag("UI").transform.FindChild("OverWorld").GetComponent<ToolbarOptions>();
        adController = controller.transform.FindChild("AdvertController").gameObject.GetComponent<Advertisment>();

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

		medianRoomCostWD = rateSetup.medianRoomCostWD;
		medianRoomCostWE = rateSetup.medianRoomCostWE;

	}

    void Start()
    {
        toolbar.UpdateCredits();
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
		//print ("Call");
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
		newMonthlyReport = new MonthlyReport(Calendar.GetDate());
		monthlyReports.Add(newMonthlyReport);

		//-----------------------------------------reception log
		currentLog = new ReceptionLog();
		currentLog = GameObject.FindGameObjectWithTag("RatesInput").GetComponent<RatesSetup>().newReceptionLog.DeepCopy();
		//Adds a new ReceptionLog to the list and Reference to the log we're currently editing.
		receptionLogs.Add (currentLog);
		currentLog.month = receptionLogs.Count;
	}

	#region Simulation Loop
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
		for (int weeks = 0; weeks < Calendar.GetNumberOfWeeksInMonth(); weeks++) 
        {
           
            //Try and generate a random event each week. 
            randomEvent.InitiateRandomEvent();
			//runs the stafftraining function to increase or decrease staff training.
			staffMenu.TrainStaff();
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
                //Ticks the billboard duration by one.
                assetSwapper.TickDown();
				//Determine how many special groups will book.
				specialBookings specialBooked = SpecialBookingRun();
				//Determine how many rooms we will try to book:
				int roomsToBook = (int)((calendarController.seasonalTrends[Calendar.GetDayOfTheYear()]) * 
				                        (MasterReference.ReturnRegularBookingPop() / 100f));
                
				//check if enought ppl show up to trigger overbooking.
				if((roomsToBook-specialBooked.numberOfRooms)/(BedroomBehaviour.allBedrooms.Count-specialBooked.numberOfRooms) > 0.9 && revenueManagementTab.GetOverbooked())
				{

					roomsToBook += (roomsToBook/revenueManagementTab.GetOverbooking());

					int overbookedCustomer = ((roomsToBook+specialBooked.numberOfRooms) - BedroomBehaviour.allBedrooms.Count);
					if(overbookedCustomer > 0)
					{
						for(int i = 0; i < overbookedCustomer; i++)
						{
							feedbackController.TryGenFeedBack("OverBooked",70f);//70% chance an overbooked customer will complain.

							MasterReference.accountsPayable += 300f;
							MasterReference.hotelExposure--;
						}
					}
				}

				//Try to book specialgroups Rooms.---------------
				BedroomBehaviour.GetNumberOfRoomsAvailable();
				if(BedroomBehaviour.roomsAvailable > specialBooked.numberOfRooms && specialBooked.numberOfRooms != 0)
				{
					//check for price ratio here.
					if(CheckGroupCost(specialBooked))
					{
						//Debug.LogError("Special Group Booked for "+ specialBooked.numberOfDays + " Days for "+ specialBooked.numberOfRooms + "rooms for a "+ specialBooked.type);
						for(int i = 0; i < specialBooked.numberOfRooms; i++)
						BookRoomSpecial(dayOfWeek, specialBooked);
					}
					else
					{
						feedbackController.TryGenFeedBack("Expensive",30f);//30% chance customoer will complain for overpriced.
					}
				}
				else if(specialBooked.numberOfRooms != 0)
				{
					//need to provide feedback to the player that a group tryed to book rooms but hotel didn't have capacity.
					feedbackController.TryGenFeedBack("Full",10f);//10% chance customer will complain due to full capacity.
				}

				//-----------------------------------------------
				float occupancyDiscount =  controller.transform.FindChild ("RevenueManagerCTR").gameObject.GetComponent<RevenueManagement>().discountOnOccupancy();
			
				//Try to book that many rooms:
				for (int j = 0; j < roomsToBook; j++) 
				{
					BedroomBehaviour.GetNumberOfRoomsAvailable ();
					if (BedroomBehaviour.roomsAvailable > 0) 
					{
						BookRoom (dayOfWeek, occupancyDiscount);
					} 
					else 
					{
						Debug.Log("no rooms");
						feedbackController.TryGenFeedBack("Full",10f);//10% chance customer will complain due to full capacity.
					}
				}
				//set the price for current occupancy
				BedroomBehaviour.occupancyDiscount = occupancyDiscount/100f;
				//Step the day counter for all rooms
				BedroomBehaviour.StepDay (dayOfWeek);
				float ProdReq = BedroomBehaviour.ProdRequired;
				float StaffProduction = staffMenu.returnActualProduction(ProdReq);
				BedroomBehaviour.CleanRooms (StaffProduction);
				//print (ProdReq + ", " +StaffProduction);

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

                toolbar.UpdateCredits();
                yield return new WaitForSeconds (debugDayDelay);
			}
			
		}
		//-----------------------------------SIMULATE THE RESTAURANT-----------------------------------------------//
		//create a new restaurant book with simulated data. 1 month at the time.
		newRestaurantBook = 
			GameObject.FindGameObjectWithTag("Restaurant").GetComponent<Restaurant>().SimulateMonth (newMonthlyReport.totalBookings()
			                                                                                        , 7*Calendar.GetNumberOfWeeksInMonth());
		restaurantBooks.Add (newRestaurantBook);//creates a new log for the restaurant numbers. stored inside a list.
		
		//--------------------------------ASSIGN VALUE OF NEW BALANCE SHEET----------------------------------------//
		BalanceSheet newBalanceSheet = 
			new BalanceSheet(Calendar.GetDate(), MasterReference.cashAtBank, MasterReference.accountsReceivable,
			                 MasterReference.inventories, MasterReference.totalCurrentAssets(), MasterReference.propretyAndEquipment,
			                 MasterReference.totalAssets(), MasterReference.accountsPayable, MasterReference.carbonOffsetReceipts,
			                 MasterReference.incomeTaxPayable, MasterReference.dividendOwed, MasterReference.currentMaturityofLongtermDebt,
			                 MasterReference.totalCurrentLiabilities(), MasterReference.longTermDebt, MasterReference.shareCapital,
			                 MasterReference.retainedEarnings, MasterReference.ownersEquity(), MasterReference.totalLiabilitiesAndOwnersEquity);
        //add date reference for nicer serialization
        Date currentDate = Calendar.GetDate();
        newBalanceSheet.dayOfTheMonth = currentDate.dayOfTheMonth;
        newBalanceSheet.month = currentDate.month;
        newBalanceSheet.year = currentDate.year;
        newBalanceSheet.day = currentDate.day;
        newBalanceSheet.numberOfWeeks = Calendar.GetNumberOfWeeksInMonth(balanceSheets.Count%12);
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
        adController.Tick();//Ends the month for all ad campaigns and decay/improves hotel exposures.
        //-------------------------------------END OF SIMULATION LOOP---------------------------------------------//
       
        //------------------------------------------AUTO SAVE-----------------------------------------------------//
        dataProcessor.SaveGame();
		//-------------------------------------RESET SOME DATA----------------------------------------------------//
		MasterReference.upgradeCost = 0f;
	}
	#endregion
	//Used to book group rooms
	public static void BookRoomSpecial(WeekDays dayOfWeek, specialBookings special)
	{
		RoomStats aRoom = BedroomBehaviour.GetNextAvailableRoom();
		aRoom.Book (special.numberOfDays);
		//Debug.Log("Room booked for " + special.numberOfDays + " days for special event!");
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
			//Debug.Log ("Room booked for " + numberOfDays + " days!");
		} 
		else 
		{
			//room too expensive.
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
		if (BoolGen (chanceToBook)) 
		{
			specialBookings newBooking = groupController.BookSpecial();
			return newBooking;
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
