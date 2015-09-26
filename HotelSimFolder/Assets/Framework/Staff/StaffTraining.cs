using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class StaffTraining : MonoBehaviour {

	public Transform departmentHeads;
	public Transform hotelServices;
	public Transform foodAndBeverages; 
	public Transform frontDesk;
	public Transform conference; 
	public Transform others; 


	// Use this for initialization
	void Start () 
	{
		//1. Get the # of staff for each department - YES
		//2. Display # of staff in the # of staff heading/UI input field. - YES
		//3. Calculate producitivty average for each department.


		// Accessing data from Staff list and counting # of objects, then saving # to a string.
			//Staff.staffDepartmentHeads.Count.ToString(); 
			//Staff.staffFoodAndBeverages.Count.ToString ();
			//Staff.staffFrontDesk.Count.ToString (); 
			//Staff.staffConference.Count.ToString (); 
			//Staff.staffHotelServices.Count.ToString (); 
			//Staff.staffOthers.Count.ToString ();
	}

	void OnEnable() //OnEnable() is reserved function in Unity, similar to Start()...
	{
		Debug.Log ("Enabled");
		Debug.Log (Staff.staffDepartmentHead.Count);
		GameObject number0 = departmentHeads.FindChild("Number").gameObject;
		number0.GetComponent<InputField>().text = Staff.staffDepartmentHead.Count.ToString();
		GameObject costToTrain0 = departmentHeads.FindChild("CostToTrain").gameObject;
		costToTrain0.GetComponent<InputField>().text = doTrainingCalculation(Staff.staffDepartmentHead.Count,4.25);

			
		GameObject number1 = hotelServices.FindChild("Number").gameObject;
		number1.GetComponent<InputField>().text = Staff.staffHotelServices.Count.ToString();
		GameObject costToTrain1 = hotelServices.FindChild ("CostToTrain").gameObject;
		costToTrain1.GetComponent<InputField>().text = doTrainingCalculation(Staff.staffHotelServices.Count,2.22);
		
		GameObject number2 = foodAndBeverages.FindChild("Number").gameObject;
		number2.GetComponent<InputField>().text = Staff.staffFoodAndBeverages.Count.ToString();
		GameObject costToTrain2 = foodAndBeverages.FindChild ("CostToTrain").gameObject;
		costToTrain2.GetComponent<InputField>().text = doTrainingCalculation(Staff.staffFoodAndBeverages.Count,1.5);

		GameObject number3 = frontDesk.FindChild("Number").gameObject;
		number3.GetComponent<InputField>().text = Staff.staffFrontDesk.Count.ToString();
		GameObject costToTrain3 = frontDesk.FindChild ("CostToTrain").gameObject;
		costToTrain3.GetComponent<InputField>().text = doTrainingCalculation(Staff.staffFrontDesk.Count,3.10);

		GameObject number4 = conference.FindChild("Number").gameObject;
		number4.GetComponent<InputField>().text = Staff.staffConference.Count.ToString();
		GameObject costToTrain4 = conference.FindChild ("CostToTrain").gameObject;
		costToTrain4.GetComponent<InputField>().text = doTrainingCalculation(Staff.staffConference.Count,.50);
		
		GameObject number5 = others.FindChild("Number").gameObject;
		number5.GetComponent<InputField>().text = Staff.staffOthers.Count.ToString();
		GameObject costToTrain5 = others.FindChild ("CostToTrain").gameObject;
		costToTrain5.GetComponent<InputField>().text = doTrainingCalculation(Staff.staffOthers.Count,.10);
	}

	// Update is called once per frame
	void Update () 
	{
		//3. Select training tier level for each department. 
		//4. Calculate tier level * # of staff in each department. 
	}

	string doTrainingCalculation(int someTypeOfWorker, double trainRate)
	{
		double trainingCost = someTypeOfWorker * trainRate; 
		return trainingCost.ToString(); 
	}

	public void doit(float sliderValue)
	{


	}

	/*
	foreach(StaffMember person in Staff.staffDepartmentHead)
	{
		person.Training += 1;
		person.Productivity += 1; 
		person.Happiness += 1; 
	}*/


}
