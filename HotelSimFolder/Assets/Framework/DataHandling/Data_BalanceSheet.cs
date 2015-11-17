using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


public class Data_BalanceSheet : MonoBehaviour {

	public string fileName = "balanceSheet";


	public void SaveBalanceSheet ()
	{
        //saves the list of balanceSheets to xml file.
        BalanceSheetList newList = new BalanceSheetList();
        newList.Listname = fileName;
        if (Reception.balanceSheets.Count != 0)
        {


            for (int k = 0; k < Reception.balanceSheets.Count; k++ )
            {
                    newList.AddBalanceSheet(Reception.balanceSheets[k]);
            }
               
         
        }
        System.Type[] sheet = { typeof(BalanceSheet)};
        XmlSerializer serializer = new XmlSerializer(typeof(BalanceSheetList), sheet);
        FileStream fs = new FileStream("BalanceSheetArray.xml", FileMode.Create);
        serializer.Serialize(fs, newList);
        fs.Close();
        newList = null;

      


	}
    public void Deserialize() 
    {


        XmlSerializer serializer = 
        new XmlSerializer(typeof(BalanceSheetList));
        // To read the file, create a FileStream.
        FileStream fs = 
        new FileStream("BalanceSheetArray.xml", FileMode.Open);
        // Call the Deserialize method and cast to the object type.
        BalanceSheetList loadedlist = (BalanceSheetList)serializer.Deserialize(fs);


         Debug.Log(loadedlist.balanceSheetList[0].cashAtBank);
         Debug.Log(loadedlist.balanceSheetList[1].cashAtBank);
         Debug.Log(loadedlist.balanceSheetList[2].cashAtBank);
         Debug.Log(loadedlist.balanceSheetList[3].cashAtBank);
    }

	public void OpenInExcel()
	{
        //this.Application.Workbooks.OpenXML(@"C:\Test.xml", missing, missing);
		//Application.OpenURL ("C:"+"\\"+"Users"+"\\"+"Leonard"+"\\"+"Desktop"+"\\"+"testSavedData"+"\\"+"balanceSheet.xml");
        Debug.Log(Reception.balanceSheets[0].cashAtBank);
        Debug.Log(Reception.balanceSheets[0].year);
        Debug.Log(Reception.balanceSheets[0].dayOfTheMonth);
        Debug.Log(Reception.balanceSheets[0].month);
        Debug.Log(Reception.balanceSheets[0].accountsPayable);
	}
    public void LoadBalanceSheets() 
    {
        BalanceSheet loadedFile = BalanceSheet.LoadFromFile("savedBalanceSheet.xml");
        Reception.balanceSheets.Add(loadedFile);
        Debug.Log(loadedFile.cashAtBank);
        Debug.Log(loadedFile.year);
        Debug.Log(loadedFile.dayOfTheMonth);
        Debug.Log(loadedFile.month);
        Debug.Log(loadedFile.accountsPayable);
        Debug.Log(Reception.balanceSheets[0].cashAtBank);
        Debug.Log(Reception.balanceSheets[0].year);
        Debug.Log(Reception.balanceSheets[0].dayOfTheMonth);
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
