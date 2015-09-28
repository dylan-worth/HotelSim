using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class Data_BalanceSheet : MonoBehaviour {

	public string fileName = "balanceSheet";
	private SaveData data;

	public void SaveBalanceSheet ()
	{

        Reception.balanceSheets[0].Save("savedBalanceSheet.xml");
        /*
		//Create data instance
		data = new SaveData(fileName);
		for(int i =0; i < Reception.balanceSheets.Count; i++)
		{
		//Add keys with significant names and values

        string reportEnding =  Reception.balanceSheets[i].dateOfReport.month.ToString()
            +Reception.balanceSheets[i].dateOfReport.day.ToString()
            +Reception.balanceSheets[i].dateOfReport.year.ToString();
        data["Report Ending:" + reportEnding] = Reception.balanceSheets[i];

		}
		
		//Save the data
        data.Save("C:" + "\\" + "Users" + "\\" + "Leonard" + "\\" + "Desktop" + "\\" + "testSavedData" + "\\" + fileName + ".xml");
		
		//Load the data we just saved
        //data = SaveData.Load("C:" + "\\" + "Users" + "\\" + "Leonard" + "\\" + "Desktop" + "\\" + "testSavedData" + "\\" + fileName + ".xml");
		*/

	}
	public void OpenInExcel()
	{
		//Application.OpenURL ("C:"+"\\"+"Users"+"\\"+"Leonard"+"\\"+"Desktop"+"\\"+"testSavedData"+"\\"+"balanceSheet.xml");
        Debug.Log(Reception.balanceSheets[0].cashAtBank);
        Debug.Log(Reception.balanceSheets[0].year);
        Debug.Log(Reception.balanceSheets[0].day);
        Debug.Log(Reception.balanceSheets[0].month);
        Debug.Log(Reception.balanceSheets[0].accountsPayable);
	}
    public void LoadBalanceSheets() 
    {
        BalanceSheet loadedFile = BalanceSheet.LoadFromFile("savedBalanceSheet.xml");
        Reception.balanceSheets.Add(loadedFile);
        Debug.Log(loadedFile.cashAtBank);
        Debug.Log(loadedFile.year);
        Debug.Log(loadedFile.day);
        Debug.Log(loadedFile.month);
        Debug.Log(loadedFile.accountsPayable);
        Debug.Log(Reception.balanceSheets[0].cashAtBank);
        Debug.Log(Reception.balanceSheets[0].year);
        Debug.Log(Reception.balanceSheets[0].day);
        Debug.Log(Reception.balanceSheets[0].month);
        Debug.Log(Reception.balanceSheets[0].accountsPayable);
        /*
        data = SaveData.Load("C:" + "\\" + "Users" + "\\" + "Leonard" + "\\" + "Desktop" + "\\" + "testSavedData" + "\\" + fileName + ".xml");

        int period = 0;
        bool loopBool = true;
        while (loopBool)
        {
            date endOfPeriod = Calendar.GetEndOfPeriodDate(period);
            string reportEnding = endOfPeriod.month.ToString()+endOfPeriod.day.ToString()+endOfPeriod.year.ToString();
            if (data.HasKey("Report Ending:" + reportEnding))
            {
                Debug.Log("Period : " + period + "Date Sorted: " + data.GetValue<string>("Report Ending:" + reportEnding));
            }
            else
            {
                loopBool = false;
            }
            period++;
        }*/
    }
   /* public void DeserializeBalanceSheets()
    {
        BalanceSheet_Collection balanceSheetCollection = null;
        string path = "C:" + "\\" + "Users" + "\\" + "Leonard" + "\\" + "Desktop" + "\\" + "testSavedData" + "\\" + fileName + ".xml";

        XmlSerializer serializer = new XmlSerializer(typeof(BalanceSheet_Collection));

        StreamReader reader = new StreamReader(path);
        balanceSheetCollection = (BalanceSheet_Collection)serializer.Deserialize(reader);
        reader.Close();
    }*/
}
