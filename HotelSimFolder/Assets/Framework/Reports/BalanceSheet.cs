using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[XmlRoot("BalanceSheets")]
[XmlInclude(typeof(BalanceSheet))] // include type class BalanceSheet
public class BalanceSheetList
{
    [XmlArray("BalanceSheetArray")]
    [XmlArrayItem("BalanceSheetObject")]
    public List<BalanceSheet> balanceSheetsTest = new List<BalanceSheet>();

    [XmlElement("Listname")]
    public string Listname { get; set; }

    // Constructor
    public BalanceSheetList() { }

    public BalanceSheetList(string name)
    {
        this.Listname = name;
    }

    public void AddBalanceSheet(BalanceSheet balanceSheet)
    {
        balanceSheetsTest.Add(balanceSheet);
    }
}
[System.Serializable]
[XmlType("BalanceSheet")]
public class BalanceSheet{


    

	//saved for every months. Last day of Period
    [XmlIgnoreAttribute]//To remove the data.
    public date dateOfReport { get; set; }
    public int dayOfTheMonth { get; set; }
    public months month { get; set; }
    public int year { get; set; }
    public WeekDays day { get; set; }
    public int numberOfWeeks { get; set; } 
	//currentAssets

    public float cashAtBank { get; set; }
    public float accountsReceivable { get; set; }
    public float inventories { get; set; }

    public float totalCurrentAssets { get; set; } 

	//Property & Equipment
    public float propretyAndEquipment { get; set; }

    public float totalAssets { get; set; } 

	//current Liabilities
    public float accountsPayable { get; set; }
    public float carbonOffsetReceipts { get; set; }
    public float incomeTaxPayable { get; set; }
    public float dividendOwed { get; set; }
    public float currentMaturityofLongtermDebt { get; set; }

    public float totalCurrentLiabilities { get; set; } 	
		
	//Long-term Liabilities	  
    public float longTermDebt { get; set; } 		
			
	//	Owners' Equity	 		 
    public float shareCapital { get; set; }
    public float retainedEarnings { get; set; }

    public float ownersEquity { get; set; }

    public float totalLiabilitiesAndOwnersEquity { get; set; }
    //save function for balance sheet
    public void Save(string filename) 
    {
        using (var stream = new FileStream(filename, FileMode.Create)) 
        {
            var XML = new XmlSerializer(typeof(BalanceSheet));
            XML.Serialize(stream, this);
        }
    }
    public void AddToSavedFile(string filename)
    {
        using (var stream = new FileStream(filename, FileMode.Append))
        {
            var XML = new XmlSerializer(typeof(BalanceSheet));                                 
            XML.Serialize(stream, this);

        }
    }
    //load function for balance sheet
    public static BalanceSheet LoadFromFile(string filename) 
    {
        using (var stream = new FileStream(filename, FileMode.Open))
        {
            var XML = new XmlSerializer(typeof(BalanceSheet));
            return (BalanceSheet)XML.Deserialize(stream);
        }
    }

    public BalanceSheet deepCopy()
	{
		BalanceSheet newSheet = new BalanceSheet();

		newSheet.dateOfReport = this.dateOfReport;
		newSheet.cashAtBank = this.cashAtBank;
		newSheet.accountsReceivable = this.accountsReceivable;
		newSheet.inventories = this.inventories;
		newSheet.totalCurrentAssets = this.totalCurrentAssets;
		newSheet.propretyAndEquipment = this.propretyAndEquipment;
		newSheet.totalAssets = this.totalAssets;
		newSheet.accountsPayable = this.accountsPayable;
		newSheet.carbonOffsetReceipts = this.carbonOffsetReceipts;
		newSheet.incomeTaxPayable = this.incomeTaxPayable;
		newSheet.dividendOwed = this.dividendOwed;
		newSheet.currentMaturityofLongtermDebt = this.currentMaturityofLongtermDebt;
		newSheet.totalCurrentLiabilities = this.totalCurrentLiabilities;
		newSheet.longTermDebt = this.longTermDebt;
		newSheet.shareCapital = this.shareCapital;
		newSheet.retainedEarnings = this.retainedEarnings;
		newSheet.ownersEquity = this.ownersEquity;
		newSheet.totalLiabilitiesAndOwnersEquity = this.totalLiabilitiesAndOwnersEquity;

		return newSheet;
	}
	public BalanceSheet(){}

	//contructor with dateofReport, cashatbank, accountreceivable, inventories, propertyandequipment, accountpayable, carbonoffsetreceipt, incometaxPayable,
	//dividenOwed, currentmaturityoflongtermdebt, longtermdept, sharecapital, retainedearnings.
	public BalanceSheet(date dOR, float cAB, float aR, float inv, float tCA,float pAE, float tA, float aP, float cOR, 
	                    float iTP, float dO, float cMOLTD, float tCMOLT, float lTD, float sC, float rE, float oE, float tLAOE)
	{
		this.dateOfReport = dOR;
		this.cashAtBank = cAB;
		this.accountsReceivable = aR;
		this.inventories = inv;
		this.totalCurrentAssets = tCA;
		this.propretyAndEquipment = pAE;
		this.totalAssets = tA;
		this.accountsPayable =	aP;		
		this.carbonOffsetReceipts = cOR;		
		this.incomeTaxPayable = iTP;		
		this.dividendOwed = dO;		
		this.currentMaturityofLongtermDebt = cMOLTD;
		this.totalCurrentLiabilities = tCMOLT;
		this.longTermDebt = lTD;		 
		this.shareCapital = sC;		
		this.retainedEarnings = rE;	
		this.ownersEquity = oE;
		this.totalLiabilitiesAndOwnersEquity = tLAOE;
	}

}