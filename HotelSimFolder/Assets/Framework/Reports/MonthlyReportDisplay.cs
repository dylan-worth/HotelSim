using UnityEngine;
using UnityEngine.UI;

public class MonthlyReportDisplay : MonoBehaviour {

	public GameObject toolbar;
	public Text scrollableMonthlyReport;
	
	public Text yearSelector;
	public Text monthSelector;
	
	
	public int year = 1;
	public int month = 1;
	int selectedReport = 0;
	
	void Update()
	{
		if (Input.GetAxis ("Cancel") != 0) {
			toolbar.SetActive(true);
			gameObject.SetActive(false);
		}

	}
	public void DisplayMonthlyReport()
	{
		selectedReport = (((year-1)*12) + month-1);
		
		if (Reception.monthlyReports.Count != 0) {
			if(Reception.monthlyReports.Count > selectedReport)
			{
				WriteMonthlyReport(Reception.monthlyReports[selectedReport]);
			}
			else
			{
				scrollableMonthlyReport.text = "This reporting Period is unavailable.";
			}
		}
		else
		{
			scrollableMonthlyReport.text = "No monthly report created yet. Simulate at least one month.";
		}
	}
	public void OpenTab()
	{
		year = Calendar.getDate().year-2016;
		month = (int)(Calendar.getDate().month)+1;
		DisplayMonthlyReport();
		months selectedMonth = (months)month-1; 
		monthSelector.text = ""+selectedMonth;
		yearSelector.text = (year+2016).ToString();
	}

	void WriteMonthlyReport(MonthlyReport toDisplay)
	{

		scrollableMonthlyReport.text = "";

		scrollableMonthlyReport.text += "Report ending: " +  toDisplay.currentMonth.month + " " +toDisplay.currentMonth.year;
		scrollableMonthlyReport.text += "\n" + "Hotel Occupancy: "+toDisplay.occupancy().ToString()+"%";
		scrollableMonthlyReport.text += "\n" + "Restaurant Revenue: $" + toDisplay.restaurantTake.ToString();

		scrollableMonthlyReport.text += "\n" + "Standard Rooms: $" + toDisplay.revenueRoomLvl1.ToString();
		scrollableMonthlyReport.text += " " + "Number: " + toDisplay.numbRoomLvl1.ToString();
		scrollableMonthlyReport.text += " " + "Occupancy: " + toDisplay.occupancyRoomLvl1().ToString()+"%";
		scrollableMonthlyReport.text += " " + "Number Of Bookings: " + toDisplay.numbRoomLvl1Booked.ToString();

		scrollableMonthlyReport.text += "\n" + "Double Rooms: $" +  toDisplay.revenueRoomLvl2.ToString();
		scrollableMonthlyReport.text += "  " +  "Number: " + toDisplay.numbRoomLvl2.ToString();
		scrollableMonthlyReport.text += "  " + "Number Of Bookings: " + toDisplay.numbRoomLvl2Booked.ToString();
		scrollableMonthlyReport.text += "  " + "Occupancy: " + toDisplay.occupancyRoomLvl2().ToString()+"%";

		scrollableMonthlyReport.text += "\n" + "Deluxe Rooms: $" +  toDisplay.revenueRoomLvl3.ToString();
		scrollableMonthlyReport.text += "  " +  "Number: " + toDisplay.numbRoomLvl3.ToString();
		scrollableMonthlyReport.text += "  " + "Number Of Bookings: " + toDisplay.numbRoomLvl3Booked.ToString();
		scrollableMonthlyReport.text += "  " + "Occupancy: " + toDisplay.occupancyRoomLvl3().ToString()+"%";

		scrollableMonthlyReport.text += "\n" + "Suites: $" +  toDisplay.revenueRoomLvl4.ToString();
		scrollableMonthlyReport.text += "  " +  "Number: " + toDisplay.numbRoomLvl4.ToString();
		scrollableMonthlyReport.text += "  " + "Number Of Bookings: " + toDisplay.numbRoomLvl4Booked.ToString();
		scrollableMonthlyReport.text += "  " + "Occupancy: " + toDisplay.occupancyRoomLvl4().ToString()+"%";

		scrollableMonthlyReport.text += "\n" + "Master Suites: $" +  toDisplay.revenueRoomLvl5.ToString();
		scrollableMonthlyReport.text += "  " +  "Number: " + toDisplay.numbRoomLvl5.ToString();
		scrollableMonthlyReport.text += "  " + "Number Of Bookings: " + toDisplay.numbRoomLvl5Booked.ToString();
		scrollableMonthlyReport.text += "  " + "Occupancy: " + toDisplay.occupancyRoomLvl5().ToString()+"%";

		scrollableMonthlyReport.text += "\n" + "Staff: ";
		scrollableMonthlyReport.text += "\n" +  "Department Heads: "+toDisplay.numbDepartmentHead.ToString();
		scrollableMonthlyReport.text += "\n" + "Service Staff: "+toDisplay.numbHotelServicesStaff.ToString();
		scrollableMonthlyReport.text += "\n" +  "Food And Beverage: "+toDisplay.numbFoodAndBeverageStaff.ToString();
		scrollableMonthlyReport.text += "\n" +  "Front Desk: "+toDisplay.numbFrontDeskStaff.ToString();
		scrollableMonthlyReport.text += "\n" +   "Conference: "+toDisplay.numbConferenceStaff.ToString();
		scrollableMonthlyReport.text += "\n" +  "Other: "+toDisplay.numbOtherStaff.ToString();


		scrollableMonthlyReport.text += "\n" + "Expenses: ";
		scrollableMonthlyReport.text += "\n" + "Staffing: "+toDisplay.expenseEmployeeTotal().ToString();
		scrollableMonthlyReport.text += "\n" + "Repairs: "+toDisplay.expenseRepairCost.ToString();
		scrollableMonthlyReport.text += "\n" + "Upgrade: "+toDisplay.expenseUpgradeCost.ToString();
			
	}



	public void SetYear(int value)
	{
		year = value;
		yearSelector.text = (value+2016).ToString();
	}
	public void SetMonth(int value)
	{
		month = value;
		months selectedMonth = (months)value-1; 
		monthSelector.text = ""+selectedMonth;
	}

}
