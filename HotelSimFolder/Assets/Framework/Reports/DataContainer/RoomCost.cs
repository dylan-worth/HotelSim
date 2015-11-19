public class RoomCost {

    //Holds all information pertaining to reception behaviour's for a single week
    //NOTE: these need default values for starting or something to setup first time use
  
    public float weekdayRoomCost;				//Cost of renting a room on a weekday (mon - thurs)
    public float weekendRoomCost;				//Cost of a room on weekends (fri/sat/sun)
    public float weekday3NightDiscount;		//Discount for a weekday room if rented for three nights
    public float weekend3NightDiscount;		//Discount for a weekend room if rented for three nights
    public float weekdayNoBreakfastDiscount;	//Discount for a weekday room if no breakfast 
    public float weekendNoBreakfastDiscount;	//Discount for a weekend room if no breakfast
    public float weekdayNoCancelDiscount;		//Discount for a weekday room if there's no cancellation return
    public float weekendNoCancelDiscount;		//Discount for a weekend room if there's no cancellation return

    public RoomCost(){}

    public RoomCost(float wDR, float wER, float wD3, float wE3, float wDB, float wEB, float wDC, float wEC)
    {
        weekdayRoomCost = wDR;				//Cost of renting a room on a weekday (mon - thurs)
        weekendRoomCost = wER;				//Cost of a room on weekends (fri/sat/sun)
        weekday3NightDiscount = wD3;		//Discount for a weekday room if rented for three nights
        weekend3NightDiscount = wE3;		//Discount for a weekend room if rented for three nights
        weekdayNoBreakfastDiscount = wDB;	//Discount for a weekday room if no breakfast 
        weekendNoBreakfastDiscount = wEB;	//Discount for a weekend room if no breakfast
        weekdayNoCancelDiscount = wDC;		//Discount for a weekday room if there's no cancellation return
        weekendNoCancelDiscount = wEC;		//Discount for a weekend room if there's no cancellation return
    }
    public RoomCost DeepCopy()
    {
        RoomCost other = new RoomCost();
        other.weekdayRoomCost = this.weekdayRoomCost;
        other.weekendRoomCost = this.weekendRoomCost;
        other.weekday3NightDiscount = this.weekday3NightDiscount;
        other.weekend3NightDiscount = this.weekend3NightDiscount;
        other.weekdayNoBreakfastDiscount = this.weekdayNoBreakfastDiscount;
        other.weekendNoBreakfastDiscount = this.weekendNoBreakfastDiscount;
        other.weekdayNoCancelDiscount = this.weekdayNoCancelDiscount;
        other.weekendNoCancelDiscount = this.weekendNoCancelDiscount;
        return other;
    }
    
}
