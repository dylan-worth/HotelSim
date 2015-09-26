using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;


public class BalanceSheet{

	//saved for every months. Last day of Period
	public date dateOfReport;
	//currentAssets
	public float cashAtBank;
	public float accountsReceivable;
	public float inventories;

	public float totalCurrentAssets;

	//Property & Equipment
	public float propretyAndEquipment;

	public float totalAssets;

	//current Liabilities
	public float accountsPayable;		
	public float carbonOffsetReceipts;		
	public float incomeTaxPayable;		
	public float dividendOwed;		
	public float currentMaturityofLongtermDebt;	

	public float totalCurrentLiabilities;	
		
	//Long-term Liabilities	  
	public float longTermDebt;		
			
	//	Owners' Equity	 		 
	public float  shareCapital;		
	public float retainedEarnings;		
 	 		 
	public float ownersEquity;		
			
	public float totalLiabilitiesAndOwnersEquity;


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