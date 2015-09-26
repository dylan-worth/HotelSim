using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Holds all the calls for menu options in the toolbar
public class ToolbarOptions : MonoBehaviour 
{


	//Variables (Setup)

	static Text cashAtBank;	//Holds the text object displaying current credits
	static Text monthlyIncome;
	static Text monthlyCosts;
	static Text monthlyProfits;
	static Text staffText;
	static Text globalDate;
	//string OptionSelected = "Cleaning";
	int optimizer = 0;
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

	void Update()
	{
		if(optimizer >= 10){
		UpdateCredits();
			optimizer = 0;
		}
		optimizer++;

	}

	//Tells all systems to step their week counter
	public void StepWeeks(int numberOfWeeks)
	{
		if (!Reception.isSimulating) {
			//Iterates through 
			Reception.RunSimulation (numberOfWeeks);
		}
	}

	void UpdateCredits()
	{
		cashAtBank.text = "$" + Mathf.RoundToInt(MasterReference.cashAtBank);
		monthlyIncome.text = "+$" + Mathf.RoundToInt(MasterReference.accountsReceivable);
		monthlyCosts.text = "-$" + Mathf.RoundToInt(MasterReference.accountsPayable);
		monthlyProfits.text = "$" + Mathf.RoundToInt(MasterReference.accountsReceivable - MasterReference.accountsPayable);
		date currentDate = Calendar.getDate ();
		globalDate.text = currentDate.month +" "+ currentDate.dayOfTheMonth +" "+ currentDate.year + " "  + currentDate.day;
	}


}
