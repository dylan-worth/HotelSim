using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class BalanceSheetDisp : MonoBehaviour {//Displays the balense sheet numbers on the screen inside a String.

	public GameObject toolbar;
	public GUIStyle styledText;
	public Text scrollableBalanceSheet;

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

	public void DisplayEndOfMonthBalanceSheet()
	{

		selectedReport = (((year-1)*12) + month-1);

		if (Reception.balanceSheets.Count != 0) {
			if(Reception.balanceSheets.Count > selectedReport)
			{
				WriteBalanceSheet(Reception.balanceSheets[selectedReport]);
			}
			else
			{
				scrollableBalanceSheet.text = "This reporting Period is unavailable.";
			}
		}
		else
		{
			scrollableBalanceSheet.text = "No balance sheet created yet. Simulate at least one month.";
		}


	
	}

	public void OpenTab()
	{
		year = Calendar.getDate().year-2016;
		month = (int)(Calendar.getDate().month)+1;
		DisplayEndOfMonthBalanceSheet();
		months selectedMonth = (months)month-1; 
		monthSelector.text = ""+selectedMonth;
		yearSelector.text = (year+2016).ToString();
	}

	void WriteBalanceSheet(BalanceSheet toDisplay)
	{
		scrollableBalanceSheet.text = "";
		scrollableBalanceSheet.text += "Period ending: " +  toDisplay.dateOfReport.month + " " + toDisplay.dateOfReport.year + " " + toDisplay.dateOfReport.day
			+ " " + toDisplay.dateOfReport.dayOfTheMonth;
		scrollableBalanceSheet.text += "\n" + "\n" +"Cash at Bank: " + "$".PadLeft(100) + toDisplay.cashAtBank.ToString ();
		scrollableBalanceSheet.text += "\n" + "\n" +"Account Receivable: " + "$".PadLeft(89) + toDisplay.accountsReceivable.ToString ();
		scrollableBalanceSheet.text += "\n" + "\n" +"Inventories: " + "$".PadLeft(104) + toDisplay.inventories.ToString ();
		scrollableBalanceSheet.text += "\n" + "\n" +"Total Current Assets: " + "$".PadLeft(87) + toDisplay.totalCurrentAssets.ToString ();
		scrollableBalanceSheet.text += "\n" + "\n" +"Proprety and equipment: " + "$".PadLeft(81) + toDisplay.propretyAndEquipment.ToString ();
		scrollableBalanceSheet.text += "\n" + "\n" +"Total Assets: " + "$".PadLeft(101) + toDisplay.totalAssets.ToString ();
		scrollableBalanceSheet.text += "\n" + "\n" +"Account Payable: " + "$".PadLeft(93) + toDisplay.accountsPayable.ToString ();
		scrollableBalanceSheet.text += "\n" + "\n" +"Carbon Offset Receipt: " + "$".PadLeft(84) + toDisplay.carbonOffsetReceipts.ToString ();
		scrollableBalanceSheet.text += "\n" + "\n" +"Income Tax Payable: " + "$".PadLeft(87) + 	toDisplay.incomeTaxPayable.ToString ();
		scrollableBalanceSheet.text += "\n" + "\n" +"Divident Owed: " + "$".PadLeft(97) + toDisplay.dividendOwed.ToString ();
		scrollableBalanceSheet.text += "\n" + "\n" +"Current Maturity of Long Term Debt: " + "$".PadLeft(60) + toDisplay.currentMaturityofLongtermDebt.ToString ();
		scrollableBalanceSheet.text += "\n" + "\n" +"Total Current Liability: " + "$".PadLeft(85) + toDisplay.totalCurrentLiabilities.ToString ();
		scrollableBalanceSheet.text += "\n" + "\n" +"Long Term Debt: " + "$".PadLeft(95) + toDisplay.longTermDebt.ToString ();
		scrollableBalanceSheet.text += "\n" + "\n" +"Share Capital: " + "$".PadLeft(100) + toDisplay.shareCapital.ToString ();
		scrollableBalanceSheet.text += "\n" + "\n" +"Retained Erning: " + "$".PadLeft(96) + toDisplay.retainedEarnings.ToString ();
		scrollableBalanceSheet.text += "\n" + "\n" +"Owners Equity: " + "$".PadLeft(99) + toDisplay.ownersEquity.ToString ();
		scrollableBalanceSheet.text += "\n" + "\n" +"Total Liabilities and Owners Equity: " + "$".PadLeft(64) + toDisplay.totalLiabilitiesAndOwnersEquity.ToString ();
			
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
