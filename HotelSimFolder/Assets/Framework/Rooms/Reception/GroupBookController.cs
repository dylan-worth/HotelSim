using UnityEngine;
using System.Collections;

public class GroupBookController : MonoBehaviour {

	[SerializeField][Tooltip("Chance a small group gets booked.")]
	float smallGroupChance = 60f;
	[SerializeField][Tooltip("Chance a medium group gets booked.")]
	float mediumGroupChance = 30f;
	[SerializeField][Tooltip("Chance a large group gets booked. Not used in code, it is the remainer of chances.")]
	float largeGroupChance = 10f;
	[SerializeField][Tooltip("Chance a conference gets booked.")]
	float conferenceBookingChance = 10f;
	[SerializeField][Tooltip("Chance a group of people gets booked.")]
	float groupBookingChance = 60f;
	[SerializeField][Tooltip("Chance an event gets booked. Not used in the code, it is the remainer of the chances.")]
	float eventBookingChance = 30f;

	[SerializeField][Tooltip("Average size of small groups.")]
	int smallGroup = 10;
	[SerializeField][Tooltip("Max increase/decrease in possible size for group. I.E. if 5, the small group size will be randomly between -5/5 person bigger/smaller.")]
	int smallGroupPlusMinus = 2;
	[SerializeField][Tooltip("Average size of medium groups.")]
	int mediumGroup = 25;
	[SerializeField][Tooltip("Max increase/decrease in possible size for group. I.E. if 5, the small group size will be randomly between -5/5 person bigger/smaller.")]
	int mediumGroupPlusMinus = 5;
	[SerializeField][Tooltip("Average size of large groups.")]
	int largeGroup = 40;
	[SerializeField][Tooltip("Max increase/decrease in possible size for group. I.E. if 5, the small group size will be randomly between -5/5 person bigger/smaller.")]
	int largeGroupPlusMinus = 10;

	[SerializeField][Tooltip("Chance the special booking books for one day.")]
	float oneDayChances = 10f;
	[SerializeField][Tooltip("Chance the special booking books for two day.")]
	float twoDayChances = 45f;
	[SerializeField][Tooltip("Chance the special booking books for three day.")]
	float threeDayChances = 30f;
	[SerializeField][Tooltip("Chance the special booking books for four day.")]
	float fourDayChances = 10f;


	public specialBookings BookSpecial()
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
			int randSize = Random.Range(-smallGroupPlusMinus,smallGroupPlusMinus+1);
			numberOfRooms = smallGroup + randSize;
		}
		else if(size <= smallGroupChance + mediumGroupChance)
		{
			int randSize = Random.Range(-mediumGroupPlusMinus,mediumGroupPlusMinus+1);
			numberOfRooms = mediumGroup + randSize;
		}
		else
		{
			int randSize = Random.Range(-largeGroupPlusMinus,largeGroupPlusMinus+1);
			numberOfRooms = largeGroup + randSize;
		}
		
		
		
		 
		return new specialBookings(numberOfRooms,numberOfDays,type);
	}

}
