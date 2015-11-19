using UnityEngine;
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

    string currentSavedSlot;//selected path from main menu.
    string fullPath;//concatenated path from savedPath and currentsavedSlot.

	GameObject gameController;

    void Start() 
    {
		gameController = GameObject.FindGameObjectWithTag("GameController");
        currentSavedSlot = GetSavedSlot();
        SetPath();
    }

    string GetSavedSlot() //restrieve the saved slot from Saved_Slot singleton.
    {  
       return Saved_Slot.singleton.GetSlot();  
    }
    void SetPath() //contatenates the path.
    {
        fullPath = savedPath+currentSavedSlot;
    }

    public void SaveGame() //Calls all save functions.
    {
        BalanceSheet_Save();
        MonthlyReport_Save();
        Feedback_Save();
        Restaurant_Save();
    }

    public void LoadGame() //Load all available data into reception.
    {
        BalanceSheet_Load();
        MonthlyReport_Load();
        Feedback_Load();
        Restaurant_Load();
    }

    #region Save/Load Methods
    //Balance Sheet----------------------------------------------------------------------//
    void BalanceSheet_Save() 
    {
        BalanceSheetList newList = new BalanceSheetList();
        newList.Listname = "balanceSheet";

        if (Reception.balanceSheets.Count != 0)
        {
            for (int i = 0; i < Reception.balanceSheets.Count; i++)
            {
                newList.Add(Reception.balanceSheets[i]);
            }
        }

        System.Type[] sheet = { typeof(BalanceSheet) };
        XmlSerializer serializer = new XmlSerializer(typeof(BalanceSheetList), sheet);
        FileStream fs = new FileStream(fullPath + balanceSheetPath, FileMode.Create);
        serializer.Serialize(fs, newList);
        fs.Close();
        newList = null;
    }

    void BalanceSheet_Load() 
    {
        if (!File.Exists(fullPath + "BalanceSheetArray.xml"))
        {
            Debug.LogError("FILE " + fullPath + balanceSheetPath + " NOT FOUND!");
            return;
        }
        XmlSerializer serializer = new XmlSerializer(typeof(BalanceSheetList));
        // To read the file, create a FileStream.
        FileStream fs = new FileStream(fullPath + balanceSheetPath, FileMode.Open);
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
    //Monthly Report------------------------------------------------------------------------//
    void MonthlyReport_Save() 
    {
        MonthlyReportList newList = new MonthlyReportList();
        newList.Listname = "MonthlyReport";

        if (Reception.monthlyReports.Count != 0)
        {
            for (int i = 0; i < Reception.monthlyReports.Count; i++)
            {
                newList.Add(Reception.monthlyReports[i]);
            }
        }

        System.Type[] sheet = { typeof(MonthlyReport) };
        XmlSerializer serializer = new XmlSerializer(typeof(MonthlyReportList), sheet);
        FileStream fs = new FileStream(fullPath + monthlyReportPath, FileMode.Create);
        serializer.Serialize(fs, newList);
        fs.Close();
        newList = null;
    }

    void MonthlyReport_Load() 
    {
        if (!File.Exists(fullPath + monthlyReportPath))
        {
            Debug.LogError("FILE " + fullPath + monthlyReportPath + " NOT FOUND!");
            return;
        }
        XmlSerializer serializer = new XmlSerializer(typeof(MonthlyReportList));
        // To read the file, create a FileStream.
        FileStream fs = new FileStream(fullPath + monthlyReportPath, FileMode.Open);
        // Call the Deserialize method and cast to the object type.
        MonthlyReportList loadedlist = (MonthlyReportList)serializer.Deserialize(fs);

        if (Reception.monthlyReports.Count != 0)//Clear the list in reception if it isnt empty.
        {
            Reception.monthlyReports.Clear();
        }

        for (int i = 0; i < loadedlist.monthlyReportList.Count; i++)
        {
            Reception.monthlyReports.Add(loadedlist.monthlyReportList[i]);
        }
    }
    //Feedback-------------------------------------------------------------------------------//
    void Feedback_Save() 
    {
		FeedbackList newList = new FeedbackList();
		newList.Listname = "Feedbacks";

		FeedbackController fbC = gameController.transform.FindChild("FeedBackController").GetComponent<FeedbackController>();

		if (fbC.archivedListOfFeedbacks.Count != 0)
		{
			for (int i = 0; i < fbC.archivedListOfFeedbacks.Count; i++)
			{
				newList.Add(fbC.archivedListOfFeedbacks[i]);
			}
		}
		
		System.Type[] sheet = { typeof(FeedBack) };
		XmlSerializer serializer = new XmlSerializer(typeof(FeedbackList), sheet);
		FileStream fs = new FileStream(fullPath + feedbackPath, FileMode.Create);
		serializer.Serialize(fs, newList);
		fs.Close();
		newList = null;
    }

    void Feedback_Load() 
    {
		if (!File.Exists(fullPath + feedbackPath))
		{
			Debug.LogError("FILE " + fullPath + feedbackPath + " NOT FOUND!");
			return;
		}

		FeedbackController fbC = gameController.transform.FindChild("FeedBackController").GetComponent<FeedbackController>();

		XmlSerializer serializer = new XmlSerializer(typeof(FeedbackList));
		// To read the file, create a FileStream.
		FileStream fs = new FileStream(fullPath + feedbackPath, FileMode.Open);
		// Call the Deserialize method and cast to the object type.
		FeedbackList loadedlist = (FeedbackList)serializer.Deserialize(fs);
		
		if (fbC.archivedListOfFeedbacks.Count != 0)//Clear the balancesheet list in reception if it isnt empty.
		{
			fbC.archivedListOfFeedbacks.Clear();
		}
		
		for (int i = 0; i < loadedlist.feedbackList.Count; i++)
		{
			fbC.archivedListOfFeedbacks.Add(loadedlist.feedbackList[i]);
		}
    }
    //Restaurant report----------------------------------------------------------------------//
    void Restaurant_Save() 
    {
        RestaurantBookList newList = new RestaurantBookList();
        newList.Listname = "RestaurantBook";

        if (Reception.restaurantBooks.Count != 0)
        {
            for (int i = 0; i < Reception.restaurantBooks.Count; i++)
            {
                newList.Add(Reception.restaurantBooks[i]);
            }
        }

        System.Type[] sheet = { typeof(RestaurantBook) };
        XmlSerializer serializer = new XmlSerializer(typeof(RestaurantBookList), sheet);
        FileStream fs = new FileStream(fullPath + restaurantPath, FileMode.Create);
        serializer.Serialize(fs, newList);
        fs.Close();
        newList = null;
    }

    void Restaurant_Load() 
    {
        if (!File.Exists(fullPath + restaurantPath))
        {
            Debug.LogError("FILE " + fullPath + restaurantPath + " NOT FOUND!");
            return;
        }
        XmlSerializer serializer = new XmlSerializer(typeof(RestaurantBookList));
        // To read the file, create a FileStream.
        FileStream fs = new FileStream(fullPath + restaurantPath, FileMode.Open);
        // Call the Deserialize method and cast to the object type.
        RestaurantBookList loadedlist = (RestaurantBookList)serializer.Deserialize(fs);

        if (Reception.restaurantBooks.Count != 0)//Clear the balancesheet list in reception if it isnt empty.
        {
            Reception.restaurantBooks.Clear();
        }

        for (int i = 0; i < loadedlist.restaurantBooklist.Count; i++)
        {
            Reception.restaurantBooks.Add(loadedlist.restaurantBooklist[i]);
        }
    }


    #endregion

}
