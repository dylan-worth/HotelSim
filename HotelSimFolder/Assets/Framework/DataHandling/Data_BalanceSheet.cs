using UnityEngine;
using System.Collections;

public class Data_BalanceSheet : MonoBehaviour {

	public string fileName = "balanceSheet";
	private SaveData data;

	public void SaveBalanceSheet ()
	{
		//Create data instance
		data = new SaveData(fileName);
		for(int i =0; i < Reception.balanceSheets.Count; i++)
		{
		//Add keys with significant names and values

        string reportEnding =  Reception.balanceSheets[i].dateOfReport.month.ToString()
            +Reception.balanceSheets[i].dateOfReport.day.ToString()
            +Reception.balanceSheets[i].dateOfReport.year.ToString();
        data["Report Ending:" + reportEnding] = Reception.balanceSheets[i];
            /*
        data["dateOfReport"] = Reception.balanceSheets[i].dateOfReport;

		data["cashAtBank"] = Reception.balanceSheets[i].cashAtBank;
		data["accountsReceivable"] = Reception.balanceSheets[i].accountsReceivable;
		data["inventories"] = Reception.balanceSheets[i].inventories;
		data["totalCurrentAssets"] = Reception.balanceSheets[i].totalCurrentAssets;
		data["propretyAndEquipment"] = Reception.balanceSheets[i].propretyAndEquipment;
		data["totalAssets"] = Reception.balanceSheets[i].totalAssets;
		data["accountsPayable"] = Reception.balanceSheets[i].accountsPayable;
		data["carbonOffsetReceipts"] = Reception.balanceSheets[i].carbonOffsetReceipts;
		data["incomeTaxPayable"] = Reception.balanceSheets[i].incomeTaxPayable;
		data["dividendOwed"] = Reception.balanceSheets[i].dividendOwed;
		data["currentMaturityofLongtermDebt"] = Reception.balanceSheets[i].currentMaturityofLongtermDebt;
		data["totalCurrentLiabilities"] = Reception.balanceSheets[i].totalCurrentLiabilities;
		data["longTermDebt"] = Reception.balanceSheets[i].longTermDebt;
		data["shareCapital"] = Reception.balanceSheets[i].shareCapital;
		data["retainedEarnings"] = Reception.balanceSheets[i].retainedEarnings;
		data["ownersEquity"] = Reception.balanceSheets[i].ownersEquity;
		data["totalLiabilitiesAndOwnersEquity"] = Reception.balanceSheets[i].totalLiabilitiesAndOwnersEquity;
            */
		}
		
		//Save the data
        data.Save("C:" + "\\" + "Users" + "\\" + "Leonard" + "\\" + "Desktop" + "\\" + "testSavedData" + "\\" + fileName + ".xml");
		
		//Load the data we just saved
        data = SaveData.Load("C:" + "\\" + "Users" + "\\" + "Leonard" + "\\" + "Desktop" + "\\" + "testSavedData" + "\\" + fileName + ".xml");
		

	}
	public void OpenInExcel()
	{
		Application.OpenURL ("C:"+"\\"+"Users"+"\\"+"Leonard"+"\\"+"Desktop"+"\\"+"testSavedData"+"\\"+"balanceSheet.xml");
		//Application.OpenURL("http://www.w3schools.com/xml/plant_catalog.xml");
	}
}
