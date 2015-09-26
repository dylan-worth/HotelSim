using UnityEngine;
using System.Collections;

public enum groupType{conference, group, specialEvent, noneBooked}

public struct specialBookings{


	public int numberOfRooms, numberOfDays;
	public groupType type;


	public specialBookings(int numRooms, int numDays, groupType type)
	{
		this.numberOfDays = numDays;
		this.numberOfRooms = numRooms;
		this.type = type;
	}
}
static public class GroupBookings {

	static public float smallGroupChance = 60f;
	static public float mediumGroupChance = 30f;
	static public float largeGroupChance = 10f;
	static public float conferenceBookingChance = 10f;
	static public float groupBookingChance = 60f;
	static public float eventBookingChance = 30f;

	static int smallGroup = 10;
	static int mediumGroup = 25;
	static int largeGroup = 40;

	static float oneDayChances = 10f;
	static float twoDayChances = 45f;
	static float threeDayChances = 30f;
	static float fourDayChances = 10f;


	static public specialBookings bookSpecial()
	{
		int numberOfRooms, numberOfDays;
		groupType type;
		//randoms the type of booking.
		int rand = Random.Range(1,101);
			if(rand <= conferenceBookingChance)
			{
				type = groupType.conference;
			}
			else if(rand <= groupBookingChance + conferenceBookingChance)
			{
				type = groupType.group;
			}
			else
			{
				type = groupType.specialEvent;
			}
		//randoms the number of days booked.
		int days = Random.Range(1, 101);
		if(days <= oneDayChances)
			numberOfDays = 1;
			else if(days <= oneDayChances+twoDayChances)
			numberOfDays = 2;
				else if(days <= oneDayChances+twoDayChances+threeDayChances)
			numberOfDays = 3;
					else if(days <= oneDayChances+twoDayChances+threeDayChances+fourDayChances)
			numberOfDays = 4;
						else
			numberOfDays = 5;
		//randoms the size of group and number of rooms needed.
		int size = Random.Range(1,101);
		if(size <= smallGroupChance)
		{
			int randSize = Random.Range(-2,3);
			numberOfRooms = smallGroup + randSize;
		}
		else if(size <= smallGroupChance + mediumGroup)
		{
			int randSize = Random.Range(-5,6);
			numberOfRooms = mediumGroup + randSize;
		}
		else
		{
			int randSize = Random.Range(-8,9);
			numberOfRooms = largeGroup + randSize;
		}



		specialBookings booking = new specialBookings(numberOfRooms,numberOfDays,type);
		return booking;
	}
}
