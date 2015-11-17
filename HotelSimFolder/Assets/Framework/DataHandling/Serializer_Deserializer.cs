using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class Serializer_Deserializer : MonoBehaviour {

    [SerializeField][Tooltip("DO NOT EDIT UNLESS INSTRUCTED.")]
    string savedPath = "Assets/Save/";
    [SerializeField][Tooltip("DO NOT EDIT UNLESS INSTRUCTED.")]
    string balanceSheetPath = "BalanceSheetArray.xml";
    [SerializeField][Tooltip("DO NOT EDIT UNLESS INSTRUCTED.")]
    string monthlyReportPath = "MonthlyReportArray.xml";
    [SerializeField][Tooltip("DO NOT EDIT UNLESS INSTRUCTED.")]
    string feedbackPath = "FeedbackArray.xml";
    [SerializeField][Tooltip("DO NOT EDIT UNLESS INSTRUCTED.")]
    string restaurantPath = "RestaurantArray.xml";

    public void SaveGame() //Calls all save functions.
    {
        BalanceSheet_Save();
        MonthlyReport_Save();
        Feedback_Save();
        Restaurant_Save();
    }

    public void LoadGame() //Load all available data.
    {
        BalanceSheet_Load();
        MonthlyReport_Load();
        Feedback_Load();
        Restaurant_Load();
    }
    //Balance Sheet
    void BalanceSheet_Save() 
    {
        BalanceSheetList newList = new BalanceSheetList();
        newList.Listname = "balanceSheet";

        if (Reception.balanceSheets.Count != 0)
        {
            for (int k = 0; k < Reception.balanceSheets.Count; k++)
            {
                newList.AddBalanceSheet(Reception.balanceSheets[k]);
            }
        }

        System.Type[] sheet = { typeof(BalanceSheet) };
        XmlSerializer serializer = new XmlSerializer(typeof(BalanceSheetList), sheet);
        FileStream fs = new FileStream("BalanceSheetArray.xml", FileMode.Create);
        serializer.Serialize(fs, newList);
        fs.Close();
        newList = null;
    }

    void BalanceSheet_Load() 
    {
        XmlSerializer serializer = new XmlSerializer(typeof(BalanceSheetList));
        // To read the file, create a FileStream.
        FileStream fs = new FileStream("BalanceSheetArray.xml", FileMode.Open);
        // Call the Deserialize method and cast to the object type.
        BalanceSheetList loadedlist = (BalanceSheetList)serializer.Deserialize(fs);

        if (Reception.balanceSheets.Count != 0)//Clear the balancesheet list in reception if it isnt empty.
        {
            Reception.balanceSheets.Clear();
        }

        for (int i = 0; i < loadedlist.balanceSheetList.Count; i++)
        {
            Reception.balanceSheets.Add(loadedlist.balanceSheetList[i]);
        }
    }
    //Monthly Report
    void MonthlyReport_Save() 
    {
    
    }

    void MonthlyReport_Load() 
    {
    
    }
    //Feedback
    void Feedback_Save() 
    {
    
    }

    void Feedback_Load() 
    {
    
    }
    //Restaurant report
    void Restaurant_Save() 
    {
    
    }

    void Restaurant_Load() 
    {
    
    }




}
