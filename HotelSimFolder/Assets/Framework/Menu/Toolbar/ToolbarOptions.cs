using UnityEngine;
using UnityEngine.UI;

//Holds all the calls for menu options in the toolbar
public class ToolbarOptions : MonoBehaviour 
{

	static Text cashAtBank;	//Holds the text object displaying current credits
	static Text monthlyIncome;
	static Text monthlyCosts;
	static Text monthlyProfits;
	static Text staffText;
	static Text globalDate;
	//string OptionSelected = "Cleaning";
	
	void Awake ()
	{
		GameObject UImenu = GameObject.FindGameObjectWithTag("UI");
		GameObject TopBar =  UImenu.transform.FindChild("OverWorld").gameObject;
		cashAtBank = TopBar.transform.FindChild("txtCreditBalance").GetComponent<Text>();
		monthlyIncome = TopBar.transform.FindChild("txtProfit").GetComponent<Text>();
		monthlyCosts = TopBar.transform.FindChild("txtLoss").GetComponent<Text>();
		monthlyProfits = TopBar.transform.FindChild("txtTotal").GetComponent<Text>();
		globalDate = TopBar.transform.FindChild("txtDate").GetComponent<Text>();
	}

	//Tells all systems to step their week counter
	public void StepWeeks(int numberOfWeeks)
	{
		if (!Reception.isSimulating) {
			//Iterates through 
			Reception.RunSimulation (numberOfWeeks);
		}
	}

	public void UpdateCredits()
	{
		cashAtBank.text = "$" + Mathf.RoundToInt(MasterReference.cashAtBank);
		monthlyIncome.text = "+$" + Mathf.RoundToInt(MasterReference.accountsReceivable);
		monthlyCosts.text = "-$" + Mathf.RoundToInt(MasterReference.accountsPayable);
		monthlyProfits.text = "$" + Mathf.RoundToInt(MasterReference.accountsReceivable - MasterReference.accountsPayable);
		Date currentDate = Calendar.GetDate ();
		globalDate.text = currentDate.month +" "+ currentDate.dayOfTheMonth +" "+ currentDate.year + " "  + currentDate.day;
	}


}
