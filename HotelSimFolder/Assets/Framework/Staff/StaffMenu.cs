using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum payBandEnum {nonePay, PayBandOne , PayBandTwo, PayBandThree }

public class StaffingLog{

	public int departmentHeads = 0;
	public payBandEnum departmentHeadsP = payBandEnum.PayBandOne;
	public int hotelService = 0;
	public payBandEnum hotelServiceP = payBandEnum.PayBandOne;
	public int foodAndBev = 0;
	public payBandEnum foodAndBevP = payBandEnum.PayBandOne;
	public int frontDesk = 0;
	public payBandEnum frontDeskP = payBandEnum.PayBandOne;
	public int conferenceAndBanquet = 0;
	public payBandEnum conferenceAndBanquetP = payBandEnum.PayBandOne;
	public int others = 0;
	public payBandEnum othersP = payBandEnum.PayBandOne;


	
	public int month;

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

    GameObject staffMenuTab;//Staff menu panel.
    GameObject panelHS;//The 5 difference panles of the staff menu page.
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
    }
    void Start()
    {
        SetFirstRunStaffing();
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
        Debug.Log( Staff.staffHotelServices.Count);
        List<List<StaffMember>> staffList = Staff.GetStaffingList();
        for (int i = 0; i < panelArray.Length; i++)
        {
            panelArray[i].transform.FindChild("Input_NumberOfStaff").GetComponent<InputField>().text = staffList[i + 1].Count.ToString();
        }
    }
	
	public float[] ReturnCostPerDay ()
	{

		float[] returnValue = new float[6]{0,0,0,0,0,0};
		foreach (StaffMember aMember in Staff.staffDepartmentHead)
		{
			returnValue[0] += aMember.PayRate*8;
		}
		foreach (StaffMember aMember in Staff.staffHotelServices)
		{
			returnValue[1] += aMember.PayRate*8;
		}
		foreach (StaffMember aMember in Staff.staffFoodAndBeverages)
		{
			returnValue[2] += aMember.PayRate*8;
		}
		foreach (StaffMember aMember in Staff.staffFrontDesk)
		{
			returnValue[3] += aMember.PayRate*8;
		}
		foreach (StaffMember aMember in Staff.staffConference)
		{
			returnValue[4] += aMember.PayRate*8;
		}
		foreach (StaffMember aMember in Staff.staffOthers)
		{
			returnValue[5] += aMember.PayRate*8;
		}

		return returnValue;
	}
	public float returnActualProduction (float ProductionNeeded)
	{
		float actualProd = 0;
		foreach (StaffMember aMember in Staff.staffDepartmentHead)
		{
			actualProd += aMember.CalculateProd()*8;
			aMember.HoursWorked = 8;
			aMember.Experience++;
		}
		foreach (StaffMember aMember in Staff.staffHotelServices)
		{
			actualProd += aMember.CalculateProd()*8;
			aMember.HoursWorked = 8;
			aMember.Experience++;
		}
		foreach (StaffMember aMember in Staff.staffFoodAndBeverages)
		{
			actualProd += aMember.CalculateProd()*8;
			aMember.HoursWorked = 8;
			aMember.Experience++;
		}
		foreach (StaffMember aMember in Staff.staffFrontDesk)
		{
			actualProd += aMember.CalculateProd()*8;
			aMember.HoursWorked = 8;
			aMember.Experience++;
		}
		foreach (StaffMember aMember in Staff.staffConference)
		{
			actualProd += aMember.CalculateProd()*8;
			aMember.HoursWorked = 8;
			aMember.Experience++;
		}
		foreach (StaffMember aMember in Staff.staffOthers)
		{
			actualProd += aMember.CalculateProd()*8;
			aMember.HoursWorked = 8;
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

}
