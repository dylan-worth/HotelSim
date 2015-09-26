using UnityEngine;
using System.Collections;

public class MonthlyReport {

	//date:
	public date currentMonth;
	//revenues:
	public float restaurantTake;
	//money made from each type of rooms for the month.
	public float revenueRoomLvl1;
	public float revenueRoomLvl2;
	public float revenueRoomLvl3;
	public float revenueRoomLvl4;
	public float revenueRoomLvl5;
	//money made from special bookings.
	public float revenueConferences;
	public float revenueEvents;
	public float revenueGroups;



	//Expenses:
	public float restaurantCost;
		//staff salaries.
	public float expenseEmployeeTotal(){
		float total = 0;
		total += (expenseDepartmentHeadStaff+expenseHotelServicesStaff
		          	+expenseFoodAndBeverageStaff+expenseConferenceStaff+expenseOtherStaff);
		return total;
	}
		public float expenseDepartmentHeadStaff;
		public float expenseHotelServicesStaff;
		public float expenseFoodAndBeverageStaff;
		public float expenseFrontDeskStaff;
		public float expenseConferenceStaff;
		public float expenseOtherStaff;

	//money spent on repairs
	public float expenseRepairCost;
	public float expenseUpgradeCost;


	//stats :
	//room unavailable stats
	public int outOfOrderRepair;
	public int outOfOrderUpgrade;
	public int outOfOrderUnusable;

	//number of available staff at start of the month.
	public int numbDepartmentHead;
	public int numbHotelServicesStaff;
	public int numbFoodAndBeverageStaff;
	public int numbFrontDeskStaff;
	public int numbConferenceStaff;
	public int numbOtherStaff;


	//number of available rooms of each type at month start.
	public int numbRoomLvl1;
	public int numbRoomLvl2;
	public int numbRoomLvl3;
	public int numbRoomLvl4;
	public int numbRoomLvl5;
	//number of days booked for each type.
	public int numbRoomLvl1Booked;
	public int numbRoomLvl2Booked;
	public int numbRoomLvl3Booked;
	public int numbRoomLvl4Booked;
	public int numbRoomLvl5Booked;
	//level of occupancy of all types of rooms for the month.
	//overall rate for the hotel.
	public float occupancy(){
		float rate =0f;
		rate = ((float)(numbRoomLvl1Booked+numbRoomLvl2Booked+numbRoomLvl3Booked+numbRoomLvl4Booked+numbRoomLvl5Booked))
			/(float)(currentMonth.numberOfWeeks*7*(numbRoomLvl1+numbRoomLvl2+numbRoomLvl3+numbRoomLvl4+numbRoomLvl5));
		rate *= 100f;
		return Mathf.Round(rate);
	}
	public float occupancyRoomLvl1(){
		float rate =0f;
		if(numbRoomLvl1!=0f){
			rate = (float)numbRoomLvl1Booked / ((float)currentMonth.numberOfWeeks * 7f * (float)numbRoomLvl1);
		}
		rate *= 100f;

		return Mathf.Round (rate);
	}
	public float occupancyRoomLvl2(){
		float rate =0f;
		if (numbRoomLvl2 != 0f) {
			rate = (float)numbRoomLvl2Booked / ((float)currentMonth.numberOfWeeks * 7f * (float)numbRoomLvl2);
		}
		rate *= 100f;

		return Mathf.Round (rate);
	}
	public float occupancyRoomLvl3(){
		float rate =0f;
		if (numbRoomLvl3 != 0f) {
			rate = (float)numbRoomLvl3Booked / ((float)currentMonth.numberOfWeeks * 7f * (float)numbRoomLvl3);
		}
		rate *= 100f;
		return Mathf.Round (rate);
	}
	public float occupancyRoomLvl4(){
		float rate =0f;
		if (numbRoomLvl4 != 0f) {
			rate = (float)numbRoomLvl4Booked / ((float)currentMonth.numberOfWeeks * 7f * (float)numbRoomLvl4);
		}
		rate *= 100f;
		return Mathf.Round (rate);
	}
	public float occupancyRoomLvl5(){
		float rate =0f;
		if (numbRoomLvl5 != 0f) {
			rate = (float)numbRoomLvl5Booked / ((float)currentMonth.numberOfWeeks * 7f * (float)numbRoomLvl5);
		}
		rate *= 100f;
		return Mathf.Round (rate);
	}
	public int totalBookings()
	{
		int total = 0;
		total = numbRoomLvl1Booked+numbRoomLvl2Booked+numbRoomLvl3Booked+numbRoomLvl4Booked+numbRoomLvl5Booked;
		return total;
	}
	public MonthlyReport DeepCopy()
	{
		MonthlyReport newReport = new MonthlyReport();

		newReport.currentMonth = this.currentMonth;
		newReport.restaurantTake = this.restaurantTake;
		newReport.revenueRoomLvl1 = this.revenueRoomLvl1;
		newReport.revenueRoomLvl2 = this.revenueRoomLvl2;
		newReport.revenueRoomLvl3 = this.revenueRoomLvl3;
		newReport.revenueRoomLvl4 = this.revenueRoomLvl4;
		newReport.revenueRoomLvl5 = this.revenueRoomLvl5;

		newReport.revenueConferences = this.revenueConferences;
		newReport.revenueEvents = this.revenueEvents;
		newReport.revenueGroups = this.revenueGroups;

		newReport.restaurantCost = this.restaurantCost;
		newReport.expenseDepartmentHeadStaff = this.expenseDepartmentHeadStaff;
		newReport.expenseHotelServicesStaff = this.expenseHotelServicesStaff;
		newReport.expenseFoodAndBeverageStaff = this.expenseFoodAndBeverageStaff;
		newReport.expenseFrontDeskStaff = this.expenseFrontDeskStaff;
		newReport.expenseConferenceStaff = this.expenseConferenceStaff;
		newReport.expenseOtherStaff = this.expenseOtherStaff;
	
		newReport.expenseRepairCost = this.expenseRepairCost;
		newReport.expenseUpgradeCost = this.expenseUpgradeCost;

		newReport.outOfOrderRepair = this.outOfOrderRepair;
		newReport.outOfOrderUpgrade = this.outOfOrderUpgrade;
		newReport.outOfOrderUnusable = this.outOfOrderUnusable;
	
		newReport.numbDepartmentHead = this.numbDepartmentHead;
		newReport.numbHotelServicesStaff = this.numbHotelServicesStaff;
		newReport.numbFoodAndBeverageStaff = this.numbFoodAndBeverageStaff;
		newReport.numbFrontDeskStaff = this.numbFrontDeskStaff;
		newReport.numbConferenceStaff = this.numbConferenceStaff;
		newReport.numbOtherStaff = this.numbOtherStaff;

		newReport.numbRoomLvl1 = this.numbRoomLvl1;
		newReport.numbRoomLvl2 = this.numbRoomLvl2;
		newReport.numbRoomLvl3 = this.numbRoomLvl3;
		newReport.numbRoomLvl4 = this.numbRoomLvl4;
		newReport.numbRoomLvl5 = this.numbRoomLvl5;

		newReport.numbRoomLvl1Booked = this.numbRoomLvl1Booked;
		newReport.numbRoomLvl2Booked = this.numbRoomLvl2Booked;
		newReport.numbRoomLvl3Booked = this.numbRoomLvl3Booked;
		newReport.numbRoomLvl4Booked = this.numbRoomLvl4Booked;
		newReport.numbRoomLvl5Booked = this.numbRoomLvl5Booked;


		return newReport;
	}

	public MonthlyReport(){}
	public MonthlyReport(date currentDate){
		this.currentMonth = currentDate.deepCopy();
	}



}
