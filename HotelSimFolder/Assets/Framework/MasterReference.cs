using UnityEngine;
using System.Collections;


//This holds all the main information about this hotel
public static class MasterReference
{
	public static int currentMonthInt = 1;
	//Variables (Finances)
	
	//***************//
	//new variables to replace legacy code.
	//public static float monthlyRevenue = 0f;
	//public static float monthlyExpenses = 0f;
	//public static float getMonthlyProfits(){return monthlyRevenue-monthlyExpenses;}
	//***************//

	public static float creditBalance = 0f;		//How much money the hotel currently has to spend / work with
	//public static float costofStaff = 0f;
	public static float profit = 0f;
	public static float debt = 0f;				//How much debt this hotel currently has
	//-------------------------------------------------EXTRA DATA---------------------------------------------------------//
	public static float guessComfortMonthlySpending=0f;



	//--------------------------------------------HOTEL POPULARITY----------------------------------------------------------//
	//Variables (Reputation)                   
	public static float hotelPopularity = 20f;	//How popular this hotel is (affects number of customers coming in) as a percent
	public static float hotelExposure = 0f;		//adds to hotel popularity, most effective on regular bookings.
	public static float hotelCorporateExposure = 0f;    //add to hotel popularity. most effective for special bookings. 
	public static float hotelTourismExposure = 0f;		//add to hotel popularity. Tourist not yet implemented.

	public static float emsModifier = 0f;

	//returns the popularity of the hotel. The front desk condition and level also affects the booking. A max condition max level 
	//front desk will add 10% to booking numbers.
	public static float ReturnRegularBookingPop(){
		float total = 0f;
		total = hotelPopularity + hotelExposure + emsModifier+ ((hotelCorporateExposure+hotelTourismExposure)/5f);
		total *= ((frontDeskLevel*2)+(frontDeskCondition/100f));
		return total;
	}
	public static float ReturnGroupBookingPop(){
		float total = 0f;
		total = hotelPopularity + hotelCorporateExposure + emsModifier+((hotelExposure+hotelTourismExposure)/5f);
		return total;
	}
	public static float ReturnTouristBookingPop(){
		float total = 0f;
		total = hotelPopularity + hotelTourismExposure + emsModifier+((hotelExposure+hotelCorporateExposure)/5f);
		return total;
	}
	//--------------------------------------------Staffing and Room Variables---------------------------------------------------------------//


	//public static float hotelQuality = 0;		//Determined by the quality of the room
	//Variables (Chance of Booking)

	//StaffingCosts
	public static float StaffingCosts = 0f;

	//Level status of special rooms;
	public static int restaurantLevel = 1;        //edit inside refurbishment tab.
	public static int conferenceRoomLevel = 1;
	public static int barLevel = 1;
	public static int frontDeskLevel = 1;
	//condition of the special rooms
	public static float restaurantConditionRepair = 100f;       //edit inside refurbishment tab.
	public static float conferenceCondition = 100f;
	public static float barCondition = 100f;
	public static float frontDeskCondition = 100f;

	//cost to the hotel for repairs and upgrades.
	public static float repairCosts = 0f;
	public static float upgradeCost = 0f;
	//current level of pay selected in staffing menu.
	public static float[] payScales = new float[6];
	public static int payBandHS;
	public static int payBandFB;
	public static int payBandFD;
	public static int payBandConference;
	public static int payBandOthers;

	//number of rooms in the hotel.
	public static int standardRooms = 15;
	public static int doubleRooms = 0;
	public static int deluxeRooms = 0;
	public static int suites = 0;
	public static int masterSuites = 0;

	//New Financial data.
	//--------
	//--------
	//currentAssets
	//--------------------------------------------------BALANCE SHEET------------------------------------------------//
	public static float cashAtBank = 200000;
	public static float accountsReceivable = 0;
	public static float inventories = 13757;
	
	public static float totalCurrentAssets(){
		float toReturn = cashAtBank + accountsReceivable + inventories;
		return toReturn;
	}
	
	//Property & Equipment
	public static float propretyAndEquipment = 5200641;
	
	public static float totalAssets (){
		float toReturn = propretyAndEquipment + totalCurrentAssets();
		return toReturn;
	}
	//current Liabilities
	public static float accountsPayable =	0;		
	public static float carbonOffsetReceipts = 0;		
	public static float incomeTaxPayable = 0;		
	public static float dividendOwed = 0;		
	public static float currentMaturityofLongtermDebt = 0;	
	
	public static float totalCurrentLiabilities (){
		float toReturn = accountsPayable + carbonOffsetReceipts + incomeTaxPayable 
			+ dividendOwed + currentMaturityofLongtermDebt;

		return toReturn;
	}
	//Long-term Liabilities	  
	public static float longTermDebt = 0;		
	
	//	Owners' Equity	 		 
	public static float shareCapital = 4750000;		
	public static float retainedEarnings = 136295;		
	
	public static float ownersEquity (){
		float toReturn = shareCapital + retainedEarnings;
		return toReturn;
	}
	public static float totalLiabilitiesAndOwnersEquity = 5487819;
	//--------------------------------------------------END OF BALANCE SHEET--------------------------------------------//
}
