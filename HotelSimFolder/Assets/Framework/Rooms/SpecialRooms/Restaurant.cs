using UnityEngine;
using UnityEngine.UI;


public class Restaurant : MonoBehaviour {

   
	//overall condition of the restaurant. Based on staff production and use.
	float condition = 100f;
	//prices set inside the UI tabs.
	float foodPrice = 15f;
	float liquorPrice = 5f;
	//amount sold everyday. RNGED at start day simulation.
	float dailyTotalProdNeeded = 0;
	//amount calculated from sold goods.
	float dailyFoodSales = 0f;
	float dailyBeverageSales = 0f;
	//base amounts required per drinks or covers.
	[SerializeField][Tooltip("Amount of work staff has to output to serve 1 meal.")][Range(0f,5f)]
	float prodRequiredFood = 1f;
	[SerializeField][Tooltip("Amount of work staff has to output to serve 1 drink.")][Range(0f,1.25f)]
	float prodRequiredBeverage = 0.25f;
	//customers lost due to lack of staff or overpriced.
	int lostCustomers = 0;

	//Arrays of median prices for the restaurant and bar. Can be edited inside the inpector.
	[SerializeField][Tooltip("Median prices for food at levels 1-5 of Restaurant. " +
		"I.E. a value of 25 on line 2 give customers no extra insentive or disinsentive to dine on a level 2 restaurant at $25. DO NOT EDIT SIZE")]
	float[] medianFoodPrice = new float[5]{15f,25f,50f,75f,125f};
	[SerializeField][Tooltip("Median prices for drinks at levels 1-5 of Bar. " +
	                         "I.E. a value of 25 on line 2 give customers no extra insentive or disinsentive to have a drink on a level 2 Bar at $25. DO NOT EDIT SIZE")]
	float[] medianBeveragePrice = new float[5]{5f,6.5f,7.5f,9f,11.5f};


	//getters and setters for prices.
	public float getFoodPrice(){return foodPrice;}
	public float getLiquorPrice(){return liquorPrice;}

	public int getLostCustomers(){return lostCustomers;}
	public void setLostCustomers(int set){lostCustomers = set;}
	public GameObject restaurant;

	public void setFoodPrice(Text set)
	{
		foodPrice = float.Parse(set.text);
	}
	public void setLiquorPrice(Text set)
	{
		liquorPrice = float.Parse(set.text);
	}

	void Start()
	{
       
        if (Reception.restaurantBooks.Count != 0)
		{
			setTab();
		}
	}
	public void SimulateDay(){} //could swap to daily simulation later.
	public RestaurantBook SimulateMonth(int numbOfGuest, int days)
	{
		float numbFOOD = numbOfGuest/days;
		float numbDRINK = numbOfGuest/days;
		RestaurantBook newMonth = new RestaurantBook (Calendar.GetDate(), days , MasterReference.restaurantLevel, MasterReference.barLevel
		                                              , Staff.staffFoodAndBeverages.Count, foodPrice, liquorPrice, condition);
		//reset daily numbers at start of day. Need to save them somewhere later.
		dailyFoodSales = 0;
		dailyBeverageSales = 0;
		dailyTotalProdNeeded = 0;
		lostCustomers = 0;

		float staffProduction = CalculateProductivity();
		float foodPriceRatio = medianFoodPrice [MasterReference.restaurantLevel - 1] / foodPrice;
		float beveragePriceRatio = medianBeveragePrice [MasterReference.barLevel - 1] / liquorPrice;
		//Simulate one day at the time.
		for (int k = 0; k < days; k++) 
		{	

			float requiredProduction = (numbFOOD*prodRequiredFood+numbDRINK*prodRequiredBeverage);  //production required for the day.


			//condition checks.
			condition += (staffProduction-requiredProduction)/100f;
			if(condition > 100f)
				condition = 100f;//can't have better than 100 condition.
			if(condition < 0)
				condition = 0f;//can't have worst than 0 condition.
			//.......................
			float rngRatioFood = condition * foodPriceRatio;
			float rngRatioBeverage = condition * beveragePriceRatio;
			for (int i = 0; i < numbFOOD; i++) 
			{
				if (RandomGenerator (rngRatioFood)) 
				{
					dailyFoodSales += foodPrice;
				} 
				else{lostCustomers++;}
			}
			//beverage purchases.
			for (int j = 0; j < numbDRINK; j++) {
				if (RandomGenerator (rngRatioBeverage)) 
				{
					dailyBeverageSales += liquorPrice;
				}
				else{lostCustomers++;}
			}


		}
		newMonth.staffCost = StaffCost ();
		newMonth.lostCustomers = lostCustomers;
		newMonth.totalFoodSales = dailyFoodSales;
		newMonth.totalBeverageSales = dailyBeverageSales;
		newMonth.conditionEnd = condition;
		newMonth.productivityNeeded = dailyTotalProdNeeded;
		return newMonth;
	}
	

	public float CalculateProductivity()
	{
		float productivity = 0f;
		foreach (StaffMember employee in Staff.staffFoodAndBeverages)
		{
			productivity += employee.CalculateProd();
		}

		return productivity;
	}

	public void setTab()
	{
		restaurant.transform.FindChild ("Txt_LevelRestaurant").gameObject.transform.FindChild("OutPut").gameObject.GetComponent<InputField>().text
			= MasterReference.restaurantLevel.ToString();
		restaurant.transform.FindChild ("Txt_CostPerMeal").gameObject.transform.FindChild("OutPut").gameObject.GetComponent<InputField>().text
			= foodPrice.ToString();
		restaurant.transform.FindChild ("Txt_LevelBar").gameObject.transform.FindChild("OutPut").gameObject.GetComponent<InputField>().text 
			= MasterReference.barLevel.ToString();
		restaurant.transform.FindChild ("Txt_CostPerDrink").gameObject.transform.FindChild("OutPut").gameObject.GetComponent<InputField>().text 
			= liquorPrice.ToString();
		restaurant.transform.FindChild ("Txt_AmountStaff").gameObject.transform.FindChild("OutPut").gameObject.GetComponent<InputField>().text 
			= Staff.staffFoodAndBeverages.Count.ToString();

		restaurant.transform.FindChild ("Txt_LatestMonth").gameObject.transform.FindChild ("OutPut").gameObject.GetComponent<InputField> ().text =
            "$" + Reception.restaurantBooks[Reception.restaurantBooks.Count - 1].GetRevenue().ToString();
		//latest staffing costs.
		restaurant.transform.FindChild ("Txt_MonthCost").gameObject.transform.FindChild("OutPut").gameObject.GetComponent<InputField>().text =
            "$" + (Reception.restaurantBooks[Reception.restaurantBooks.Count - 1].staffCost).ToString();
	
		restaurant.transform.FindChild ("Txt_YTD").gameObject.transform.FindChild("OutPut").gameObject.GetComponent<InputField>().text
			= "$"+YTDRevenue().ToString();
		//YTD staffing costs.
		float ytdStaffTotal = 0;
        foreach (RestaurantBook rb in Reception.restaurantBooks) 
		{
			ytdStaffTotal += rb.staffCost;
		}
		restaurant.transform.FindChild ("Txt_YTDCost").gameObject.transform.FindChild("OutPut").gameObject.GetComponent<InputField>().text
			= "$"+ytdStaffTotal.ToString();
		//total customer lost.
		restaurant.transform.FindChild ("Txt_LostCustomersMonth").gameObject.transform.FindChild("OutPut").gameObject.GetComponent<InputField>().text
            = Reception.restaurantBooks[Reception.restaurantBooks.Count - 1].lostCustomers.ToString();
		float ytdTotal = 0;
        foreach (RestaurantBook rb in Reception.restaurantBooks) 
		{
			ytdTotal += rb.lostCustomers;
		}
		restaurant.transform.FindChild ("Txt_LostCustomersYTD").gameObject.transform.FindChild("OutPut").gameObject.GetComponent<InputField>().text
			= ytdTotal.ToString();

	}
	//calculate the cost of staffing.
	float StaffCost()
	{
		float costs = 0f;
		foreach (StaffMember aStaff in Staff.staffFoodAndBeverages) {
			costs += (aStaff.PayRate * 8*Calendar.GetNumberOfWeeksInMonth());
		}
		return costs;
	}
	float YTDRevenue(){
		float totals = 0f;
        foreach (RestaurantBook rb in Reception.restaurantBooks) 
		{
			totals += rb.GetRevenue();
		}
		return totals;
	}

	bool RandomGenerator(float ratio)
	{
		float rnd = Random.Range (1,101);
		if (rnd >= ratio) 
		{
			return false;
		} else
			return true;
	}

}
