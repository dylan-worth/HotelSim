using System.Collections.Generic;

public static class Staff 
{	
	public static List<StaffMember> staffDepartmentHead;
	public static List<StaffMember> staffHotelServices;
	public static List<StaffMember> staffFoodAndBeverages;
	public static List<StaffMember> staffFrontDesk;
	public static List<StaffMember> staffConference;
	public static List<StaffMember> staffOthers;

    public static List<List<StaffMember>> GetStaffingList()
    {
        List<List<StaffMember>> staffingList = new List<List<StaffMember>>();
        staffingList.Add(staffDepartmentHead);
        staffingList.Add(staffHotelServices);
        staffingList.Add(staffFoodAndBeverages);
        staffingList.Add(staffFrontDesk);
        staffingList.Add(staffConference);
        staffingList.Add(staffOthers);
        return staffingList;
    }
}