using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StaffingLog{

	public int departmentHeads = 0;
	public float departmentHeadsP;
	public int hotelService = 0;
	public float hotelServiceP;
	public int foodAndBev = 0;
	public float foodAndBevP ;
	public int frontDesk = 0;
	public float frontDeskP;
	public int conferenceAndBanquet = 0;
	public float conferenceAndBanquetP;
	public int others = 0;
	public float othersP;

	public int month;

	public StaffingLog(){}
	public StaffingLog(float dh, float hs, float fb, float fd, float c, float o)
	{
		this.departmentHeadsP = dh;
		this.hotelServiceP = hs;
		this.foodAndBevP = fb;
		this.frontDeskP = fd;
		this.conferenceAndBanquetP = c;
		this.othersP = o;
	}

	


	public StaffingLog DeepCopy()
	{
		StaffingLog other = new StaffingLog(); 
		
		other.month = this.month;
		
		
		other.departmentHeads = this.departmentHeads;			
		other.departmentHeadsP = this.departmentHeadsP;				
		other.hotelService= this.hotelService;		
		other.hotelServiceP= this.hotelServiceP;		
		other.foodAndBev= this.foodAndBev;	
		other.foodAndBevP= this.foodAndBevP;	
		other.frontDesk= this.frontDesk;		
		other.frontDeskP= this.frontDeskP;		
		other.conferenceAndBanquet= this.conferenceAndBanquet; 			
		other.conferenceAndBanquetP= this.conferenceAndBanquetP;		
		other.others= this.others;      
		other.othersP= this.othersP;                 

		return other;
	}
}


public class StaffMenu : MonoBehaviour {

	public StaffingLog newStaffingLog = new StaffingLog();

    //color for selected tab.
    [SerializeField]
    [Tooltip("Color of the selected Tab")]
    Color selectedColor;
    [SerializeField]
    [Tooltip("Default color of the tabs")]
    Color defaultColor;
    //staffing starting values.
    [SerializeField]
    [Range(0, 20)]
    [Tooltip("Starting number of Hotel service staff.")]
    int staff_HS_Num;
    [SerializeField]
    [Range(0, 20)]
    [Tooltip("Starting number of Food and Beverage staff.")]
    int staff_FB_Num;
    [SerializeField]
    [Range(0, 20)]
    [Tooltip("Starting number of Front Desk staff.")]
    int staff_FD_Num;
    [SerializeField]
    [Range(0, 20)]
    [Tooltip("Starting number of Conference staff.")]
    int staff_C_Num;
    [SerializeField]
    [Range(0, 20)]
    [Tooltip("Starting number of other staff.")]
    int staff_O_Num;
	[SerializeField][Tooltip("Cost of each Department head per hours.")]
	float payDepartmentHead;
	[SerializeField][Tooltip("Pay levels of hotel services staff. DO NOT CHANGE SIZE!")]
	float[] payBandHotelServices = new float[4];
	[SerializeField][Tooltip("Pay levels of food and beverage staff. DO NOT CHANGE SIZE!")]
	float[] payBandFoodAndBeverages = new float[4];
	[SerializeField][Tooltip("Pay levels of front desk staff. DO NOT CHANGE SIZE!")]
	float[] payBandFrontDesk = new float[4];
	[SerializeField][Tooltip("Pay levels of conference staff. DO NOT CHANGE SIZE!")]
	float[] payBandConference = new float[4];
	[SerializeField][Tooltip("Pay levels of other staff. DO NOT CHANGE SIZE!")]
	float[] payBandOther = new float[4];
	[SerializeField][Tooltip("Effect of training per hour per week. StaffMember has 50 base productivity and get this bonus each week for each hours.")][Range(0f,2f)]
	float trainingBonusToProductivity;
	[SerializeField][Tooltip("Effect of not training a staffMember for one week. Base productivity is 50 and cannot get lower.")][Range(0f,2f)]
	float trainingEffectDecay;

	int[] trainingAllocation = new int[5]{0,0,0,0,0};//array holding weekly hours of training for all department.




    GameObject staffMenuTab;//Staff menu panel.
    GameObject panelHS;//The 5 difference panels of the staff menu page.
    GameObject panelFB;
    GameObject panelFD;
    GameObject panelC;
    GameObject panelO;

    GameObject[] panelArray = new GameObject[5];
    GameObject[] buttonArray = new GameObject[5];

    void Awake() 
    {
        staffMenuTab = GameObject.FindGameObjectWithTag("UI").transform.FindChild("Tabs").transform.FindChild("StaffMenu").gameObject;
        panelHS = staffMenuTab.transform.FindChild("Panel_HotelServices").gameObject;
        panelFB = staffMenuTab.transform.FindChild("Panel_FoodAndBeverages").gameObject;
        panelFD = staffMenuTab.transform.FindChild("Panel_FrontDesk").gameObject;
        panelC = staffMenuTab.transform.FindChild("Panel_Conference").gameObject;
        panelO = staffMenuTab.transform.FindChild("Panel_Other").gameObject;
        panelArray[0] = panelHS;
        panelArray[1] = panelFB;
        panelArray[2] = panelFD;
        panelArray[3] = panelC;
        panelArray[4] = panelO;
        //Reference to selection buttons.
        GameObject buttonHolder = staffMenuTab.transform.FindChild("ButtonHolder").gameObject;
        buttonArray[0] = buttonHolder.transform.FindChild("btn_HotelServices").gameObject;
        buttonArray[1] = buttonHolder.transform.FindChild("btn_FoodAndBeverage").gameObject;
        buttonArray[2] = buttonHolder.transform.FindChild("btn_FrontDesk").gameObject;
        buttonArray[3] = buttonHolder.transform.FindChild("btn_Conference").gameObject;
        buttonArray[4] = buttonHolder.transform.FindChild("btn_Other").gameObject;
        buttonArray[0].GetComponent<Image>().color = selectedColor;//Set the first Tab as selected.
    }
    void Start()
    {
		SetPayBandInMaster();
        SetFirstRunStaffing();
		SetPayScale();

    }
	void SetPayBandInMaster()
	{
		MasterReference.payScales[0]= payDepartmentHead;
		MasterReference.payScales[1]= payBandHotelServices[0];
		MasterReference.payScales[2]= payBandFoodAndBeverages[0];
		MasterReference.payScales[3]= payBandFrontDesk[0];
		MasterReference.payScales[4]= payBandConference[0];
		MasterReference.payScales[5]= payBandOther[0];
		MasterReference.payBandHS = 0;
		MasterReference.payBandFB = 0;
		MasterReference.payBandFD = 0;
		MasterReference.payBandConference = 0;
		MasterReference.payBandOthers = 0;

	}


	void SetPayScale()//Sets everyone's pay one by one from the masterReferences value.
	{
		foreach (StaffMember aMember in Staff.staffDepartmentHead)
		{
			aMember.PayRate = MasterReference.payScales[0];
		}
		foreach (StaffMember aMember in Staff.staffHotelServices)
		{
			aMember.PayRate = MasterReference.payScales[1];
		}
		foreach (StaffMember aMember in Staff.staffFoodAndBeverages)
		{
			aMember.PayRate = MasterReference.payScales[2];
		}
		foreach (StaffMember aMember in Staff.staffFrontDesk)
		{
			aMember.PayRate = MasterReference.payScales[3];
		}
		foreach (StaffMember aMember in Staff.staffConference)
		{
			aMember.PayRate = MasterReference.payScales[4];
		}
		foreach (StaffMember aMember in Staff.staffOthers)
		{
			aMember.PayRate = MasterReference.payScales[5];
		}
	}
    void SetFirstRunStaffing() 
    {
        for (int i = 0; i < staff_HS_Num; i++)
        {
            StaffMember newGuy = new StaffMember(staffType.HotelServices);
            Staff.staffHotelServices.Add(newGuy);
        }
        for (int i = 0; i < staff_FB_Num; i++)
        {
            StaffMember newGuy = new StaffMember(staffType.FoodAndBeverages);
            Staff.staffFoodAndBeverages.Add(newGuy);
        }
        for (int i = 0; i < staff_FD_Num; i++)
        {
            StaffMember newGuy = new StaffMember(staffType.FrontDesk);
            Staff.staffFrontDesk.Add(newGuy);
        }
        for (int i = 0; i < staff_C_Num; i++)
        {
            StaffMember newGuy = new StaffMember(staffType.Conference);
            Staff.staffConference.Add(newGuy);
        }
        for (int i = 0; i < staff_O_Num; i++)
        {
            StaffMember newGuy = new StaffMember(staffType.Others);
            Staff.staffOthers.Add(newGuy);
        }
    }

    public void RefreshTabs()
    {
       
        List<List<StaffMember>> staffList = Staff.GetStaffingList();
        for (int i = 0; i < panelArray.Length; i++)
        {
			float totalProd = 0;
			int count = 0;
			foreach(StaffMember aMember in staffList[i + 1])
			{
				totalProd += aMember.CalculateProd();
				count++;
			}
            panelArray[i].transform.FindChild("Input_NumberOfStaff").GetComponent<InputField>().text = staffList[i + 1].Count.ToString();
			panelArray[i].transform.FindChild("Input_Payscale").GetComponent<InputField>().text = "$"+ MasterReference.payScales[i+1].ToString();
			panelArray[i].transform.FindChild("Input_Training").GetComponent<InputField>().text = trainingAllocation[i].ToString() + " H/Week";
			panelArray[i].transform.FindChild("Input_TrainingCost").GetComponent<InputField>().text = 
				"$"+(trainingAllocation[i]*staffList[i + 1].Count*MasterReference.payScales[i+1]).ToString();
			panelArray[i].transform.FindChild("Input_WeeklyCost").GetComponent<InputField>().text = 
				"$" + (staffList[i + 1].Count*MasterReference.payScales[i+1]*40).ToString();
			if(count > 0)
			{
				panelArray[i].transform.FindChild("Input_Productivity").GetComponent<InputField>().text = (Mathf.Round(totalProd/count)).ToString();
			}
			else
			{
				panelArray[i].transform.FindChild("Input_Productivity").GetComponent<InputField>().text = "NA";
			}
			panelArray[i].transform.FindChild("Input_TotalProductivity").GetComponent<InputField>().text = totalProd.ToString();
        }
    }
	
	public float[] ReturnCostPerDay ()
	{

		float[] returnValue = new float[6]{0,0,0,0,0,0};
		foreach (StaffMember aMember in Staff.staffDepartmentHead)
		{
			returnValue[0] += aMember.PayRate*5.7f;
		}
		foreach (StaffMember aMember in Staff.staffHotelServices)
		{
			returnValue[1] += aMember.PayRate*5.7f;
		}
		foreach (StaffMember aMember in Staff.staffFoodAndBeverages)
		{
			returnValue[2] += aMember.PayRate*5.7f;
		}
		foreach (StaffMember aMember in Staff.staffFrontDesk)
		{
			returnValue[3] += aMember.PayRate*5.7f;
		}
		foreach (StaffMember aMember in Staff.staffConference)
		{
			returnValue[4] += aMember.PayRate*5.7f;
		}
		foreach (StaffMember aMember in Staff.staffOthers)
		{
			returnValue[5] += aMember.PayRate*5.7f;
		}

		return returnValue;
	}
	public float returnActualProduction (float ProductionNeeded)
	{
		float actualProd = 0f;
		foreach (StaffMember aMember in Staff.staffDepartmentHead)
		{
			actualProd += aMember.CalculateProd()*5.7f;
			aMember.HoursWorked = 5.7f;
			aMember.Experience++;
		}
		foreach (StaffMember aMember in Staff.staffHotelServices)
		{
			actualProd += aMember.CalculateProd()*5.7f;
			aMember.HoursWorked = 5.7f;
			aMember.Experience++;
		}
		foreach (StaffMember aMember in Staff.staffFoodAndBeverages)
		{
			actualProd += aMember.CalculateProd()*5.7f;
			aMember.HoursWorked = 5.7f;
			aMember.Experience++;
		}
		foreach (StaffMember aMember in Staff.staffFrontDesk)
		{
			actualProd += aMember.CalculateProd()*5.7f;
			aMember.HoursWorked = 5.7f;
			aMember.Experience++;
		}
		foreach (StaffMember aMember in Staff.staffConference)
		{
			actualProd += aMember.CalculateProd()*5.7f;
			aMember.HoursWorked = 5.7f;
			aMember.Experience++;
		}
		foreach (StaffMember aMember in Staff.staffOthers)
		{
			actualProd += aMember.CalculateProd()*5.7f;
			aMember.HoursWorked = 5.7f;
			aMember.Experience++;
		}

		return actualProd;
	
	}

    public void TabSelection(int selected) 
    {
        for (int i = 0; i < 5; i++)
        {
            if (i == selected)
            {
                buttonArray[i].GetComponent<Image>().color = selectedColor;
            }
            else
            {
                buttonArray[i].GetComponent<Image>().color = defaultColor;
            }
        }
    }
	public void TrainStaff()//Adds one week of training to each staff members and removes one week of non-training. 
	{

		foreach(StaffMember aMember in Staff.staffHotelServices)
		{
			if(trainingAllocation[0] > 0)
			{
				aMember.baseProductivity += trainingAllocation[0]*trainingBonusToProductivity;
			}
			else
			{
				if(aMember.baseProductivity > 51)
					aMember.baseProductivity -= trainingEffectDecay;
			}
		}
		foreach(StaffMember aMember in Staff.staffFoodAndBeverages)
		{
			if(trainingAllocation[1] > 0)
			{
				aMember.baseProductivity += trainingAllocation[1]*trainingBonusToProductivity;
			}
			else
			{
				if(aMember.baseProductivity > 51)
					aMember.baseProductivity -= trainingEffectDecay;
			}
		}
		foreach(StaffMember aMember in Staff.staffFrontDesk)
		{
			if(trainingAllocation[2] > 0)
			{
				aMember.baseProductivity += trainingAllocation[2]*trainingBonusToProductivity;
			}
			else
			{
				if(aMember.baseProductivity > 51)
					aMember.baseProductivity -= trainingEffectDecay;
			}
		}
		foreach(StaffMember aMember in Staff.staffConference)
		{
			if(trainingAllocation[3] > 0)
			{
				aMember.baseProductivity += trainingAllocation[3]*trainingBonusToProductivity;
			}
			else
			{
				if(aMember.baseProductivity > 51)
					aMember.baseProductivity -= trainingEffectDecay;
			}
		}
		foreach(StaffMember aMember in Staff.staffOthers)
		{
			if(trainingAllocation[4] > 0)
			{
				aMember.baseProductivity += trainingAllocation[4]*trainingBonusToProductivity;
			}
			else
			{
				if(aMember.baseProductivity > 51)
					aMember.baseProductivity -= trainingEffectDecay;
			}
		}
	}
	public void IncreasePay(int type)
	{
		switch (type) 
		{
		case 1:
			MasterReference.payBandHS++;
			MasterReference.payScales[1] = payBandHotelServices[MasterReference.payBandHS];
			break;
		case 2:
			MasterReference.payBandFB++;
			MasterReference.payScales[2] = payBandFoodAndBeverages[MasterReference.payBandFB];
			break;
		case 3:
			MasterReference.payBandFD++;
			MasterReference.payScales[3] = payBandFrontDesk[MasterReference.payBandFD];
			break;
		case 4:
			MasterReference.payBandConference++;
			MasterReference.payScales[4] = payBandConference[MasterReference.payBandConference];
			break;
		case 5:
			MasterReference.payBandOthers++;
			MasterReference.payScales[5] = payBandOther[MasterReference.payBandOthers];
			break;
		}
		if(MasterReference.payBandHS >= 3)//disables the plus buttons once we reach the maximum number of pay increase.
		{
			panelArray[0].transform.FindChild("btn_PayIncrease").GetComponent<Button>().interactable = false;
		}
		if(MasterReference.payBandFB >= 3)
		{
			panelArray[1].transform.FindChild("btn_PayIncrease").GetComponent<Button>().interactable = false;
		}
		if(MasterReference.payBandFD >= 3)
		{
			panelArray[2].transform.FindChild("btn_PayIncrease").GetComponent<Button>().interactable = false;
		}
		if(MasterReference.payBandConference >= 3)
		{
			panelArray[3].transform.FindChild("btn_PayIncrease").GetComponent<Button>().interactable = false;
		}
		if(MasterReference.payBandOthers >= 3)
		{
			panelArray[4].transform.FindChild("btn_PayIncrease").GetComponent<Button>().interactable = false;
		}
		SetPayScale();
		RefreshTabs();
	}

    public void AddStaffMember(int type) 
    {
        switch (type) 
        {
            case 1:
               StaffMember newGuyHS = new StaffMember(staffType.HotelServices);
               Staff.staffHotelServices.Add(newGuyHS);
                break;
            case 2:
                StaffMember newGuyFB = new StaffMember(staffType.FoodAndBeverages);
               Staff.staffFoodAndBeverages.Add(newGuyFB);
                break;
            case 3:
                StaffMember newGuyFD = new StaffMember(staffType.FrontDesk);
               Staff.staffFrontDesk.Add(newGuyFD);
                break;
            case 4:
                StaffMember newGuyC = new StaffMember(staffType.Conference);
               Staff.staffConference.Add(newGuyC);
                break;
            case 5:
                StaffMember newGuyO = new StaffMember(staffType.Others);
               Staff.staffOthers.Add(newGuyO);
                break;
        }
        RefreshTabs();
    }

    public void RemoveStaffMember(int type)
    {
        switch (type) 
        {
            case 1:
                if (Staff.staffHotelServices.Count > 0)
                {
                    Staff.staffHotelServices.RemoveAt(Staff.staffHotelServices.Count - 1);
                }
                break;
            case 2:
                if (Staff.staffFoodAndBeverages.Count > 0)
                {
                    Staff.staffFoodAndBeverages.RemoveAt(Staff.staffFoodAndBeverages.Count - 1);
                }
                break;
            case 3:
                if (Staff.staffFrontDesk.Count > 0)
                {
                    Staff.staffFrontDesk.RemoveAt(Staff.staffFrontDesk.Count - 1);
                }
                break;
            case 4:
                if (Staff.staffConference.Count > 0)
                { 
                    Staff.staffConference.RemoveAt(Staff.staffConference.Count - 1);
                }
                break;
            case 5:
                if (Staff.staffOthers.Count > 0)
                {
                    Staff.staffOthers.RemoveAt(Staff.staffOthers.Count - 1);
                }
                break;
        }
        RefreshTabs();
    }
	public void ModifyTrainingHours(int type)
	{
		switch(Mathf.Abs(type))
		{
		case 1:
			if(type > 0)
			{
				trainingAllocation[0]++;
			}
			else
			{
				trainingAllocation[0]--;
			}
			break;
		case 2:
			if(type > 0)
			{
				trainingAllocation[1]++;
			}
			else
			{
				trainingAllocation[1]--;
			}
			break;
		case 3:
			if(type > 0)
			{
				trainingAllocation[2]++;
			}
			else
			{
				trainingAllocation[2]--;
			}
			break;
		case 4:
			if(type > 0)
			{
				trainingAllocation[3]++;
			}
			else
			{
				trainingAllocation[3]--;
			}
			break;
		case 5:
			if(type > 0)
			{
				trainingAllocation[4]++;
			}
			else
			{
				trainingAllocation[4]--;
			}
			break;
		}
		if(trainingAllocation[Mathf.Abs(type)-1] <= 0)
		{
			panelArray[Mathf.Abs(type)-1].transform.FindChild("btn_TrainingDecrease").GetComponent<Button>().interactable = false;
		}
		else
		{
			panelArray[Mathf.Abs(type)-1].transform.FindChild("btn_TrainingDecrease").GetComponent<Button>().interactable = true;
		}
		if (trainingAllocation[Mathf.Abs(type)-1] >= 20)
		{
			panelArray[Mathf.Abs(type)-1].transform.FindChild("btn_TrainingIncrease").GetComponent<Button>().interactable = false;
		}
		else
		{
			panelArray[Mathf.Abs(type)-1].transform.FindChild("btn_TrainingIncrease").GetComponent<Button>().interactable = true;
		}
		RefreshTabs();
	}

}
