using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//Each bedroom holds this script
public static class BedroomBehaviour 
{
	public static float ProdRequired = 0; 	// How much work is required to clean all rooms. 
	public static int roomsAvailable = 0;
	public static List<RoomStats> allBedrooms = new List<RoomStats>();	//Holds all the rooms in the scene
	public static List<RoomStats> uncleanRooms = new List<RoomStats>(); //Holds a copy of all unclean Rooms
	public static float occupancyDiscount = 1f;

	//Adds this room to the list of hotel rooms in this scene
	public static void AddToRoomList (RoomStats RS)
	{
		allBedrooms.Add (RS);
	}

	//Steps the day counter for all rooms
	public static void StepDay (WeekDays curDay)
	{
		//reference the reception log for this week.
		ReceptionLog currentLog = Reception.receptionLogs[Reception.receptionLogs.Count - 1];
		//Iterate through each room stepping their day counters if necessary
		foreach(RoomStats aRoom in allBedrooms)
		{
			if(aRoom.daysUnderConstruction > 0)
				aRoom.daysUnderConstruction--;//Decrease days under construction
			else
			{
				aRoom.DegradeRoom ();
				if(aRoom.daysOccupied > 0)
				{
					int roomTypeSelected = 0;
					float roomPrice = 0;
					//check if this is a special booking
					if(aRoom.typeRented == groupType.noneBooked)
					{
						
						roomCosts roomType = new roomCosts();//assign the correct list of prices and discount.
						switch(aRoom.roomQuality)
						{
						case 1:roomType = currentLog.standardRoom;
							roomTypeSelected = 1;
							Reception.monthlyReports[Reception.monthlyReports.Count-1].numbRoomLvl1Booked++;
							break;
						case 2:roomType = currentLog.doubleRoom;
							roomTypeSelected = 2;
							Reception.monthlyReports[Reception.monthlyReports.Count-1].numbRoomLvl2Booked++;
							break;
						case 3:roomType = currentLog.deluxeRoom;
							roomTypeSelected = 3;
							Reception.monthlyReports[Reception.monthlyReports.Count-1].numbRoomLvl3Booked++;
							break;
						case 4:roomType = currentLog.suiteRoom;
							roomTypeSelected = 4;
							Reception.monthlyReports[Reception.monthlyReports.Count-1].numbRoomLvl4Booked++;
							break;
						case 5:roomType = currentLog.masterSuiteRoom;
							roomTypeSelected = 5;
							Reception.monthlyReports[Reception.monthlyReports.Count-1].numbRoomLvl5Booked++;
							break;
						}
						if(curDay <= WeekDays.Wednesday)
						{
							//weekday rate.
							roomPrice = roomType.weekdayRoomCost;
							//Give discount if 3 days booked or higher:
							if(aRoom.discount3Days)
							{
								//applies the 3 day discount.
								roomPrice *=  (1-roomType.weekday3NightDiscount/100);
							}
							if(aRoom.discountNoBreakfast)
							{
								//applies the no breakfast discount.
								roomPrice *=  (1-roomType.weekdayNoBreakfastDiscount/100);
							}
							if(aRoom.discountNoCancel)
							{
								//applies the no cancellation discount.
								roomPrice *=  (1-roomType.weekdayNoCancelDiscount/100);
							}
						}
						else //If it's a weekend, charge weekend rate
						{
							//weekend rate.
							roomPrice = roomType.weekendRoomCost;
							//Give discount if 3 days booked or higher:
							if(aRoom.discount3Days)
							{
								// applies the 3 night discount.
								roomPrice *=  (1-roomType.weekend3NightDiscount/100);
							}
							if(aRoom.discountNoBreakfast)
							{
								//applies the no breakfast discount.
								roomPrice *= (1-roomType.weekendNoBreakfastDiscount/100);
							}
							if(aRoom.discountNoCancel)
							{
								//applies the no cancellation discount.
								roomPrice *= (1-roomType.weekendNoCancelDiscount/100);
							}
						}
					}
					//set price if conference booking.
					else if(aRoom.typeRented == groupType.conference){
						roomTypeSelected = 6;
						roomPrice = currentLog.conferenceHeadCost;
						if(curDay <= WeekDays.Wednesday)
							roomPrice *= (1-currentLog.conferenceWeekendDiscount/100);
					}
					//set price if group type booking
					else if(aRoom.typeRented == groupType.group){
						roomTypeSelected = 7;
						roomPrice = currentLog.groupHeadCost;
					}
					//set price if special event booked
					else if(aRoom.typeRented == groupType.specialEvent){
						roomTypeSelected = 8;
						roomPrice = currentLog.eventsCost;
						if(curDay <= WeekDays.Wednesday)
							roomPrice *= (1-currentLog.eventWeekEndDiscount/100);
					}
					else{Debug.LogError("Error in groupTypeSelection");}
					//adds the discount from revenue management tab.

					roomPrice *= occupancyDiscount;


				

					switch(roomTypeSelected){
					case 1:Reception.monthlyReports[Reception.monthlyReports.Count-1].revenueRoomLvl1 += roomPrice;
						break;
					case 2:Reception.monthlyReports[Reception.monthlyReports.Count-1].revenueRoomLvl2 += roomPrice;
						break;
					case 3:Reception.monthlyReports[Reception.monthlyReports.Count-1].revenueRoomLvl3 += roomPrice;
						break;
					case 4:Reception.monthlyReports[Reception.monthlyReports.Count-1].revenueRoomLvl4 += roomPrice;
							break;
					case 5:Reception.monthlyReports[Reception.monthlyReports.Count-1].revenueRoomLvl5 += roomPrice;
						break;
					case 6:Reception.monthlyReports[Reception.monthlyReports.Count-1].revenueConferences += roomPrice;
						break;
					case 7:Reception.monthlyReports[Reception.monthlyReports.Count-1].revenueGroups += roomPrice;
						break;
					case 8:Reception.monthlyReports[Reception.monthlyReports.Count-1].revenueEvents += roomPrice;
						break;
					}


					MasterReference.accountsReceivable += roomPrice;
					aRoom.daysOccupied--;
					//Reset scale when room is available again

				}
				if(aRoom.daysOccupied == 0 && aRoom.inUse)
				{
					aRoom.typeRented = groupType.noneBooked;
					if (aRoom.roomCleanliness >= 50)
						aRoom.GetComponent<Renderer>().material.color = Color.white;
					else 
						aRoom.GetComponent<Renderer>().material.color = Color.yellow;
					aRoom.inUse = false;
				}
			}

		}

		ShuffleRooms();
	}

	//Determine how many rooms are still available for rent
	public static void GetNumberOfRoomsAvailable()
	{
		int track  = 0;
		roomsAvailable = 0;
		//Iterate through each room, stepping our count if the room isn't occupied or under construction
		foreach(RoomStats aBedroom in allBedrooms)
		{
			track++;
			if(!aBedroom.inUse && aBedroom.roomCleanliness >= 50 && aBedroom.roomCondition > 10)
			{
				//print (" Passed");
				roomsAvailable++;
			}
		}
		//print ("NumCheck "+track+". NumRooms Availabe " + roomsAvailable + ", TotalRooms " + allBedrooms.Count +".\n First Room Degridation "+allBedrooms[0].roomDegradation +", Cleanliness "+allBedrooms[0].Cleanliness+", daysUnderConstruction "+allBedrooms[0].daysUnderConstruction+ ", and daysOccupied "+allBedrooms[0].daysOccupied);
	}
	
	//Returns the next available room for rent
	public static RoomStats GetNextAvailableRoom()
	{		
		//Iterate through all rooms until suitable one is found
		foreach(RoomStats aRoom in allBedrooms)
		{
			if(!aRoom.inUse && aRoom.roomCleanliness >= 50 && aRoom.roomCondition > 10)
				return aRoom;
		}
		
		//No available rooms:
		return null;
	}

	//Shuffles the order of the room list
	static void ShuffleRooms()
	{
		/*for (int i = 0; i < allBedrooms.Count; i++) {
			RoomStats temp = allBedrooms[i];
			int randomIndex = Random.Range(i, allBedrooms.Count);
			allBedrooms[i] = allBedrooms[randomIndex];
			allBedrooms[randomIndex] = temp;
		}*/
	}

	public static void CleanRooms (float actualProd)
	{
		ProdRequired = 0;
		if (uncleanRooms.Count > 0)
		{
			if (uncleanRooms.Count > 1)
			{
				sortUncleanRooms ();
				foreach (RoomStats aRoom in uncleanRooms) 
				{
					if (actualProd > 0)
					{
						if (100-aRoom.roomCleanliness > actualProd)
						{
							aRoom.roomCleanliness += actualProd;
							break;
						}
						else
						{
							actualProd -= (100-aRoom.roomCleanliness);
							aRoom.roomCleanliness = 100;
							if (aRoom.inUse)
								aRoom.GetComponent<Renderer>().material.color = Color.red;
							else if(aRoom.roomCondition <= 10)
							{
								aRoom.GetComponent<Renderer>().material.color = Color.black;
							}
							else
								aRoom.GetComponent<Renderer>().material.color = Color.white;
						}
					}
					else
						break;
				}
			}
		}
		uncleanRooms.Clear ();
	}

	public static void sortUncleanRooms()
	{
		List<RoomStats> newUncleanRooms = new List<RoomStats>();
		int numInUse = 0;
		foreach (RoomStats aRoom in uncleanRooms)
		{
			if (aRoom.inUse)
			{
				newUncleanRooms.Add(aRoom);
				numInUse++;
			}
			else if (aRoom.roomCleanliness < 100)
			{
				if (numInUse <= newUncleanRooms.Count)
				{
					newUncleanRooms.Add(aRoom);
				}
				else
				{
					int i = numInUse;
					for (; i < newUncleanRooms.Count; i++)
					{
						if (newUncleanRooms[i].roomCleanliness >= aRoom.roomCleanliness)
						{
							newUncleanRooms.Insert(i, aRoom);
							break;
						}
					}
				}
			}
			else 
			{
				newUncleanRooms.Insert(newUncleanRooms.Count, aRoom);
			}
		}
		uncleanRooms.Clear();
		uncleanRooms.AddRange (newUncleanRooms);
	}
}
