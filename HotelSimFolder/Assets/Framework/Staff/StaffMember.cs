public enum staffType{ DepartmentHead, HotelServices, FoodAndBeverages, FrontDesk, Conference, Others }

public class StaffMember
{
	public staffType Type;
	public float Training;
	//public int Age;
	public float PayRate;
	public float Happiness;
	public float Overtime;
	public float HoursWorked;
	public int Benefits;
	public int Experience=0;

	public float baseProductivity = 50;

	public StaffMember (staffType newType,
	                    float newTraining = 1,
	                    float newPayRate = 12,
	                    int newBenefits = 1                                                                                                                                                                                                                                                                                                                                                                                                                             ,
	                    int newExperience = 0,
	                    float newHappiness = 80,
	                    float newProductivity = 50)
	{
		Type = newType;
		Training = newTraining;
		PayRate = newPayRate;
		Benefits = newBenefits;
		Experience = newExperience;
		Happiness = newHappiness;
		baseProductivity = newProductivity;
	}


	public float CalculateProd ()
	{
		float Productivity = (baseProductivity * Training)*Happiness/100;
		return Productivity;
	}
	public void SetPay(float payBand)
	{
		switch(Type)
		{
		case staffType.DepartmentHead:
			PayRate = MasterReference.payScales[0];
			break;
		case staffType.HotelServices:
			PayRate = MasterReference.payScales[1];
			break;
		case staffType.FoodAndBeverages:
			PayRate = MasterReference.payScales[2];
			break;
		case staffType.FrontDesk:
			PayRate = MasterReference.payScales[3];
			break;
		case staffType.Conference:
			PayRate = MasterReference.payScales[4];
			break;
		case staffType.Others:
			PayRate = MasterReference.payScales[5];
			break;
		}
	}
}