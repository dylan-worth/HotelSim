using UnityEngine;
using System.Collections;

public class ReceptionLog
{
    //Variables (Standard)
    public int month;	//Which week this data is for
    //Variables (Costs)
    //standard rooms
    public RoomCost standardRoom = new RoomCost();
    //double rooms
    public RoomCost doubleRoom = new RoomCost();
    //Deluxe rooms
    public RoomCost deluxeRoom = new RoomCost();
    //suite
    public RoomCost suiteRoom = new RoomCost();
    //master suites
    public RoomCost masterSuiteRoom = new RoomCost();


    public float conferenceHeadCost = 50; 			//Conference shit, worry about this later
    public float conferenceWeekendDiscount;		//Conference shit, worry about this later
    public float conferenceRoomsDiscount;       //conference variable not yet in use.
    public float groupHeadCost = 50;                 //group variable not used yet.
    public float groupRoomsDiscount;            //groupd variable not yet in use.
    public float eventsCost = 50;                    //event booking variable not in use.
    public float conferenceRevenueSplitFood;    //not in use
    public float groupRevenueSplitFood;         //not in use
    public float eventRevenueSplitFood;         //not in use.
    public float conferenceConfRoomsDiscount;   //not in use
    public float eventConfRoomsDiscount; 		//not in use.
    public float eventWeekEndDiscount;          //not in use.

    //Variables (Results)
    public int roomsAvailable;		//How many rooms the hotel had this week
    public int weekdayRoomsBooked;	//How many rooms were booked during the week
    public int weekendRoomsBooked;	//How many rooms were booked during the weekend


    public ReceptionLog DeepCopy()
    {
        ReceptionLog other = new ReceptionLog();

        other.month = this.month;

        other.standardRoom = this.standardRoom.DeepCopy();
        other.doubleRoom = this.doubleRoom.DeepCopy();
        other.deluxeRoom = this.deluxeRoom.DeepCopy();
        other.suiteRoom = this.suiteRoom.DeepCopy();
        other.masterSuiteRoom = this.masterSuiteRoom.DeepCopy();

        other.conferenceHeadCost = this.conferenceHeadCost;
        other.conferenceWeekendDiscount = this.conferenceWeekendDiscount;
        other.conferenceRoomsDiscount = this.conferenceRoomsDiscount;
        other.groupHeadCost = this.groupHeadCost;
        other.groupRoomsDiscount = this.groupRoomsDiscount;
        other.eventsCost = this.eventsCost;
        other.conferenceRevenueSplitFood = this.conferenceRevenueSplitFood;
        other.groupRevenueSplitFood = this.groupRevenueSplitFood;
        other.eventRevenueSplitFood = this.eventRevenueSplitFood;
        other.conferenceConfRoomsDiscount = this.conferenceConfRoomsDiscount;
        other.eventConfRoomsDiscount = this.eventConfRoomsDiscount;
        other.eventWeekEndDiscount = this.eventWeekEndDiscount;


        other.roomsAvailable = this.roomsAvailable;
        other.weekdayRoomsBooked = this.weekdayRoomsBooked;
        other.weekendRoomsBooked = this.weekendRoomsBooked;
        return other;
    }
}
