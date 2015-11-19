using UnityEngine;
using UnityEngine.UI;

public class BankingReport : MonoBehaviour {
	
	//reference to the loan tabs to disable them once a loan is taken.
	public GameObject tabSmallLoan;
	public GameObject tabMediumLoan;
	public GameObject tabLargeLoan;
	//references to loan display text
	public GameObject activeSmallLoan;
	public GameObject activeMedLoan;
	public GameObject activeLargeLoan;
	//references to loan activation buttons.
	public GameObject btnSmallLoan;
	public GameObject btnMedLoan;
	public GameObject btnLargeLoan;

	//-----------------------------------New Set of Variables----------------------------------//
	[Range(0f,1f)][Tooltip("In percentage, 1 = 100% interest over a year. Rate for shortest Borrowed time. Rate will be lowered by longer loan duration.")]
	public float rateSmallLoan;
	[Range(0f,1f)][Tooltip("In percentage, 1 = 100% interest over a year. Rate for shortest Borrowed time. Rate will be lowered by longer loan duration.")]
	public float rateMedLoan;
	[Range(0f,1f)][Tooltip("In percentage, 1 = 100% interest over a year. Rate for shortest Borrowed time. Rate will be lowered by longer loan duration.")]
	public float rateLargeLoan;
	[SerializeField][Tooltip("Number of months we multiply the loan duration slider by.")]
	int numberOfMonths;
	[SerializeField][Tooltip("Base amount per slider incremment of loan amount.")]
	int smallLoanBaseAmount;
	[SerializeField][Tooltip("Base amount per slider incremment of loan amount.")]
	int mediumLoanBaseAmount;
	[SerializeField][Tooltip("Base amount per slider incremment of loan amount.")]
	int largeLoanBaseAmount;
	//monthly cost per loan.
	float monthlyRepaymentSmallLoan = 0f;
	float monthlyRepaymentMedLoan = 0f;
	float monthlyRepaymentLargeLoan = 0f;
	//amount borrowed before loan is taken small-med-large.
	float borrowed1 = 0f;
	float borrowed2 = 0f;
	float borrowed3 = 0f;
	//amount borrowed
	float smallLoanAmount = 0f;
	float medLoanAmount = 0f;
	float largeLoanAmount = 0f;
	//cost of borrowing small-med-large.
	float costOfSmall = 0f;
	float costOfMed = 0f;
	float costOfLarge = 0f;
	//duration of loans small-med-large.
	int durationSmallLoan = 0;
	int durationMedLoan = 0;
	int durationLargeLoan = 0;
	//remaining payment periods
	int smallLoanRemaining = 0;
	int medLoanremaining = 0;
	int largeLoanRemaining = 0;
	//Totals
	//float totalAmountBorrowed = 0f; 
	//float totalMonthlyRepayment = 0f; 

	//references to the tab inputs.
	public InputField allAmountBorrowed; 
	public InputField allAmountPayBack; 
	public InputField loan1InputField;
	public InputField loan1Time; 
	public InputField loan1Interest; 
	public InputField loan1Payback; 
	public InputField loan2InputField;
	public InputField loan2Time; 
	public InputField loan2Interest; 
	public InputField loan2Payback; 
	public InputField loan3InputField;
	public InputField loan3Time; 
	public InputField loan3Interest; 
	public InputField loan3Payback; 

	//getters for accounts payables
	public float GetLoanRepayments()
	{
		return (monthlyRepaymentSmallLoan+monthlyRepaymentMedLoan+monthlyRepaymentLargeLoan);
	}
	//tick the duration down on all loans
	public void EndMonth()
	{
		if(smallLoanAmount !=0)
		{
		smallLoanRemaining--;
		}
		if(medLoanAmount !=0)
		{
		medLoanremaining--;
		}
		if(largeLoanAmount !=0)
		{
		largeLoanRemaining--;
		}
		if (smallLoanRemaining == 0) 
		{
			borrowed1 = 0f;
			costOfSmall = 0f;
			smallLoanAmount = 0f;
			monthlyRepaymentSmallLoan = 0f;
			activeSmallLoan.SetActive(false);
			tabSmallLoan.SetActive(true);
			btnSmallLoan.transform.localScale = new Vector3(0f,0f,0f);
			tabSmallLoan.transform.FindChild("MoneyBorrowed1").GetComponent<Slider>().value = 1;
			loan1InputField.text = "";
			tabSmallLoan.transform.FindChild("TimetoPay1").GetComponent<Slider>().value = 6;
			loan1Time.text = "";
			loan1Payback.text = "";
			allAmountBorrowed.text = (smallLoanAmount+medLoanAmount+largeLoanAmount).ToString();
			allAmountPayBack.text = Mathf.RoundToInt(monthlyRepaymentSmallLoan+monthlyRepaymentMedLoan+monthlyRepaymentLargeLoan).ToString();
		}
		if (medLoanremaining == 0) 
		{
			borrowed2 = 0f;
			costOfMed = 0f;
			medLoanAmount = 0f;
			monthlyRepaymentMedLoan = 0f;
			tabMediumLoan.SetActive(true);
			activeMedLoan.SetActive(false);
			tabMediumLoan.SetActive(true);
			btnMedLoan.transform.localScale = new Vector3(0f,0f,0f);
			tabMediumLoan.transform.FindChild("MoneyBorrowed2").GetComponent<Slider>().value = 1;
			loan2InputField.text = "";
			tabMediumLoan.transform.FindChild("TimetoPay2").GetComponent<Slider>().value = 6;
			loan2Time.text = "";
			loan2Payback.text = "";
			allAmountBorrowed.text = (smallLoanAmount+medLoanAmount+largeLoanAmount).ToString();
			allAmountPayBack.text = Mathf.RoundToInt(monthlyRepaymentSmallLoan+monthlyRepaymentMedLoan+monthlyRepaymentLargeLoan).ToString();
		}
		if (largeLoanRemaining == 0) 
		{
			borrowed3 = 0f;
			costOfLarge = 0f;
			largeLoanAmount = 0f;
			monthlyRepaymentLargeLoan = 0f;
			tabLargeLoan.SetActive(true);
			activeLargeLoan.SetActive(false);
			tabLargeLoan.SetActive(true);
			btnLargeLoan.transform.localScale = new Vector3(0f,0f,0f);
			tabLargeLoan.transform.FindChild("MoneyBorrowed3").GetComponent<Slider>().value = 1;
			loan3InputField.text = "";
			tabLargeLoan.transform.FindChild("TimetoPay3").GetComponent<Slider>().value = 6;
			loan3Time.text = "";
			loan3Payback.text = "";
			allAmountBorrowed.text = (smallLoanAmount+medLoanAmount+largeLoanAmount).ToString();
			allAmountPayBack.text = Mathf.RoundToInt(monthlyRepaymentSmallLoan+monthlyRepaymentMedLoan+monthlyRepaymentLargeLoan).ToString();
		}
	}
	// Set and display the interest rates for each type of loan
	public void OnEnable()
	{
		loan1Interest.text = rateSmallLoan.ToString() + "%"; 
		loan2Interest.text = rateMedLoan.ToString () + "%";
		loan3Interest.text = rateLargeLoan.ToString () + "%";
	}
	public void SmallLoanEdit(Slider slider)
	{
		if(slider.value < 6)//player is editing the amount
		{
			borrowed1 = slider.value * smallLoanBaseAmount;
			loan1InputField.text = borrowed1.ToString(); 
		}
		else//player is editing the duration.
		{
			durationSmallLoan = (int)((slider.value-5) * numberOfMonths); // add slider value 
			loan1Time.text = durationSmallLoan.ToString(); 
		}
		if(loan1InputField.text != "" && loan1Time.text != "")
		{
			btnSmallLoan.transform.localScale = new Vector3(1f,1f,1f);

			float interestPerMonth;
			interestPerMonth = rateSmallLoan / 12f;
			
			monthlyRepaymentSmallLoan = (borrowed1 * interestPerMonth) / (1 - Mathf.Pow ((1 + interestPerMonth), -durationSmallLoan));
			loan1Payback.text = Mathf.RoundToInt(monthlyRepaymentSmallLoan).ToString (); 
			costOfSmall = (monthlyRepaymentSmallLoan*durationSmallLoan)-borrowed1;
		}
	}
	public void MedLoanEdit(Slider slider)
	{
		if(slider.value < 6)//player is editing the amount
		{
			borrowed2 = slider.value * mediumLoanBaseAmount;
			loan2InputField.text = borrowed2.ToString(); 
		}
		else//player is editing the duration.
		{
			durationMedLoan = (int)((slider.value-5) * numberOfMonths); // add slider value 
			loan2Time.text = durationMedLoan.ToString(); 
		}
		if(loan2InputField.text != "" && loan2Time.text != "")
		{
			btnMedLoan.transform.localScale = new Vector3(1f,1f,1f);

			float interestPerMonth;
			interestPerMonth = rateMedLoan / 12f;
			
			monthlyRepaymentMedLoan = (borrowed2 * interestPerMonth) / (1 - Mathf.Pow ((1 + interestPerMonth), -durationMedLoan));
			
			loan2Payback.text = Mathf.RoundToInt(monthlyRepaymentMedLoan).ToString (); 
			costOfMed = (monthlyRepaymentMedLoan*durationMedLoan)-borrowed2;
		}
	}
	public void LargeLoanEdit(Slider slider)
	{
		if(slider.value < 6)//player is editing the amount
		{
			borrowed3 = slider.value * largeLoanBaseAmount;
			loan3InputField.text = borrowed3.ToString(); 
		}
		else//player is editing the duration.
		{
			durationLargeLoan = (int)((slider.value-5) * numberOfMonths); // add slider value 
			loan3Time.text = durationLargeLoan.ToString(); 
		}
		if(loan3InputField.text != "" && loan3Time.text != "")
		{
			btnLargeLoan.transform.localScale = new Vector3(1f,1f,1f);

			float interestPerMonth;
			interestPerMonth = rateLargeLoan / 12f;
			
			monthlyRepaymentLargeLoan = (borrowed3 * interestPerMonth) / (1 - Mathf.Pow ((1 + interestPerMonth), -durationLargeLoan));
			
			
			loan3Payback.text = Mathf.RoundToInt(monthlyRepaymentLargeLoan).ToString (); 
			costOfLarge = (monthlyRepaymentLargeLoan*durationLargeLoan)-borrowed3;
		}
	}
	public void ActivateLoan(int type)
	{
		if(type == 1)
		{
			smallLoanAmount = borrowed1;
			smallLoanRemaining = durationSmallLoan;
			tabSmallLoan.SetActive(false);
			activeSmallLoan.GetComponent<Text>().text = "Current Small Loan: $" + borrowed1 + " at " 
				+ rateSmallLoan + "%. Repayment Period: "+ durationSmallLoan +" months. Last payment period : " 
					+ Calendar.getDate().month + " " +(Calendar.getDate().year+(durationSmallLoan/12)) + ". Borrowing cost: $" + Mathf.RoundToInt(costOfSmall);
			activeSmallLoan.SetActive(true);
			allAmountBorrowed.text = (smallLoanAmount+medLoanAmount+largeLoanAmount).ToString();
			allAmountPayBack.text = Mathf.RoundToInt(monthlyRepaymentSmallLoan+monthlyRepaymentMedLoan+monthlyRepaymentLargeLoan).ToString();
			MasterReference.cashAtBank += smallLoanAmount;
		}
		if(type == 2)
		{
			medLoanAmount = borrowed2;
			medLoanremaining = durationMedLoan;
			tabMediumLoan.SetActive(false);
			activeMedLoan.GetComponent<Text>().text = "Current Medium Loan: $" + borrowed2 + " at " 
				+ rateMedLoan + "%. Repayment Period: "+ durationMedLoan +" months. Last payment period : " 
					+ Calendar.getDate().month + " " +(Calendar.getDate().year+(durationMedLoan/12)) + ". Borrowing cost: $" + Mathf.RoundToInt(costOfMed);
			activeMedLoan.SetActive(true);
			allAmountBorrowed.text = (smallLoanAmount+medLoanAmount+largeLoanAmount).ToString();
			allAmountPayBack.text = Mathf.RoundToInt(monthlyRepaymentSmallLoan+monthlyRepaymentMedLoan+monthlyRepaymentLargeLoan).ToString();
			MasterReference.cashAtBank += medLoanAmount;
		}
		if(type == 3)
		{
			largeLoanAmount = borrowed3;
			largeLoanRemaining = durationLargeLoan;
			tabLargeLoan.SetActive(false);
			activeLargeLoan.GetComponent<Text>().text = "Current Large Loan: $" + borrowed3 + " at " 
				+ rateLargeLoan + "%. Repayment Period: "+ durationLargeLoan +" months. Last payment period : " 
					+ Calendar.getDate().month + " " +(Calendar.getDate().year+(durationLargeLoan/12)) + ". Borrowing cost: $" + Mathf.RoundToInt(costOfLarge);
			activeLargeLoan.SetActive(true);
			allAmountBorrowed.text = (smallLoanAmount+medLoanAmount+largeLoanAmount).ToString();
			allAmountPayBack.text = Mathf.RoundToInt(monthlyRepaymentSmallLoan+monthlyRepaymentMedLoan+monthlyRepaymentLargeLoan).ToString();
			MasterReference.cashAtBank += largeLoanAmount;
		}
	}



}
